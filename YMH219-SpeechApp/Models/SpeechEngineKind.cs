namespace SpeechApp.Models;

// Metin okumada kullanılan ses altyapısı
public enum SpeechEngineKind
{
    // System.Speech (SAPI masaüstü sesleri)
    Sapi,

    // Windows.Media.SpeechSynthesis (Windows OneCore sesleri, ör. Microsoft Tolga)
    Windows
}
