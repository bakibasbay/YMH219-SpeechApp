using System.ComponentModel;
using System.Drawing.Drawing2D;
using SpeechApp.Models;
using SpeechApp.UI;

namespace SpeechApp.Controls;

// Aktif işlemi renkli bir hap (pill) içinde simge ve yazıyla gösterir.
// Dinleme sırasında mikrofon simgesi yanıp söner.
public class StatusIndicator : Control
{
    private readonly System.Windows.Forms.Timer _blinkTimer;
    private AppStatus _status = AppStatus.Hazir;
    private bool _blinkOn = true;

    public StatusIndicator()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        _blinkTimer = new System.Windows.Forms.Timer { Interval = 500 };
        _blinkTimer.Tick += BlinkTimer_Tick;
    }

    [Category("Davranış")]
    [DefaultValue(AppStatus.Hazir)]
    public AppStatus Status
    {
        get => _status;
        set
        {
            if (_status == value)
                return;

            _status = value;
            _blinkOn = true;
            // Yanıp sönme yalnızca dinleme durumunda çalışır
            _blinkTimer.Enabled = value == AppStatus.Dinleniyor;
            Invalidate();
        }
    }

    // Duruma karşılık gelen yazı ve renk
    public static (string Text, Color Color) Describe(AppStatus status) => status switch
    {
        AppStatus.Okunuyor => ("Okunuyor…", Theme.Success),
        AppStatus.Duraklatildi => ("Duraklatıldı", Theme.Warning),
        AppStatus.Dinleniyor => ("Dinleniyor…", Theme.Danger),
        _ => ("Hazır", Theme.Idle)
    };

    private void BlinkTimer_Tick(object? sender, EventArgs e)
    {
        _blinkOn = !_blinkOn;
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent?.BackColor ?? BackColor);

        var (text, color) = Describe(_status);
        var rect = new Rectangle(0, 0, Width - 1, Height - 1);

        // Hap şeklindeki zemin: durum renginin saydam tonu + kenarlık
        using (var path = Theme.RoundedRect(rect, rect.Height / 2))
        {
            using var fill = new SolidBrush(Color.FromArgb(_status == AppStatus.Dinleniyor && _blinkOn ? 70 : 45, color));
            using var pen = new Pen(Color.FromArgb(200, color), 1.5f);
            g.FillPath(fill, path);
            g.DrawPath(pen, path);
        }

        int iconSize = Math.Min(20, Height - 12);
        var iconRect = new Rectangle(14, (Height - iconSize) / 2, iconSize, iconSize);
        switch (_status)
        {
            case AppStatus.Dinleniyor:
                DrawMicrophone(g, iconRect, _blinkOn ? color : Color.FromArgb(70, color));
                break;
            case AppStatus.Okunuyor:
                DrawSpeaker(g, iconRect, color);
                break;
            case AppStatus.Duraklatildi:
                DrawPause(g, iconRect, color);
                break;
            default:
                using (var dot = new SolidBrush(color))
                    g.FillEllipse(dot, iconRect.X + 5, iconRect.Y + 5, iconSize - 10, iconSize - 10);
                break;
        }

        var textRect = new Rectangle(iconRect.Right + 10, 0, Width - iconRect.Right - 18, Height);
        TextRenderer.DrawText(g, text, Font, textRect, Color.White,
            TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
    }

    // Mikrofon: kapsül gövde, alt yay, sap ve taban
    private static void DrawMicrophone(Graphics g, Rectangle r, Color c)
    {
        using var brush = new SolidBrush(c);
        using var pen = new Pen(c, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round };

        var body = new Rectangle(r.X + r.Width / 2 - r.Width / 5, r.Y, r.Width * 2 / 5, r.Height * 3 / 5);
        using (var bodyPath = Theme.RoundedRect(body, body.Width / 2))
            g.FillPath(brush, bodyPath);

        var arc = new Rectangle(r.X + r.Width / 6, r.Y + r.Height / 4, r.Width * 2 / 3, r.Height / 2);
        g.DrawArc(pen, arc, 0, 180);

        int cx = r.X + r.Width / 2;
        g.DrawLine(pen, cx, arc.Bottom, cx, r.Bottom - 1);
        g.DrawLine(pen, cx - r.Width / 5, r.Bottom - 1, cx + r.Width / 5, r.Bottom - 1);
    }

    // Hoparlör: gövde ve iki ses dalgası
    private static void DrawSpeaker(Graphics g, Rectangle r, Color c)
    {
        using var brush = new SolidBrush(c);
        using var pen = new Pen(c, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round };

        int midY = r.Y + r.Height / 2;
        var cone = new[]
        {
            new Point(r.X, midY - r.Height / 6),
            new Point(r.X + r.Width / 4, midY - r.Height / 6),
            new Point(r.X + r.Width / 2, r.Y + 2),
            new Point(r.X + r.Width / 2, r.Bottom - 2),
            new Point(r.X + r.Width / 4, midY + r.Height / 6),
            new Point(r.X, midY + r.Height / 6)
        };
        g.FillPolygon(brush, cone);

        g.DrawArc(pen, r.X + r.Width / 3, midY - r.Height / 4, r.Width / 2, r.Height / 2, -50, 100);
        g.DrawArc(pen, r.X + r.Width / 6, midY - r.Height * 2 / 5, r.Width * 5 / 6, r.Height * 4 / 5, -50, 100);
    }

    // Duraklatma: iki dikey çubuk
    private static void DrawPause(Graphics g, Rectangle r, Color c)
    {
        using var brush = new SolidBrush(c);
        int barWidth = Math.Max(3, r.Width / 4);
        g.FillRectangle(brush, r.X + r.Width / 2 - barWidth - 2, r.Y + 2, barWidth, r.Height - 4);
        g.FillRectangle(brush, r.X + r.Width / 2 + 2, r.Y + 2, barWidth, r.Height - 4);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _blinkTimer.Stop();
            _blinkTimer.Dispose();
        }

        base.Dispose(disposing);
    }
}
