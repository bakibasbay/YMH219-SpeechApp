namespace SpeechApp.Models;

// Tek bir konuşma tanıma sonucunu temsil eder: metin, zaman ve güven skoru
public sealed class RecognitionResultEntry
{
    public RecognitionResultEntry(string text, DateTime timestamp, float confidence)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Tanınan metin boş olamaz.", nameof(text));

        Text = text.Trim();
        Timestamp = timestamp;
        // Güven skoru 0 ile 1 arasında tutulur
        Confidence = Math.Clamp(confidence, 0f, 1f);
    }

    public string Text { get; }

    public DateTime Timestamp { get; }

    // 0..1 aralığında tanıyıcının verdiği güven değeri
    public float Confidence { get; }

    // Arayüzde gösterim için yüzde değeri
    public int ConfidencePercent => (int)Math.Round(Confidence * 100);

    public override string ToString() => $"[{Timestamp:HH:mm:ss}] (%{ConfidencePercent}) {Text}";
}
