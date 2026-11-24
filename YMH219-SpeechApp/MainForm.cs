using SpeechApp.Controls;

namespace SpeechApp;

// Ana pencere: Text-to-Speech ve Speech-to-Text bölümlerini barındırır
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
    }

    // Üst menüdeki sekme butonlarına göre ilgili paneli gösterir
    private void ShowPage(bool showTts)
    {
        pnlTts.Visible = showTts;
        pnlStt.Visible = !showTts;
        btnNavTts.Variant = showTts ? ButtonVariant.Primary : ButtonVariant.Ghost;
        btnNavStt.Variant = showTts ? ButtonVariant.Ghost : ButtonVariant.Primary;
    }

    private void BtnNavTts_Click(object? sender, EventArgs e) => ShowPage(showTts: true);

    private void BtnNavStt_Click(object? sender, EventArgs e) => ShowPage(showTts: false);
}
