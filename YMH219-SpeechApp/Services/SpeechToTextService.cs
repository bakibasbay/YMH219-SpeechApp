using SpeechApp.Models;

namespace SpeechApp.Services;

// Mikrofondan alınan konuşmayı metne dönüştürür.
// Seçilen dile göre SAPI (System.Speech) ya da Windows (OneCore) motorunu kullanır.
public sealed class SpeechToTextService : IDisposable
{
    private IRecognizerEngine? _engine;
    private LanguageOption? _engineLanguage;
    private bool _disposed;

    // Bir cümle/ifade kesin olarak tanındığında tetiklenir
    public event EventHandler<RecognitionResultEntry>? Recognized;

    // Konuşma sürerken tahmini (ara) metin geldiğinde tetiklenir
    public event EventHandler<string>? Hypothesized;

    // Dinleme başladığında veya bittiğinde tetiklenir
    public event EventHandler? ListeningChanged;

    // Dinleme sırasında beklenmeyen bir hata oluştuğunda tetiklenir
    public event EventHandler<ServiceException>? ErrorOccurred;

    public bool IsListening { get; private set; }

    // Önce SAPI, ardından Windows altyapısında kurulu diller listelenir
    public static IReadOnlyList<LanguageOption> GetInstalledLanguages() =>
        SapiRecognizerEngine.GetLanguages()
            .Concat(WindowsRecognizerEngine.GetLanguages())
            .ToList();

    // Seçilen dilde mikrofonu dinlemeye başlar
    public async Task StartAsync(LanguageOption language)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (IsListening)
            return;

        // Dil veya altyapı değiştiyse eski motor kapatılıp yenisi hazırlanır
        if (_engine is null || _engineLanguage != language)
        {
            DisposeEngine();
            _engine = language.Engine == RecognizerKind.Sapi
                ? new SapiRecognizerEngine(language.Culture)
                : new WindowsRecognizerEngine(language.Culture);
            _engine.Recognized += Engine_Recognized;
            _engine.Hypothesized += Engine_Hypothesized;
            _engine.Stopped += Engine_Stopped;
            _engineLanguage = language;
        }

        await _engine.StartAsync();
        IsListening = true;
        ListeningChanged?.Invoke(this, EventArgs.Empty);
    }

    // Dinlemeyi durdurur (motor bir sonraki başlatma için saklanır)
    public void Stop()
    {
        if (!IsListening || _engine is null)
            return;

        _engine.Stop();
        IsListening = false;
        ListeningChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Engine_Recognized(object? sender, RecognitionResultEntry entry) => Recognized?.Invoke(this, entry);

    private void Engine_Hypothesized(object? sender, string text) => Hypothesized?.Invoke(this, text);

    private void Engine_Stopped(object? sender, ServiceException? error)
    {
        if (!IsListening)
            return;

        IsListening = false;
        if (error is not null)
            ErrorOccurred?.Invoke(this, error);
        ListeningChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DisposeEngine()
    {
        if (_engine is null)
            return;

        _engine.Recognized -= Engine_Recognized;
        _engine.Hypothesized -= Engine_Hypothesized;
        _engine.Stopped -= Engine_Stopped;
        _engine.Dispose();
        _engine = null;
        _engineLanguage = null;
        IsListening = false;
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        // Dinleme durdurulur ve mikrofon serbest bırakılır
        DisposeEngine();
        _disposed = true;
    }
}
