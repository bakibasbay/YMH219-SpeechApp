using System.Globalization;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using SpeechApp.Models;

namespace SpeechApp.Services;

// System.Speech (SAPI) ile mikrofondan serbest dikte tanıma
internal sealed class SapiRecognizerEngine : IRecognizerEngine
{
    private readonly SpeechRecognitionEngine _engine;
    private bool _running;
    private bool _disposed;

    public SapiRecognizerEngine(CultureInfo culture)
    {
        RecognizerInfo? info;
        try
        {
            info = SpeechRecognitionEngine.InstalledRecognizers()
                .FirstOrDefault(r => r.Culture.Name.Equals(culture.Name, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            throw new ServiceException("Konuşma Servisi Hatası", "Windows konuşma tanıma servisine (SAPI) erişilemedi.", ex);
        }

        if (info is null)
        {
            throw new ServiceException("Tanıyıcı Kurulu Değil",
                $"\"{culture.DisplayName}\" dili için kurulu bir SAPI konuşma tanıyıcısı bulunamadı.");
        }

        try
        {
            _engine = new SpeechRecognitionEngine(info);
            _engine.LoadGrammar(new DictationGrammar());
        }
        catch (Exception ex) when (ex is InvalidOperationException or COMException or ArgumentException)
        {
            throw new ServiceException("Tanıyıcı Başlatılamadı",
                $"\"{culture.DisplayName}\" tanıyıcısı yüklenemedi veya serbest dikteyi desteklemiyor.", ex);
        }

        _engine.SpeechRecognized += Engine_SpeechRecognized;
        _engine.SpeechHypothesized += Engine_SpeechHypothesized;
        _engine.RecognizeCompleted += Engine_RecognizeCompleted;
    }

    public event EventHandler<RecognitionResultEntry>? Recognized;
    public event EventHandler<string>? Hypothesized;
    public event EventHandler<ServiceException?>? Stopped;

    // Kurulu SAPI tanıyıcılarının dilleri
    public static IEnumerable<LanguageOption> GetLanguages()
    {
        try
        {
            return SpeechRecognitionEngine.InstalledRecognizers()
                .Select(r => new LanguageOption(r.Culture, RecognizerKind.Sapi))
                .ToList();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            return Array.Empty<LanguageOption>();
        }
    }

    public Task StartAsync()
    {
        try
        {
            _engine.SetInputToDefaultAudioDevice();
        }
        catch (Exception ex) when (ex is InvalidOperationException or COMException)
        {
            throw new ServiceException("Mikrofon Bulunamadı",
                "Varsayılan bir kayıt aygıtı (mikrofon) bulunamadı.\n\n" +
                "Mikrofonun bağlı olduğundan ve Windows Ayarlar > Gizlilik ve güvenlik > Mikrofon " +
                "bölümünde masaüstü uygulamalarının mikrofona erişimine izin verildiğinden emin olun.", ex);
        }

        try
        {
            _engine.RecognizeAsync(RecognizeMode.Multiple);
        }
        catch (Exception ex) when (ex is InvalidOperationException or COMException)
        {
            throw new ServiceException("Dinleme Başlatılamadı",
                "Konuşma tanıma başlatılamadı. Mikrofon başka bir uygulama tarafından kullanılıyor olabilir.", ex);
        }

        _running = true;
        return Task.CompletedTask;
    }

    public void Stop()
    {
        if (!_running)
            return;

        _running = false;
        _engine.RecognizeAsyncCancel();
    }

    private void Engine_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.Result.Text))
            Recognized?.Invoke(this, new RecognitionResultEntry(e.Result.Text, DateTime.Now, e.Result.Confidence));
    }

    private void Engine_SpeechHypothesized(object? sender, SpeechHypothesizedEventArgs e) =>
        Hypothesized?.Invoke(this, e.Result.Text);

    private void Engine_RecognizeCompleted(object? sender, RecognizeCompletedEventArgs e)
    {
        // Kullanıcı durdurduysa bildirim gerekmez; aksi halde (ör. mikrofon çıkarıldı) hata bildirilir
        if (!_running)
            return;

        _running = false;
        Stopped?.Invoke(this, e.Error is null
            ? null
            : new ServiceException("Dinleme Kesildi",
                "Konuşma tanıma beklenmedik şekilde durdu. Mikrofon bağlantısını kontrol edin.\n\nAyrıntı: " + e.Error.Message, e.Error));
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _engine.SpeechRecognized -= Engine_SpeechRecognized;
        _engine.SpeechHypothesized -= Engine_SpeechHypothesized;
        _engine.RecognizeCompleted -= Engine_RecognizeCompleted;
        Stop();
        _engine.Dispose();
    }
}
