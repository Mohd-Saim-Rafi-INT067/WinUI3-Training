using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Assignment_Product_Manager.Controls;

public sealed partial class AuthDivider : UserControl
{
    public AuthDivider()
    {
        this.InitializeComponent();
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string),
            typeof(AuthDivider), new PropertyMetadata("   OR   ",
                (d, e) => ((AuthDivider)d).DividerLabel.Text = (string)e.NewValue));

    
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
