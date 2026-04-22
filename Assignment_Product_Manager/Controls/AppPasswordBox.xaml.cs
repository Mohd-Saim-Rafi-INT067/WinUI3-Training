using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;


namespace Assignment_Product_Manager.Controls
{
    public sealed partial class AppPasswordBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(AppPasswordBox),
                new PropertyMetadata("", (d, e) => ((AppPasswordBox)d).LabelBlock.Text = (string)e.NewValue));

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register(nameof(Password), typeof(string), typeof(AppPasswordBox),
                new PropertyMetadata(""));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(AppPasswordBox),
                new PropertyMetadata("", (d, e) => ((AppPasswordBox)d).PwdBox.PlaceholderText = (string)e.NewValue));

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(AppPasswordBox),
                new PropertyMetadata("", (d, e) =>
                {
                    var pb = (AppPasswordBox)d;
                    var msg = (string)e.NewValue;
                    pb.ErrorBlock.Text = msg;
                    pb.ErrorBlock.Visibility = string.IsNullOrEmpty(msg) ? Visibility.Collapsed : Visibility.Visible;
                    pb.PwdBox.BorderBrush = string.IsNullOrEmpty(msg)
                        ? (SolidColorBrush)Application.Current.Resources["BorderBrush"]
                        : (SolidColorBrush)Application.Current.Resources["BrandDangerBrush"];
                }));

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
        public string Password { get => (string)GetValue(PasswordProperty); set => SetValue(PasswordProperty, value); }
        public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
        public string ErrorMessage { get => (string)GetValue(ErrorMessageProperty); set => SetValue(ErrorMessageProperty, value); }

        public new event KeyEventHandler? KeyDown;

        public AppPasswordBox()
        {
            this.InitializeComponent();
            PwdBox.KeyDown += OnInnerKeyDown;
            Unloaded += OnUnloaded;
        }
        private void OnInnerKeyDown(object sender, KeyRoutedEventArgs e)
            => KeyDown?.Invoke(this, e);

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            PwdBox.KeyDown -= OnInnerKeyDown;
            Unloaded -= OnUnloaded;
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
            => Password = PwdBox.Password;

        private void PwdBox_GotFocus(object sender, RoutedEventArgs e)
            => PwdBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["BrandPrimaryBrush"];

        private void PwdBox_LostFocus(object sender, RoutedEventArgs e)
            => PwdBox.BorderBrush = string.IsNullOrEmpty(ErrorMessage)
                ? (SolidColorBrush)Application.Current.Resources["BorderBrush"]
                : (SolidColorBrush)Application.Current.Resources["BrandDangerBrush"];
    }
}
