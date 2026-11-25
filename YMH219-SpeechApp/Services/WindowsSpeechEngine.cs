using System.Runtime.InteropServices;
using SpeechApp.Models;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;
using WinSynthesizer = Windows.Media.SpeechSynthesis.SpeechSynthesizer;

namespace SpeechApp.Services;

// Windows.Media.SpeechSynthesis ile metin okuma (OneCore sesleri, ör. Microsoft Tolga).
// Metin ses akışına dönüştürülür ve MediaPlayer ile çalınır; kelime sınırları akıştaki
// "SpeechWord" meta veri izinden (SpeechCue) okunur.
internal sealed class WindowsSpeechEngine : ISpeechEngine
{
    private enum PlaybackState { Ready, Speaking, Paused }

    private readonly WinSynthesizer _synthesizer = new();
    private readonly MediaPlayer _player = new();
    private readonly object _sync = new();
    private PlaybackState _state = PlaybackState.Ready;
    private int _rate;
    private bool _disposed;

    public WindowsSpeechEngine()
    {
        _player.AutoPlay = false;
        _player.MediaEnded += Player_MediaEnded;
        _player.MediaFailed += Player_MediaFailed;
    }

    public event EventHandler<WordProgressEventArgs>? WordProgress;
    public event EventHandler? StateChanged;
    public event EventHandler<Exception?>? SpeakFinished;

    public bool IsSpeaking => _state == PlaybackState.Speaking;

    public bool IsPaused => _state == PlaybackState.Paused;

    // Hız sentez sırasında uygulanır (bir sonraki okumada geçerli olur)
    public int Rate
    {
        set => _rate = Math.Clamp(value, -10, 10);
    }

    // Ses düzeyi çalma sırasında da anında uygulanır
    public int Volume
    {
        set => _player.Volume = Math.Clamp(value, 0, 100) / 100.0;
    }

    public IEnumerable<VoiceOption> GetVoices()
    {
        try
        {
            return WinSynthesizer.AllVoices
                .Select(v => new VoiceOption(v.Id, v.DisplayName, v.Language,
                    v.Gender == VoiceGender.Female ? "Kadın" : "Erkek", SpeechEngineKind.Windows))
                .ToList();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException or TypeLoadException)
        {
            return Array.Empty<VoiceOption>();
        }
    }

    public void SelectVoice(VoiceOption voice)
    {
        var info = WinSynthesizer.AllVoices.FirstOrDefault(v => v.Id == voice.Id)
            ?? throw new ServiceException("Ses Seçilemedi", $"\"{voice.Name}\" sesi bulunamadı. Başka bir ses seçin.");
        _synthesizer.Voice = info;
    }

    public async Task SpeakAsync(string text)
    {
        Stop();

        SpeechSynthesisStream stream;
        try
        {
            _synthesizer.Options.IncludeWordBoundaryMetadata = true;
            _synthesizer.Options.SpeakingRate = ToSpeakingRate(_rate);
            stream = await _synthesizer.SynthesizeTextToStreamAsync(text);
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException or ArgumentException)
        {
            throw new ServiceException("Ses Sentezlenemedi",
                "Metin Windows ses motoruyla sese dönüştürülemedi. Başka bir ses seçip tekrar deneyin.\n\nAyrıntı: " + ex.Message, ex);
        }

        var item = new MediaPlaybackItem(MediaSource.CreateFromStream(stream, stream.ContentType));
        RegisterWordBoundaryTracks(item);

        try
        {
            _player.Source = item;
            _player.Play();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            throw new ServiceException("Ses Aygıtı Hatası",
                "Ses çalınamadı. Hoparlör veya kulaklığın bağlı ve etkin olduğundan emin olun.", ex);
        }

        SetState(PlaybackState.Speaking);
    }

    public void Pause()
    {
        if (_state != PlaybackState.Speaking)
            return;

        _player.Pause();
        SetState(PlaybackState.Paused);
    }

    public void Resume()
    {
        if (_state != PlaybackState.Paused)
            return;

        _player.Play();
        SetState(PlaybackState.Speaking);
    }

    // Çalmayı durdurur; SAPI ile aynı davranış için "bitti" olayı da tetiklenir
    public void Stop()
    {
        if (_state == PlaybackState.Ready)
            return;

        _player.Pause();
        _player.Source = null;
        SetState(PlaybackState.Ready);
        SpeakFinished?.Invoke(this, null);
    }

    // Ses akışındaki kelime sınırı izleri uygulama tarafından işlenecek şekilde kaydedilir
    private void RegisterWordBoundaryTracks(MediaPlaybackItem item)
    {
        for (int i = 0; i < item.TimedMetadataTracks.Count; i++)
            RegisterTrack(item, i);

        // İzler kaynak açıldıktan sonra da eklenebilir
        item.TimedMetadataTracksChanged += (sender, args) =>
        {
            if (args.CollectionChange == CollectionChange.ItemInserted)
                RegisterTrack(sender, (int)args.Index);
            else if (args.CollectionChange == CollectionChange.Reset)
                for (int i = 0; i < sender.TimedMetadataTracks.Count; i++)
                    RegisterTrack(sender, i);
        };
    }

    private void RegisterTrack(MediaPlaybackItem item, int index)
    {
        var track = item.TimedMetadataTracks[index];
        if (track.Id != "SpeechWord")
            return;

        track.CueEntered += Track_CueEntered;
        item.TimedMetadataTracks.SetPresentationMode((uint)index, TimedMetadataTrackPresentationMode.ApplicationPresented);
    }

    private void Track_CueEntered(TimedMetadataTrack sender, MediaCueEventArgs args)
    {
        if (_state == PlaybackState.Ready || args.Cue is not SpeechCue cue)
            return;
        if (cue.StartPositionInInput is not int start || cue.EndPositionInInput is not int end)
            return;

        // Bitiş konumu kelimenin son karakterini gösterir
        WordProgress?.Invoke(this, new WordProgressEventArgs(start, end - start + 1, cue.Text));
    }

    private void Player_MediaEnded(MediaPlayer sender, object args)
    {
        if (_state == PlaybackState.Ready)
            return;

        SetState(PlaybackState.Ready);
        SpeakFinished?.Invoke(this, null);
    }

    private void Player_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
    {
        if (_state == PlaybackState.Ready)
            return;

        SetState(PlaybackState.Ready);
        SpeakFinished?.Invoke(this, new InvalidOperationException(args.ErrorMessage));
    }

    private void SetState(PlaybackState state)
    {
        lock (_sync)
        {
            if (_state == state)
                return;
            _state = state;
        }

        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    // -10..10 arayüz değeri Windows'un 0,5..3,0 konuşma hızı aralığına çevrilir
    private static double ToSpeakingRate(int rate) =>
        rate < 0 ? 1.0 + rate * 0.05 : 1.0 + rate * 0.2;

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _player.MediaEnded -= Player_MediaEnded;
        _player.MediaFailed -= Player_MediaFailed;
        _player.Pause();
        _player.Source = null;
        _player.Dispose();
        _synthesizer.Dispose();
    }
}
