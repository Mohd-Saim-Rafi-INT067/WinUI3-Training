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
    public sealed partial class SignUpPage : Page
    {
        private SignUpViewModel _viewModel = null!;

        public SignUpPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            _viewModel = new SignUpViewModel();
            _viewModel.NavigateToLogin += OnNavigateToLogin;
            _viewModel.RegistrationSuccess += OnRegistrationSuccess;

            DataContext = _viewModel;

            PasswordBox.PasswordChanged += (s, args) =>
            {
                _viewModel.Password = PasswordBox.Password;
            };

            ConfirmPasswordBox.PasswordChanged += (s, args) =>
            {
                _viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
            };
        }

        private void OnNavigateToLogin()
        {
            Frame?.Navigate(typeof(LoginPage));
        }

        private void OnRegistrationSuccess()
        {
            // Optional: Show success message
        }
    }
}
