using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.UI;

namespace Assignment_Product_Manager.Controls;

public enum AlertType { Error, Success, Info }

public sealed partial class AlertBanner : UserControl
{
    public AlertBanner()
    {
        this.InitializeComponent();
    }


    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string),
            typeof(AlertBanner), new PropertyMetadata(string.Empty, OnMessageChanged));

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var banner = (AlertBanner)d;
        var msg = e.NewValue as string;
        banner.MessageText.Text = msg ?? string.Empty;
        banner.Visibility = string.IsNullOrEmpty(msg) ? Visibility.Collapsed : Visibility.Visible;
    }
    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register(nameof(Type), typeof(AlertType),
            typeof(AlertBanner), new PropertyMetadata(AlertType.Error, OnTypeChanged));

    public AlertType Type
    {
        get => (AlertType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((AlertBanner)d).ApplyStyle();

    private void ApplyStyle()
    {
        switch (Type)
        {
            case AlertType.Error:
                RootBorder.Background = (SolidColorBrush)Application.Current.Resources["AlertErrorBackground"];
                RootBorder.BorderBrush = (SolidColorBrush)Application.Current.Resources["AlertErrorBorder"];
                IconElement.Glyph = "\uE783";
                IconElement.Foreground = (SolidColorBrush)Application.Current.Resources["BrandDangerBrush"];
                MessageText.Foreground = (SolidColorBrush)Application.Current.Resources["BrandDangerBrush"];
                break;

            case AlertType.Success:
                RootBorder.Background = (SolidColorBrush)Application.Current.Resources["AlertSuccessBackground"];
                RootBorder.BorderBrush = (SolidColorBrush)Application.Current.Resources["AlertSuccessBorder"];
                IconElement.Glyph = "\uE73E";
                IconElement.Foreground = (SolidColorBrush)Application.Current.Resources["BrandSuccessBrush"];
                MessageText.Foreground = (SolidColorBrush)Application.Current.Resources["BrandSuccessBrush"];
                break;

            case AlertType.Info:
                RootBorder.Background = (SolidColorBrush)Application.Current.Resources["AlertInfoBackground"];
                RootBorder.BorderBrush = (SolidColorBrush)Application.Current.Resources["AlertInfoBorder"];
                IconElement.Glyph = "\uE946";
                IconElement.Foreground = (SolidColorBrush)Application.Current.Resources["AlertInfoForeground"];
                MessageText.Foreground = (SolidColorBrush)Application.Current.Resources["AlertInfoForeground"];
                break;
        }
    }
}