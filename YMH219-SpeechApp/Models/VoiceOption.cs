namespace SpeechApp.Models;

// Ses seçim listesinde gösterilen kurulu TTS sesi ve ait olduğu altyapı
public sealed record VoiceOption(string Id, string Name, string Culture, string Gender, SpeechEngineKind Engine)
{
    public string EngineName => Engine == SpeechEngineKind.Sapi ? "SAPI" : "Windows";

    public override string ToString() => $"{Name}  ({Culture}, {Gender}) · {EngineName}";
}
