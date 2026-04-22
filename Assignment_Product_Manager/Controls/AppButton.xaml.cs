using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;


namespace Assignment_Product_Manager.Controls
{
    public sealed partial class AppButton : UserControl
    {
        
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(AppButton),
                new PropertyMetadata("Button", (d, e) => ((AppButton)d).LabelText.Text = (string)e.NewValue));

        public static readonly DependencyProperty IconGlyphProperty =
            DependencyProperty.Register(nameof(IconGlyph), typeof(string), typeof(AppButton),
                new PropertyMetadata("", (d, e) =>
                {
                    var btn = (AppButton)d;
                    btn.IconElement.Glyph = (string)e.NewValue;
                    btn.IconElement.Visibility = string.IsNullOrEmpty((string)e.NewValue)
                        ? Visibility.Collapsed : Visibility.Visible;
                }));

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(nameof(IsLoading), typeof(bool), typeof(AppButton),
                new PropertyMetadata(false, (d, e) =>
                {
                    var btn = (AppButton)d;
                    bool loading = (bool)e.NewValue;
                    btn.LoadingRing.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
                    btn.LoadingRing.IsActive = loading;
                    btn.IconElement.Visibility = loading ? Visibility.Collapsed
                        : (string.IsNullOrEmpty(btn.IconGlyph) ? Visibility.Collapsed : Visibility.Visible);
                    btn.RootButton.IsEnabled = !loading;
                }));

        public static readonly DependencyProperty VariantProperty =
            DependencyProperty.Register(nameof(Variant), typeof(ButtonVariant), typeof(AppButton),
                new PropertyMetadata(ButtonVariant.Primary, (d, _) => ((AppButton)d).ApplyVariant()));

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(System.Windows.Input.ICommand),
                typeof(AppButton), new PropertyMetadata(null,
                    (d, e) => ((AppButton)d).RootButton.Command = (System.Windows.Input.ICommand?)e.NewValue));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object),
                typeof(AppButton), new PropertyMetadata(null,
                    (d, e) => ((AppButton)d).RootButton.CommandParameter = e.NewValue));

       
        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
        public string IconGlyph { get => (string)GetValue(IconGlyphProperty); set => SetValue(IconGlyphProperty, value); }
        public bool IsLoading { get => (bool)GetValue(IsLoadingProperty); set => SetValue(IsLoadingProperty, value); }
        public ButtonVariant Variant { get => (ButtonVariant)GetValue(VariantProperty); set => SetValue(VariantProperty, value); }
        public System.Windows.Input.ICommand? Command { get => (System.Windows.Input.ICommand?)GetValue(CommandProperty); set => SetValue(CommandProperty, value); }
        public object? CommandParameter { get => GetValue(CommandParameterProperty); set => SetValue(CommandParameterProperty, value); }

        public event RoutedEventHandler? Click;

        public AppButton()
        {
            this.InitializeComponent();
            ApplyVariant();
        }

        private void RootButton_Click(object sender, RoutedEventArgs e) => Click?.Invoke(this, e);
        //call all the methods which is sub to the click event

        private void ApplyVariant()
        {
            var res = Application.Current.Resources;
            RootButton.Background = Variant switch
            {
                ButtonVariant.Secondary => new SolidColorBrush(Microsoft.UI.Colors.Transparent),
                ButtonVariant.Danger => (SolidColorBrush)res["BrandDangerBrush"],
                ButtonVariant.Success => (SolidColorBrush)res["BrandSuccessBrush"],
                _ => (SolidColorBrush)res["BrandPrimaryBrush"],
            };
            RootButton.Foreground = Variant == ButtonVariant.Secondary
                ? (SolidColorBrush)res["BrandPrimaryBrush"]
                : new SolidColorBrush(Microsoft.UI.Colors.White);

            if (Variant == ButtonVariant.Secondary)
            {
                RootButton.BorderBrush = (SolidColorBrush)res["BrandPrimaryBrush"];
                RootButton.BorderThickness = new Thickness(1.5);
            }
            else
            {
                RootButton.BorderThickness = new Thickness(0);
            }
            LabelText.Text = Label;
        }
    }

    public enum ButtonVariant { Primary, Secondary, Danger, Success }
}
