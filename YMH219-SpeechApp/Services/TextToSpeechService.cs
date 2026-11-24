using System.Runtime.InteropServices;
using System.Speech.Synthesis;
using SpeechApp.Models;

namespace SpeechApp.Services;

// Metni sesli okuma işlemlerini SpeechSynthesizer üzerinden yönetir
public sealed class TextToSpeechService : IDisposable
{
    private readonly SpeechSynthesizer _synthesizer;
    private bool _disposed;

    public TextToSpeechService()
    {
        _synthesizer = new SpeechSynthesizer();
        _synthesizer.SpeakProgress += Synthesizer_SpeakProgress;
        _synthesizer.StateChanged += Synthesizer_StateChanged;
        _synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
    }

    // Okunan her kelimede tetiklenir (vurgulama için)
    public event EventHandler<WordProgressEventArgs>? WordProgress;

    // Okuma / duraklatma / hazır durumları değiştiğinde tetiklenir
    public event EventHandler? StateChanged;

    // Okuma bittiğinde veya durdurulduğunda tetiklenir; hata varsa taşır
    public event EventHandler<Exception?>? SpeakFinished;

    public bool IsSpeaking => !_disposed && _synthesizer.State == SynthesizerState.Speaking;

    public bool IsPaused => !_disposed && _synthesizer.State == SynthesizerState.Paused;

    // Okuma sürüyor ya da duraklatılmış
    public bool IsBusy => IsSpeaking || IsPaused;

    // Konuşma hızı: -10 (yavaş) ile 10 (hızlı) arası
    public int Rate
    {
        get => _synthesizer.Rate;
        set => _synthesizer.Rate = Math.Clamp(value, -10, 10);
    }

    // Ses düzeyi: 0 ile 100 arası
    public int Volume
    {
        get => _synthesizer.Volume;
        set => _synthesizer.Volume = Math.Clamp(value, 0, 100);
    }

    // Bilgisayarda kurulu ve etkin TTS seslerini döndürür
    public IReadOnlyList<VoiceOption> GetVoices()
    {
        ThrowIfDisposed();
        try
        {
            return _synthesizer.GetInstalledVoices()
                .Where(v => v.Enabled)
                .Select(v => new VoiceOption(v.VoiceInfo.Name, v.VoiceInfo.Culture.Name, GenderText(v.VoiceInfo.Gender)))
                .ToList();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            // Konuşma altyapısı bozuksa boş liste dönülür, arayüz uyarı gösterir
            return Array.Empty<VoiceOption>();
        }
    }

    public void SelectVoice(string voiceName)
    {
        ThrowIfDisposed();
        try
        {
            _synthesizer.SelectVoice(voiceName);
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            throw new ServiceException("Ses Seçilemedi", $"\"{voiceName}\" sesi kullanılamıyor. Başka bir ses seçin.", ex);
        }
    }

    // Metni eşzamansız (arayüzü kilitlemeden) okumaya başlar
    public void Speak(string text)
    {
        ThrowIfDisposed();
        if (string.IsNullOrWhiteSpace(text))
            throw new ServiceException("Metin Yok", "Okunacak bir metin girin.");

        if (IsBusy)
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

    // Okumayı tamamen durdurur ve kuyruğu temizler
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

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, this);

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
