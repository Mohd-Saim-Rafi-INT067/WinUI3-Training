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
    public sealed partial class LoginPage : Page
    {
        private LoginViewModel _viewModel = null!;

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _viewModel = new LoginViewModel();
            _viewModel.NavigateToDashboard += OnNavigateToDashboard;
            _viewModel.NavigateToSignUp += OnNavigateToSignUp;

            DataContext = _viewModel;

            // Handle PasswordBox binding (PasswordBox doesn't support direct binding for security)
            PasswordBox.PasswordChanged += (s, args) =>
            {
                _viewModel.Password = PasswordBox.Password;
            };
        }

        private void OnNavigateToDashboard(string email)
        {
            Frame?.Navigate(typeof(DashboardPage), email);
        }

        private void OnNavigateToSignUp()
        {
            Frame?.Navigate(typeof(SignUpPage));
        }
    }
}
