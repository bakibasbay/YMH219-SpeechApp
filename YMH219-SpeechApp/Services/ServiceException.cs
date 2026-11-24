namespace SpeechApp.Services;

// Servislerin kullanıcıya gösterilebilecek anlaşılır hata bilgisi taşıyan istisnası
public sealed class ServiceException : Exception
{
    public ServiceException(string title, string message, Exception? inner = null)
        : base(message, inner)
    {
        Title = title;
    }

    // MessageBox başlığında gösterilecek kısa hata adı
    public string Title { get; }
}
