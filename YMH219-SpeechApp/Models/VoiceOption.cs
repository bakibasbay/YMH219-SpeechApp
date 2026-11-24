namespace SpeechApp.Models;

// Ses seçim listesinde gösterilen kurulu TTS sesi
public sealed record VoiceOption(string Name, string Culture, string Gender)
{
    public override string ToString() => $"{Name}  ({Culture}, {Gender})";
}
