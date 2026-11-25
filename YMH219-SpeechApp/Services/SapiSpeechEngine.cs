using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using SpeechApp.Models;

namespace SpeechApp.Services;

// System.Speech (SAPI) SpeechSynthesizer ile metin okuma
internal sealed class SapiSpeechEngine : ISpeechEngine
{
    private readonly SpeechSynthesizer _synthesizer;
    private bool _disposed;

    public SapiSpeechEngine()
    {
        _synthesizer = new SpeechSynthesizer();
        _synthesizer.SpeakProgress += Synthesizer_SpeakProgress;
        _synthesizer.StateChanged += Synthesizer_StateChanged;
        _synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
    }

    public event EventHandler<WordProgressEventArgs>? WordProgress;
    public event EventHandler? StateChanged;
    public event EventHandler<Exception?>? SpeakFinished;

    public bool IsSpeaking => !_disposed && _synthesizer.State == SynthesizerState.Speaking;

    public bool IsPaused => !_disposed && _synthesizer.State == SynthesizerState.Paused;

    public int Rate
    {
        set => _synthesizer.Rate = Math.Clamp(value, -10, 10);
    }

    public int Volume
    {
        set => _synthesizer.Volume = Math.Clamp(value, 0, 100);
    }

    public IEnumerable<VoiceOption> GetVoices()
    {
        try
        {
            return _synthesizer.GetInstalledVoices()
                .Where(v => v.Enabled)
                .Select(v => new VoiceOption(v.VoiceInfo.Name, v.VoiceInfo.Name, v.VoiceInfo.Culture.Name,
                    GenderText(v.VoiceInfo.Gender), SpeechEngineKind.Sapi))
                .ToList();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            // Konuşma altyapısı bozuksa boş liste dönülür, arayüz uyarı gösterir
            return Array.Empty<VoiceOption>();
        }
    }

    public void SelectVoice(VoiceOption voice)
    {
        try
        {
            _synthesizer.SelectVoice(voice.Id);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            throw new ServiceException("Ses Seçilemedi", $"\"{voice.Name}\" sesi kullanılamıyor. Başka bir ses seçin.", ex);
        }
    }

    // Metni eşzamansız (arayüzü kilitlemeden) okumaya başlar
    public Task SpeakAsync(string text)
    {
        if (IsSpeaking || IsPaused)
            Stop();

        try
        {
            _synthesizer.SetOutputToDefaultAudioDevice();
            _synthesizer.SpeakAsync(text);
        }
        catch (Exception ex) when (ex is InvalidOperationException or COMException or System.ComponentModel.Win32Exception)
        {
            throw new ServiceException("Ses Aygıtı Hatası",
                "Varsayılan ses çıkış aygıtına erişilemedi. Hoparlör veya kulaklığın bağlı ve etkin olduğundan emin olun.", ex);
        }

        return Task.CompletedTask;
    }

    public void Pause()
    {
        if (IsSpeaking)
            _synthesizer.Pause();
    }

    public void Resume()
    {
        if (IsPaused)
            _synthesizer.Resume();
    }

    public void Stop()
    {
        if (_disposed)
            return;

        // Duraklatılmış sentezleyici iptal edilmeden önce devam ettirilmeli
        if (IsPaused)
            _synthesizer.Resume();

        _synthesizer.SpeakAsyncCancelAll();
    }

    private void Synthesizer_SpeakProgress(object? sender, SpeakProgressEventArgs e) =>
        WordProgress?.Invoke(this, new WordProgressEventArgs(e.CharacterPosition, e.CharacterCount, e.Text));

    private void Synthesizer_StateChanged(object? sender, System.Speech.Synthesis.StateChangedEventArgs e) =>
        StateChanged?.Invoke(this, EventArgs.Empty);

    private void Synthesizer_SpeakCompleted(object? sender, SpeakCompletedEventArgs e) =>
        SpeakFinished?.Invoke(this, e.Error);

    private static string GenderText(VoiceGender gender) => gender switch
    {
        VoiceGender.Female => "Kadın",
        VoiceGender.Male => "Erkek",
        _ => "Belirsiz"
    };

    public void Dispose()
    {
        if (_disposed)
            return;

        // Olaylar ayrılır, okuma durdurulur ve sentezleyici kapatılır
        _synthesizer.SpeakProgress -= Synthesizer_SpeakProgress;
        _synthesizer.StateChanged -= Synthesizer_StateChanged;
        _synthesizer.SpeakCompleted -= Synthesizer_SpeakCompleted;
        try
        {
            Stop();
        }
        catch (InvalidOperationException)
        {
            // Kapanış sırasında durdurma hatası önemsizdir
        }

        _disposed = true;
        _synthesizer.Dispose();
    }
}
