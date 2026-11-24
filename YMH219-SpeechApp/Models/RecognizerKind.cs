namespace SpeechApp.Models;

// Konuşma tanımada kullanılan altyapı türü
public enum RecognizerKind
{
    // System.Speech (SAPI masaüstü tanıyıcısı)
    Sapi,

    // Windows.Media.SpeechRecognition (Windows OneCore / çevrimiçi dikte)
    Windows
}
