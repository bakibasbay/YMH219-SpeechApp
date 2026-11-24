using System.ComponentModel;
using System.Drawing.Drawing2D;
using SpeechApp.UI;

namespace SpeechApp.Controls;

// Beyaz zeminli, köşeleri yuvarlatılmış ve ince kenarlıklı kart paneli
public class CardPanel : Panel
{
    public CardPanel()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
    }

    [Category("Görünüm")]
    [DefaultValue(12)]
    public int CornerRadius { get; set; } = 12;

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent?.BackColor ?? Theme.Background);

        var rect = new Rectangle(0, 0, Width - 1, Height - 1);
        using var path = Theme.RoundedRect(rect, CornerRadius);
        using var brush = new SolidBrush(BackColor);
        using var pen = new Pen(Theme.Border);
        g.FillPath(brush, path);
        g.DrawPath(pen, path);
    }
}
