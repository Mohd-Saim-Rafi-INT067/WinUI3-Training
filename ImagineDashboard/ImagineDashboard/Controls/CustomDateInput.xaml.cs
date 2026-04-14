using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ImagineDashboard.Controls
{
    public sealed partial class CustomDateInput : UserControl
    {
        public CustomDateInput()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(CustomDateInput),
                new PropertyMetadata(string.Empty));

        // date + time + timezone - Date can either have a value or be null
        public DateTimeOffset? Date
        {
            get => (DateTimeOffset?)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register(nameof(Date), typeof(DateTimeOffset?), typeof(CustomDateInput),
                new PropertyMetadata(null));
    }
}