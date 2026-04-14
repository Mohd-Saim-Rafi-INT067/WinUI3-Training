using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace ImagineDashboard.Controls
{
    public sealed partial class CustomTextInput : UserControl
    {
        public CustomTextInput()
        {
            this.InitializeComponent();
        }

        // Label - floating text
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(CustomTextInput),
                new PropertyMetadata(string.Empty));

        //Text equivalent of SelectedItem / value
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(CustomTextInput),
                new PropertyMetadata(string.Empty));

        // IsHighlighted — turns the border red when true 
        public bool IsHighlighted
        {
            get => (bool)GetValue(IsHighlightedProperty);
            set => SetValue(IsHighlightedProperty, value);
        }
        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(
                nameof(IsHighlighted), typeof(bool), typeof(CustomTextInput),
                new PropertyMetadata(false, OnIsHighlightedChanged));

        private static void OnIsHighlightedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomTextInput ctrl)
                ctrl.ApplyHighlight((bool)e.NewValue);
        }

        private void ApplyHighlight(bool highlight)
        {
            OutlineBorder.BorderBrush = highlight
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Windows.UI.Color.FromArgb(255, 195, 200, 201)); // #c3c8c9
        }
    }
}