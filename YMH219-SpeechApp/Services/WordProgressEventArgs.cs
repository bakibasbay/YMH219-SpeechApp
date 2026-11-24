namespace SpeechApp.Services;

// Okunan kelimenin metin içindeki konumunu taşır
public sealed class WordProgressEventArgs : EventArgs
{
    public WordProgressEventArgs(int position, int length, string word)
    {
        Position = position;
        Length = length;
        Word = word;
    }

    public int Position { get; }

    public int Length { get; }

    public string Word { get; }
}
