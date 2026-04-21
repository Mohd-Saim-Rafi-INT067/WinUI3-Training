using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Assignment_Product_Manager.Controls;


public sealed partial class AuthBrandPanel : UserControl
{
    public AuthBrandPanel()
    {
        this.InitializeComponent();
    }
    
    public static readonly DependencyProperty IconGlyphProperty =
        DependencyProperty.Register(nameof(IconGlyph), typeof(string),
            typeof(AuthBrandPanel), new PropertyMetadata("\uE7BF"));

    public string IconGlyph
    {
        get => (string)GetValue(IconGlyphProperty);
        set => SetValue(IconGlyphProperty, value);
    }

    public static readonly DependencyProperty HeadlineProperty =
        DependencyProperty.Register(nameof(Headline), typeof(string),
            typeof(AuthBrandPanel), new PropertyMetadata("Smart Product Manager"));

    public string Headline
    {
        get => (string)GetValue(HeadlineProperty);
        set => SetValue(HeadlineProperty, value);
    }

    public static readonly DependencyProperty SubtextProperty =
        DependencyProperty.Register(nameof(Subtext), typeof(string),
            typeof(AuthBrandPanel), new PropertyMetadata(string.Empty));

    public string Subtext
    {
        get => (string)GetValue(SubtextProperty);
        set => SetValue(SubtextProperty, value);
    }
 

}
