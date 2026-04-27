using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.Foundation.Collections;
using Windows.UI;

namespace Assignment_ProductManager_WithoutBinding.Controls;

public sealed partial class StatsCard : UserControl
{
    public StatsCard()
    {
        this.InitializeComponent();
    }

  
    public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register(nameof(Label), typeof(string),
            typeof(StatsCard), new PropertyMetadata(string.Empty,
                (d, e) => ((StatsCard)d).LabelText.Text = (string)e.NewValue));

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

   
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string),
            typeof(StatsCard), new PropertyMetadata(string.Empty, (d, e) => ((StatsCard)d).UpdateValueDisplay()));

    public string Value
    {
        get => (string)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    
    public static readonly DependencyProperty IconGlyphProperty =
        DependencyProperty.Register(nameof(IconGlyph), typeof(string),
            typeof(StatsCard), new PropertyMetadata(string.Empty,
                (d, e) => ((StatsCard)d).IconElement.Glyph = (string)e.NewValue));

    public string IconGlyph
    {
        get => (string)GetValue(IconGlyphProperty);
        set => SetValue(IconGlyphProperty, value);
    }

    
    public static readonly DependencyProperty IconBackgroundProperty =
        DependencyProperty.Register(nameof(IconBackground), typeof(Brush),
            typeof(StatsCard), new PropertyMetadata(null, OnIconBackgroundChanged));

    public Brush IconBackground
    {
        get => (Brush)GetValue(IconBackgroundProperty);
        set => SetValue(IconBackgroundProperty, value);
    }

    private static void OnIconBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var card = (StatsCard)d;
        card.IconBorder.Background = e.NewValue as Brush;
    }


    public static readonly DependencyProperty ValuePrefixProperty =
    DependencyProperty.Register(nameof(ValuePrefix), typeof(string),
        typeof(StatsCard), new PropertyMetadata(string.Empty,
            (d, e) => ((StatsCard)d).UpdateValueDisplay()));

    public string ValuePrefix
    {
        get => (string)GetValue(ValuePrefixProperty);
        set => SetValue(ValuePrefixProperty, value);
    }


    private void UpdateValueDisplay()
    {
        ValueText.Text = string.IsNullOrEmpty(ValuePrefix)
            ? Value
            : ValuePrefix + Value;
    }

}
