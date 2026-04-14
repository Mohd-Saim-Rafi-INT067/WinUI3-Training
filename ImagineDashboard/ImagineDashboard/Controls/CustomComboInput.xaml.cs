using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections;
using System.Windows.Input;

namespace ImagineDashboard.Controls
{
    public sealed partial class CustomComboInput : UserControl
    {
        public CustomComboInput()
        {
            this.InitializeComponent();
        }


        // Label floating text on border
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(CustomComboInput), new PropertyMetadata(""));
        //propertyname, type, owner class, default value - empty string

        
        // Items to populate the dropdown menu
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CustomComboInput),
                //d->object whose property changed, e->event args with old/new value
                new PropertyMetadata(null, (d, e) =>
                {
                    var c = (CustomComboInput)d;
                    c.InnerComboBox.ItemsSource = e.NewValue as IEnumerable;
                }));

        // Selected index
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(CustomComboInput),
                new PropertyMetadata(0, (d, e) =>
                {
                    var c = (CustomComboInput)d;
                    c.InnerComboBox.SelectedIndex = (int)e.NewValue;
                }));

        // Selected item (two-way bindable) - actual selected object
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(CustomComboInput), new PropertyMetadata(null));

        private void InnerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = InnerComboBox.SelectedItem;
            SelectedIndex = InnerComboBox.SelectedIndex;
        }
    }
}