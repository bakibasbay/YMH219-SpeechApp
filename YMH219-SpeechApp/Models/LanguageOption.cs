using System.Globalization;

namespace SpeechApp.Models;

// Dil seçim listesinde gösterilen, kurulu bir tanıyıcıya ait dil ve altyapısı
public sealed record LanguageOption(CultureInfo Culture, RecognizerKind Engine)
{
    public string EngineName => Engine == RecognizerKind.Sapi ? "SAPI" : "Windows";

    public override string ToString() => $"{Culture.DisplayName} [{Culture.Name}] · {EngineName}";
}
