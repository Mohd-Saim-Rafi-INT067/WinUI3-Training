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

namespace ImagineDashboard.Controls
{
    public sealed partial class CustomNumberInput : UserControl
    {
        public CustomNumberInput()
        {
            this.InitializeComponent();
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(CustomNumberInput), new PropertyMetadata(""));

        public double InputNumber
        {
            get { return (double)GetValue(InputNumberProperty); }
            set { SetValue(InputNumberProperty, value); }
        }

        public static readonly DependencyProperty InputNumberProperty =
            DependencyProperty.Register("InputNumber", typeof(double), typeof(CustomNumberInput), new PropertyMetadata(0.0));

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(CustomNumberInput), new PropertyMetadata(""));

        public string ErrorMessage
        {
            get { return (string)GetValue(ErrorMessageProperty); }
            set { SetValue(ErrorMessageProperty, value); }
        }

        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(CustomNumberInput), new PropertyMetadata(""));

        public Visibility ShowError
        {
            get { return (Visibility)GetValue(ShowErrorProperty); }
            set { SetValue(ShowErrorProperty, value); }
        }

        public static readonly DependencyProperty ShowErrorProperty =
            DependencyProperty.Register("ShowError", typeof(Visibility), typeof(CustomNumberInput), new PropertyMetadata(Visibility.Collapsed));

        public bool _IsEnabled
        {
            get { return (bool)GetValue(_IsEnabledProperty); }
            set { SetValue(_IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty _IsEnabledProperty =
            DependencyProperty.Register("_IsEnabled", typeof(bool), typeof(CustomNumberInput), new PropertyMetadata(false));

    }
}
