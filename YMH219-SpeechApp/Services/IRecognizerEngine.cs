using SpeechApp.Models;

namespace SpeechApp.Services;

// Konuşma tanıma motorlarının ortak sözleşmesi (SAPI ve Windows altyapısı)
internal interface IRecognizerEngine : IDisposable
{
    // Bir ifade kesin olarak tanındığında
    event EventHandler<RecognitionResultEntry>? Recognized;

    // Konuşma sürerken ara (tahmini) metin geldiğinde
    event EventHandler<string>? Hypothesized;

    // Dinleme kullanıcı istemeden durduğunda; varsa hata bilgisini taşır
    event EventHandler<ServiceException?>? Stopped;

    Task StartAsync();

    void Stop();
}
