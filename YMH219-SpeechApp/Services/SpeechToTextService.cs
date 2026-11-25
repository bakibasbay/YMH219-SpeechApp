using System.Globalization;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using SpeechApp.Models;

namespace SpeechApp.Services;

// Mikrofondan alınan konuşmayı SpeechRecognitionEngine ile metne dönüştürür
public sealed class SpeechToTextService : IDisposable
{
    private SpeechRecognitionEngine? _engine;
    private string? _engineCulture;
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

    // Bilgisayarda kurulu tanıyıcıların dillerini döndürür
    public static IReadOnlyList<LanguageOption> GetInstalledLanguages()
    {
        try
        {
            return SpeechRecognitionEngine.InstalledRecognizers()
                .Select(r => new LanguageOption(r.Culture, r.Name))
                .ToList();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            return Array.Empty<LanguageOption>();
        }
    }

    // Seçilen dilde mikrofonu dinlemeye başlar
    public void Start(CultureInfo culture)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (IsListening)
            return;

        // Dil değiştiyse eski motor kapatılıp yenisi hazırlanır
        if (_engine is null || _engineCulture != culture.Name)
        {
            DisposeEngine();
            _engine = CreateEngine(culture);
            _engineCulture = culture.Name;
        }

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
                "Konuşma tanıma servisi başlatılamadı. Mikrofon başka bir uygulama tarafından kullanılıyor olabilir.", ex);
        }

        IsListening = true;
        ListeningChanged?.Invoke(this, EventArgs.Empty);
    }

    // Dinlemeyi durdurur (motor bir sonraki başlatma için saklanır)
    public void Stop()
    {
        if (!IsListening || _engine is null)
            return;

        _engine.RecognizeAsyncCancel();
        IsListening = false;
        ListeningChanged?.Invoke(this, EventArgs.Empty);
    }

    // Seçilen dile ait tanıyıcıyı bulur ve serbest dikte dil bilgisiyle hazırlar
    private SpeechRecognitionEngine CreateEngine(CultureInfo culture)
    {
        RecognizerInfo? info;
        try
        {
            info = SpeechRecognitionEngine.InstalledRecognizers()
                .FirstOrDefault(r => r.Culture.Name.Equals(culture.Name, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            throw new ServiceException("Konuşma Servisi Hatası", "Windows konuşma tanıma servisine erişilemedi.", ex);
        }

        if (info is null)
        {
            throw new ServiceException("Tanıyıcı Kurulu Değil",
                $"\"{culture.DisplayName}\" dili için kurulu bir konuşma tanıyıcı bulunamadı.");
        }

        SpeechRecognitionEngine? engine = null;
        try
        {
            engine = new SpeechRecognitionEngine(info);
            engine.LoadGrammar(new DictationGrammar());
            engine.SpeechRecognized += Engine_SpeechRecognized;
            engine.SpeechHypothesized += Engine_SpeechHypothesized;
            engine.RecognizeCompleted += Engine_RecognizeCompleted;
            return engine;
        }
        catch (Exception ex) when (ex is InvalidOperationException or COMException or ArgumentException)
        {
            engine?.Dispose();
            throw new ServiceException("Tanıyıcı Başlatılamadı",
                $"\"{culture.DisplayName}\" tanıyıcısı yüklenemedi veya serbest dikteyi desteklemiyor.", ex);
        }
    }

    private void Engine_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.Result.Text))
            return;

        Recognized?.Invoke(this, new RecognitionResultEntry(e.Result.Text, DateTime.Now, e.Result.Confidence));
    }

    private void Engine_SpeechHypothesized(object? sender, SpeechHypothesizedEventArgs e) =>
        Hypothesized?.Invoke(this, e.Result.Text);

    private void Engine_RecognizeCompleted(object? sender, RecognizeCompletedEventArgs e)
    {
        // Kullanıcı durdurmadıysa (ör. mikrofon çıkarıldı) durum ve hata bildirilir
        bool wasListening = IsListening;
        IsListening = false;

        if (e.Error is not null)
        {
            ErrorOccurred?.Invoke(this, new ServiceException("Dinleme Kesildi",
                "Konuşma tanıma beklenmedik şekilde durdu. Mikrofon bağlantısını kontrol edin.\n\nAyrıntı: " + e.Error.Message, e.Error));
        }

        if (wasListening)
            ListeningChanged?.Invoke(this, EventArgs.Empty);
    }

    private void DisposeEngine()
    {
        if (_engine is null)
            return;

        _engine.SpeechRecognized -= Engine_SpeechRecognized;
        _engine.SpeechHypothesized -= Engine_SpeechHypothesized;
        _engine.RecognizeCompleted -= Engine_RecognizeCompleted;
        if (IsListening)
            _engine.RecognizeAsyncCancel();

        _engine.Dispose();
        _engine = null;
        _engineCulture = null;
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
