using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Assignment_Product_Manager.Controls
{
    public sealed partial class AppTextBox : UserControl
    {
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(AppTextBox),
                new PropertyMetadata("", (d, e) => ((AppTextBox)d).LabelBlock.Text = (string)e.NewValue));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(AppTextBox),
                new PropertyMetadata("", (d, e) => {
                    var tb = (AppTextBox)d;
                    if (tb.InputBox.Text != (string)e.NewValue) tb.InputBox.Text = (string)e.NewValue ?? "";
                }));

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(AppTextBox),
                new PropertyMetadata("", (d, e) => ((AppTextBox)d).InputBox.PlaceholderText = (string)e.NewValue));

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register(nameof(ErrorMessage), typeof(string), typeof(AppTextBox),
                new PropertyMetadata("", (d, e) =>
                {
                    var tb = (AppTextBox)d;
                    var msg = (string)e.NewValue;
                    tb.ErrorBlock.Text = msg;
                    tb.ErrorBlock.Visibility = string.IsNullOrEmpty(msg) ? Visibility.Collapsed : Visibility.Visible;
                    tb.InputBox.BorderBrush = string.IsNullOrEmpty(msg)
                        ? (SolidColorBrush)Application.Current.Resources["BorderBrush"]
                        : (SolidColorBrush)Application.Current.Resources["BrandDangerBrush"];
                }));

        public static readonly DependencyProperty IsMultilineProperty =
            DependencyProperty.Register(nameof(IsMultiline), typeof(bool), typeof(AppTextBox),
                new PropertyMetadata(false, (d, e) =>
                {
                    var tb = (AppTextBox)d;
                    if ((bool)e.NewValue)
                    {
                        tb.InputBox.AcceptsReturn = true;
                        tb.InputBox.Height = double.NaN;
                        tb.InputBox.MinHeight = 88;
                        tb.InputBox.TextWrapping = Microsoft.UI.Xaml.TextWrapping.Wrap;
                    }
                }));

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
        public string ErrorMessage { get => (string)GetValue(ErrorMessageProperty); set => SetValue(ErrorMessageProperty, value); }
        public bool IsMultiline { get => (bool)GetValue(IsMultilineProperty); set => SetValue(IsMultilineProperty, value); }

        public event TextChangedEventHandler? TextChanged;

        public AppTextBox() => this.InitializeComponent();

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = InputBox.Text;
            TextChanged?.Invoke(sender, e);
        }

        private void InputBox_GotFocus(object sender, RoutedEventArgs e)
            => InputBox.BorderBrush = (SolidColorBrush)Application.Current.Resources["BrandPrimaryBrush"];

        private void InputBox_LostFocus(object sender, RoutedEventArgs e)
            => InputBox.BorderBrush = string.IsNullOrEmpty(ErrorMessage)
                ? (SolidColorBrush)Application.Current.Resources["BorderBrush"]
                : (SolidColorBrush)Application.Current.Resources["BrandDangerBrush"];
    }
}
