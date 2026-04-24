using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Assignment_ProductManager_WithoutBinding.Controls;
public sealed partial class AuthBrandPanel : UserControl
{
    public AuthBrandPanel()
    {
        this.InitializeComponent();
    }

    public static readonly DependencyProperty IconGlyphProperty =
        DependencyProperty.Register(nameof(IconGlyph), typeof(string),
            typeof(AuthBrandPanel), new PropertyMetadata("\uE7BF", OnIconGlyphChanged));

    public string IconGlyph
    {
        get => (string)GetValue(IconGlyphProperty);
        set => SetValue(IconGlyphProperty, value);
    }

    private static void OnIconGlyphChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((AuthBrandPanel)d).IconElement.Glyph = (string)e.NewValue;


    public static readonly DependencyProperty HeadlineProperty =
        DependencyProperty.Register(nameof(Headline), typeof(string),
            typeof(AuthBrandPanel), new PropertyMetadata(string.Empty, OnHeadlineChanged));

    public string Headline
    {
        get => (string)GetValue(HeadlineProperty);
        set => SetValue(HeadlineProperty, value);
    }

    private static void OnHeadlineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((AuthBrandPanel)d).HeadlineText.Text = (string)e.NewValue;


    public static readonly DependencyProperty SubtextProperty =
        DependencyProperty.Register(nameof(Subtext), typeof(string),
            typeof(AuthBrandPanel), new PropertyMetadata(string.Empty, OnSubtextChanged));

    public string Subtext
    {
        get => (string)GetValue(SubtextProperty);
        set => SetValue(SubtextProperty, value);
    }

    private static void OnSubtextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((AuthBrandPanel)d).SubtextText.Text = (string)e.NewValue;


}
