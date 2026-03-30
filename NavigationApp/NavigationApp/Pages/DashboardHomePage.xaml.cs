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

namespace NavigationApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DashboardHomePage : Page
    {
        private readonly List<string> _allItems = new()
        {
            "Important: Pay bills",
            "Normal: Buy groceries",
            "Important: Finish assignment",
            "Normal: Clean desk",
            "Normal: Read documentation"
        };
        public DashboardHomePage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string email)
            {
                WelcomeText.Text = $"Welcome, {email}!";
            }
            CategoryCombo.SelectedIndex = 0;
            LoadItems("All");
        }
        private void CategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryCombo.SelectedItem is ComboBoxItem item)
            {
                string selected = item.Content?.ToString() ?? "All";
                LoadItems(selected);
            }
        }
        private void LoadItems(string category)
        {
            ItemsListView.Items.Clear();
            foreach (string it in _allItems)
            {
                if (category == "All")
                {
                    ItemsListView.Items.Add(it);
                }
                else if (category == "Important" && it.StartsWith("Important:"))
                {
                    ItemsListView.Items.Add(it);
                }
                else if (category == "Normal" && it.StartsWith("Normal:"))
                {
                    ItemsListView.Items.Add(it);
                }
            }
            StatusText.Text = $"Showing {ItemsListView.Items.Count} item(s) for: {category}";
        }
    }
}
