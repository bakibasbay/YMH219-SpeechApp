using System.Globalization;
using System.Runtime.InteropServices;
using SpeechApp.Models;
using Windows.Media.SpeechRecognition;
using WinLanguage = Windows.Globalization.Language;

namespace SpeechApp.Services;

// Windows.Media.SpeechRecognition (OneCore) ile sürekli dikte tanıma.
// Windows 11 24H2 ve sonrasında SAPI tanıyıcısı bulunmadığında kullanılır.
internal sealed class WindowsRecognizerEngine : IRecognizerEngine
{
    // "Çevrimiçi konuşma tanıma" gizlilik ayarı kapalıyken dönen hata kodu
    private const int PrivacyPolicyNotAccepted = unchecked((int)0x80045509);

    private readonly SpeechRecognizer _recognizer;
    private bool _compiled;
    private bool _running;
    private bool _disposed;

    public WindowsRecognizerEngine(CultureInfo culture)
    {
        try
        {
            _recognizer = new SpeechRecognizer(new WinLanguage(culture.Name));
        }
        catch (Exception ex) when (ex is COMException or ArgumentException or InvalidOperationException)
        {
            throw new ServiceException("Tanıyıcı Başlatılamadı",
                $"\"{culture.DisplayName}\" dili için Windows konuşma tanıyıcısı oluşturulamadı.", ex);
        }

        _recognizer.HypothesisGenerated += Recognizer_HypothesisGenerated;
        _recognizer.ContinuousRecognitionSession.ResultGenerated += Session_ResultGenerated;
        _recognizer.ContinuousRecognitionSession.Completed += Session_Completed;
    }

    public event EventHandler<RecognitionResultEntry>? Recognized;
    public event EventHandler<string>? Hypothesized;
    public event EventHandler<ServiceException?>? Stopped;

    // Windows'un dikte (topic) için desteklediği, konuşma paketi kurulu diller
    public static IEnumerable<LanguageOption> GetLanguages()
    {
        var result = new List<LanguageOption>();
        try
        {
            foreach (var language in SpeechRecognizer.SupportedTopicLanguages)
            {
                try
                {
                    result.Add(new LanguageOption(new CultureInfo(language.LanguageTag), RecognizerKind.Windows));
                }
                catch (CultureNotFoundException)
                {
                    // .NET'in tanımadığı dil etiketi atlanır
                }
            }
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException or TypeLoadException)
        {
            // Konuşma altyapısı yoksa boş liste döner
        }

        return result;
    }

    public async Task StartAsync()
    {
        try
        {
            if (!_compiled)
            {
                // Kısıt eklenmediği için varsayılan serbest dikte dil bilgisi derlenir
                var compilation = await _recognizer.CompileConstraintsAsync();
                if (compilation.Status != SpeechRecognitionResultStatus.Success)
                    throw new ServiceException("Tanıyıcı Hazırlanamadı", DescribeStatus(compilation.Status));
                _compiled = true;
            }

            // Kısa sessizliklerde dinlemenin kendiliğinden kapanmaması için
            _recognizer.ContinuousRecognitionSession.AutoStopSilenceTimeout = TimeSpan.FromMinutes(10);
            await _recognizer.ContinuousRecognitionSession.StartAsync();
            _running = true;
        }
        catch (ServiceException)
        {
            throw;
        }
        catch (Exception ex) when (ex.HResult == PrivacyPolicyNotAccepted)
        {
            throw new ServiceException("Çevrimiçi Konuşma Tanıma Kapalı",
                "Windows dikte tanıması için çevrimiçi konuşma tanıma izni gerekiyor.\n\n" +
                "Ayarlar > Gizlilik ve güvenlik > Konuşma > \"Çevrimiçi konuşma tanıma\" seçeneğini açıp tekrar deneyin.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new ServiceException("Mikrofon İzni Yok",
                "Uygulamanın mikrofona erişim izni yok.\n\n" +
                "Ayarlar > Gizlilik ve güvenlik > Mikrofon bölümünde \"Masaüstü uygulamalarının mikrofonunuza erişmesine izin verin\" seçeneğini açın.", ex);
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            throw new ServiceException("Mikrofon Bulunamadı",
                "Dinleme başlatılamadı. Bir mikrofonun bağlı ve varsayılan kayıt aygıtı olarak seçili olduğundan emin olun.\n\nAyrıntı: " + ex.Message, ex);
        }
    }

    public void Stop()
    {
        if (!_running)
            return;

        _running = false;
        try
        {
            // Sonucu beklenmez; oturum arka planda kapanır
            _ = _recognizer.ContinuousRecognitionSession.CancelAsync();
        }
        catch (Exception ex) when (ex is COMException or InvalidOperationException)
        {
            // Oturum zaten kapanmışsa önemsizdir
        }
    }

    private void Recognizer_HypothesisGenerated(SpeechRecognizer sender, SpeechRecognitionHypothesisGeneratedEventArgs args) =>
        Hypothesized?.Invoke(this, args.Hypothesis.Text);

    private void Session_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
    {
        var result = args.Result;
        if (result.Status != SpeechRecognitionResultStatus.Success ||
            result.Confidence == SpeechRecognitionConfidence.Rejected ||
            string.IsNullOrWhiteSpace(result.Text))
            return;

        Recognized?.Invoke(this, new RecognitionResultEntry(result.Text, DateTime.Now, ToScore(result)));
    }

    private void Session_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
    {
        // Kullanıcı durdurmadıysa (sessizlik, ağ, mikrofon) neden bildirilir
        if (!_running)
            return;

        _running = false;
        Stopped?.Invoke(this, args.Status is SpeechRecognitionResultStatus.Success or SpeechRecognitionResultStatus.UserCanceled
            ? null
            : new ServiceException("Dinleme Durdu", DescribeStatus(args.Status)));
    }

    // Ham güven değeri yoksa güven seviyesi yaklaşık bir skora çevrilir
    private static float ToScore(SpeechRecognitionResult result)
    {
        if (result.RawConfidence > 0)
            return (float)result.RawConfidence;

        return result.Confidence switch
        {
            SpeechRecognitionConfidence.High => 0.9f,
            SpeechRecognitionConfidence.Medium => 0.7f,
            _ => 0.4f
        };
    }

    private static string DescribeStatus(SpeechRecognitionResultStatus status) => status switch
    {
        SpeechRecognitionResultStatus.NetworkFailure =>
            "Çevrimiçi konuşma servisine ulaşılamadı. İnternet bağlantınızı kontrol edin.",
        SpeechRecognitionResultStatus.MicrophoneUnavailable =>
            "Mikrofona erişilemedi. Mikrofonun bağlı ve başka bir uygulama tarafından kullanılmıyor olduğundan emin olun.",
        SpeechRecognitionResultStatus.AudioQualityFailure =>
            "Ses kalitesi tanıma için yetersiz. Mikrofona daha yakın ve net konuşun.",
        SpeechRecognitionResultStatus.TimeoutExceeded or SpeechRecognitionResultStatus.PauseLimitExceeded =>
            "Uzun süre konuşma algılanmadığı için dinleme durduruldu.",
        SpeechRecognitionResultStatus.TopicLanguageNotSupported or SpeechRecognitionResultStatus.GrammarLanguageMismatch =>
            "Seçilen dil Windows dikte tanıması tarafından desteklenmiyor.",
        _ => $"Konuşma tanıma durdu ({status})."
    };

    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
        _recognizer.HypothesisGenerated -= Recognizer_HypothesisGenerated;
        _recognizer.ContinuousRecognitionSession.ResultGenerated -= Session_ResultGenerated;
        _recognizer.ContinuousRecognitionSession.Completed -= Session_Completed;
        Stop();
        _recognizer.Dispose();
    }
}
