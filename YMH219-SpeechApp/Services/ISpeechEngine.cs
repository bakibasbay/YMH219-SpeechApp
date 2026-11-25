using SpeechApp.Models;

namespace SpeechApp.Services;

// Metin okuma motorlarının ortak sözleşmesi (SAPI ve Windows altyapısı)
internal interface ISpeechEngine : IDisposable
{
    // Okunan her kelimede tetiklenir (vurgulama için)
    event EventHandler<WordProgressEventArgs>? WordProgress;

    // Okuma / duraklatma / hazır durumları değiştiğinde tetiklenir
    event EventHandler? StateChanged;

    // Okuma bittiğinde veya durdurulduğunda tetiklenir; hata varsa taşır
    event EventHandler<Exception?>? SpeakFinished;

    bool IsSpeaking { get; }

    bool IsPaused { get; }

    // -10 (yavaş) ile 10 (hızlı) arası
    int Rate { set; }

    // 0 ile 100 arası
    int Volume { set; }

    IEnumerable<VoiceOption> GetVoices();

    void SelectVoice(VoiceOption voice);

    Task SpeakAsync(string text);

    void Pause();

    void Resume();

    void Stop();
}
