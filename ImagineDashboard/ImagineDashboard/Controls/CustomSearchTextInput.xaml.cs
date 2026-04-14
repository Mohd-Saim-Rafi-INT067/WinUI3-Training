using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
namespace ImagineDashboard.Controls
{
    public sealed partial class CustomSearchTextInput : UserControl
    {
        // Hardcoded lookup list shown in the dialog 
        private static readonly List<string> _lookupItems = new()
        {
            "US",
            "CT",
            "EKG",
            "MRI",
            "XR",
            "PT",
            "OT"
        };
        public CustomSearchTextInput()
        {
            InitializeComponent();
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(CustomSearchTextInput),
                new PropertyMetadata(string.Empty));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(nameof(PlaceholderText), typeof(string), typeof(CustomSearchTextInput),
                new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(CustomSearchTextInput),
                new PropertyMetadata(string.Empty));



        // IsHighlighted : turns the border red when true 
        public bool IsHighlighted
        {
            get => (bool)GetValue(IsHighlightedProperty);
            set => SetValue(IsHighlightedProperty, value);
        }
        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register(nameof(IsHighlighted), typeof(bool),
                typeof(CustomSearchTextInput),
                new PropertyMetadata(false, OnIsHighlightedChanged));


        //runs whenever the dependency property changes
        private static void OnIsHighlightedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CustomSearchTextInput ctrl)
                ctrl.ApplyHighlight((bool)e.NewValue);
        }

        private void ApplyHighlight(bool highlight)
        {
            OutlineBorder.BorderBrush = highlight
                ? new SolidColorBrush(Colors.Red)
                : new SolidColorBrush(Windows.UI.Color.FromArgb(255, 195, 200, 201)); 
        }

        // Search icon click — opens ContentDialog with lookup list
        private async void SearchIconButton_Click(object sender, RoutedEventArgs e)
        {
            // Build the list view
            var listView = new ListView
            {
                SelectionMode = ListViewSelectionMode.Single,
                ItemsSource = _lookupItems,
                Height = 300,
            };

            var dialog = new ContentDialog
            {
                Title = $"Select value for \"{Label}\"",
                Content = listView,
                PrimaryButtonText = "Select",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = this.XamlRoot,
            };

            // Enable Primary button only when something is selected
            dialog.IsPrimaryButtonEnabled = false;
            listView.SelectionChanged += (s, _) =>
            {
                dialog.IsPrimaryButtonEnabled = listView.SelectedItem != null;
            };


            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary && listView.SelectedItem is string selected)
            {
                // Replace the chosen value into the field
                Text = selected;

                // Clear red highlight once a value is picked
                IsHighlighted = false;
            }
        }
    }
}