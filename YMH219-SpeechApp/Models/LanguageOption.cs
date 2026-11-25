using System.Globalization;

namespace SpeechApp.Models;

// Dil seçim listesinde gösterilen, kurulu bir tanıyıcıya ait dil
public sealed record LanguageOption(CultureInfo Culture, string RecognizerName)
{
    public override string ToString() => $"{Culture.DisplayName} [{Culture.Name}]";
}
