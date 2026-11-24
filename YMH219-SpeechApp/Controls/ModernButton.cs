using System.ComponentModel;
using System.Drawing.Drawing2D;
using SpeechApp.UI;

namespace SpeechApp.Controls;

// Buton renk türleri
public enum ButtonVariant
{
    Secondary,
    Primary,
    Success,
    Danger,
    Ghost
}

// Köşeleri yuvarlatılmış, üzerine gelince renk değiştiren düz buton
public class ModernButton : Button
{
    private ButtonVariant _variant = ButtonVariant.Secondary;
    private bool _hover;
    private bool _pressed;

    public ModernButton()
    {
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        Cursor = Cursors.Hand;
    }

    [Category("Görünüm")]
    [DefaultValue(ButtonVariant.Secondary)]
    public ButtonVariant Variant
    {
        get => _variant;
        set
        {
            _variant = value;
            Invalidate();
        }
    }

    [Category("Görünüm")]
    [DefaultValue(8)]
    public int CornerRadius { get; set; } = 8;

    protected override void OnMouseEnter(EventArgs e)
    {
        _hover = true;
        Invalidate();
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        _hover = false;
        _pressed = false;
        Invalidate();
        base.OnMouseLeave(e);
    }

    protected override void OnMouseDown(MouseEventArgs mevent)
    {
        _pressed = true;
        Invalidate();
        base.OnMouseDown(mevent);
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
        _pressed = false;
        Invalidate();
        base.OnMouseUp(mevent);
    }

    protected override void OnEnabledChanged(EventArgs e)
    {
        _hover = false;
        _pressed = false;
        base.OnEnabledChanged(e);
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        var g = pevent.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent?.BackColor ?? Theme.Background);

        var (back, fore, border) = GetColors();
        var rect = new Rectangle(0, 0, Width - 1, Height - 1);
        using (var path = Theme.RoundedRect(rect, CornerRadius))
        {
            using var brush = new SolidBrush(back);
            g.FillPath(brush, path);
            if (border != Color.Empty)
            {
                using var pen = new Pen(border);
                g.DrawPath(pen, path);
            }
        }

        TextRenderer.DrawText(g, Text, Font, rect, fore,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
            TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);

        // Klavye ile odaklanıldığında ince bir çerçeve çizilir
        if (Focused && ShowFocusCues && Enabled)
        {
            var focusRect = Rectangle.Inflate(rect, -3, -3);
            using var focusPath = Theme.RoundedRect(focusRect, Math.Max(2, CornerRadius - 3));
            using var focusPen = new Pen(Color.FromArgb(120, fore)) { DashStyle = DashStyle.Dot };
            g.DrawPath(focusPen, focusPath);
        }
    }

    // Duruma (pasif / üzerinde / basılı) göre arka plan, yazı ve kenar rengi
    private (Color back, Color fore, Color border) GetColors()
    {
        if (!Enabled)
        {
            return _variant == ButtonVariant.Ghost
                ? (Parent?.BackColor ?? Theme.Header, Theme.DisabledText, Color.Empty)
                : (Theme.DisabledBack, Theme.DisabledText, Color.Empty);
        }

        float shade = _pressed ? 0.18f : _hover ? 0.09f : 0f;
        return _variant switch
        {
            ButtonVariant.Primary => (Theme.Darken(Theme.Primary, shade), Color.White, Color.Empty),
            ButtonVariant.Success => (Theme.Darken(Theme.Success, shade), Color.White, Color.Empty),
            ButtonVariant.Danger => (Theme.Darken(Theme.Danger, shade), Color.White, Color.Empty),
            ButtonVariant.Ghost => (_hover || _pressed ? Theme.HeaderHover : Parent?.BackColor ?? Theme.Header,
                                    Theme.TextOnDark, Color.Empty),
            _ => (Theme.Darken(Theme.Surface, shade * 0.6f), Theme.Text, Theme.Border)
        };
    }
}
