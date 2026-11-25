using SpeechApp.Models;

namespace SpeechApp.Services;

// Metni sesli okuma işlemlerini yönetir. Seçilen sese göre SAPI (System.Speech)
// ya da Windows (Windows.Media.SpeechSynthesis) motorunu kullanır.
public sealed class TextToSpeechService : IDisposable
{
    private readonly SapiSpeechEngine _sapi = new();
    private readonly WindowsSpeechEngine _windows = new();
    private ISpeechEngine _current;
    private int _rate;
    private int _volume = 100;
    private bool _disposed;

    public TextToSpeechService()
    {
        _current = _sapi;
        foreach (var engine in new ISpeechEngine[] { _sapi, _windows })
        {
            engine.WordProgress += Engine_WordProgress;
            engine.StateChanged += Engine_StateChanged;
            engine.SpeakFinished += Engine_SpeakFinished;
        }
    }

    // Okunan her kelimede tetiklenir (vurgulama için)
    public event EventHandler<WordProgressEventArgs>? WordProgress;

    // Okuma / duraklatma / hazır durumları değiştiğinde tetiklenir
    public event EventHandler? StateChanged;

    // Okuma bittiğinde veya durdurulduğunda tetiklenir; hata varsa taşır
    public event EventHandler<Exception?>? SpeakFinished;

    public bool IsSpeaking => !_disposed && _current.IsSpeaking;

    public bool IsPaused => !_disposed && _current.IsPaused;

    // Okuma sürüyor ya da duraklatılmış
    public bool IsBusy => IsSpeaking || IsPaused;

    // Konuşma hızı: -10 (yavaş) ile 10 (hızlı) arası
    public int Rate
    {
        get => _rate;
        set
        {
            _rate = Math.Clamp(value, -10, 10);
            _current.Rate = _rate;
        }
    }

    // Ses düzeyi: 0 ile 100 arası
    public int Volume
    {
        get => _volume;
        set
        {
            _volume = Math.Clamp(value, 0, 100);
            _current.Volume = _volume;
        }
    }

    // İki altyapıda kurulu tüm sesler (önce SAPI, sonra Windows)
    public IReadOnlyList<VoiceOption> GetVoices()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return _sapi.GetVoices().Concat(_windows.GetVoices()).ToList();
    }

    // Sese göre motor seçilir; motor değişirse süren okuma durdurulur
    public void SelectVoice(VoiceOption voice)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ISpeechEngine target = voice.Engine == SpeechEngineKind.Sapi ? _sapi : _windows;
        if (target != _current)
        {
            _current.Stop();
            _current = target;
        }

        _current.SelectVoice(voice);
        _current.Rate = _rate;
        _current.Volume = _volume;
    }

    public async Task SpeakAsync(string text)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (string.IsNullOrWhiteSpace(text))
            throw new ServiceException("Metin Yok", "Okunacak bir metin girin.");

        _current.Rate = _rate;
        _current.Volume = _volume;
        await _current.SpeakAsync(text);
    }

    public void Pause() => _current.Pause();

    public void Resume() => _current.Resume();

    public void Stop()
    {
        if (!_disposed)
            _current.Stop();
    }

    // Yalnızca etkin motorun olayları dışarı aktarılır
    private void Engine_WordProgress(object? sender, WordProgressEventArgs e)
    {
        if (sender == _current)
            WordProgress?.Invoke(this, e);
    }

    private void Engine_StateChanged(object? sender, EventArgs e)
    {
        if (sender == _current)
            StateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Engine_SpeakFinished(object? sender, Exception? error)
    {
        if (sender == _current)
            SpeakFinished?.Invoke(this, error);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        // İki motor da durdurulup kapatılır
        _disposed = true;
        _sapi.Dispose();
        _windows.Dispose();
    }
}
