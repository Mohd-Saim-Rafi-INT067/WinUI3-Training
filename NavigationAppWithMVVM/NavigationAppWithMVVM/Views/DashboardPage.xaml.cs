using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NavigationAppWithMVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NavigationAppWithMVVM.Views
{
    public sealed partial class DashboardPage : Page
    {
        private DashboardViewModel _viewModel = null!;

        public DashboardPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _viewModel = new DashboardViewModel();
            _viewModel.ShowDashboardHome += OnShowDashboardHome;
            _viewModel.ShowSettings += OnShowSettings;
            _viewModel.NavigateToLogin += OnNavigateToLogin;

            DataContext = _viewModel;

            if (e.Parameter is string username)
            {
                _viewModel.Initialize(username);
            }
        }

        private void MainNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                OnShowSettings();
                return;
            }

            if (args.SelectedItem is NavigationViewItem item)
            {
                string tag = item.Tag?.ToString() ?? "";
                if (tag == "dashboard")
                {
                    OnShowDashboardHome();
                }
                else if (tag == "logout")
                {
                    OnNavigateToLogin();
                }
            }
        }

        private void OnShowDashboardHome()
        {
            MainNav.SelectedItem = MainNav.MenuItems[0];
            ContentFrame.Navigate(typeof(DashboardHomePage), _viewModel.Username);
        }

        private void OnShowSettings()
        {
            ContentFrame.Navigate(typeof(SettingPage), _viewModel.Username);
        }

        private void OnNavigateToLogin()
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
