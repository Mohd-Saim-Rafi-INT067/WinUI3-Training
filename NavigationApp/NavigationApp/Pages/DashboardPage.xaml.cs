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
    public sealed partial class DashboardPage : Page
    {
        private string _username = "";
        public DashboardPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is string u)
            {
                _username = u;

            }
            ShowDashboardHome();
        }

        private void MainNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ShowSettings();
                return;
            }
            if (args.SelectedItem is NavigationViewItem item)
            {
                string tag = item.Tag?.ToString() ?? "";
                if (tag == "dashboard")
                {
                    ShowDashboardHome();
                }

                else if (tag == "logout")
                {
                    Logout();
                }
            }

        }
        private void ShowDashboardHome()
        {
            MainNav.SelectedItem = MainNav.MenuItems[0];
            string displayName = GetNameFromEmail(_username);
            ContentFrame.Navigate(typeof(DashboardHomePage),displayName);
        }
        private void ShowSettings()
        {
            ContentFrame.Navigate(typeof(SettingPage),_username);
        }
        private void Logout()
        {
            Frame.Navigate(typeof(LoginPage));
        }
        private static string GetNameFromEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return "";
            }
            int at = email.IndexOf('@');
            if (at <= 0)
            {
                return email.Trim();
            }
            return email.Substring(0, at).Trim();
        }

    }
}