namespace SpeechApp;

internal static class Program
{
    /// <summary>
    /// Uygulamanın giriş noktası.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Beklenmeyen hatalarda uygulama kapanmasın; kullanıcıya mesaj gösterilsin
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => ShowUnexpectedError(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                ShowUnexpectedError(ex);
        };

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }

    private static void ShowUnexpectedError(Exception ex) =>
        MessageBox.Show("Beklenmeyen bir hata oluştu:\n\n" + ex.Message,
            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
