using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using NavigationApp.Services;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ClearErrors();

            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (!ValidateLoginInputs(email, password))
            {
                return;
            }

            if (!UserStore.ValidateLogin(email, password, out var user))
            {
                ShowLoginError("User not found or password is incorrect");
                return;
            }

            NavigateToDashboard(user!.Email);

        }

        private void SignUpLink_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUpPage));
        }
        private void NavigateToDashboard(string email)
        {
            Frame.Navigate(typeof(DashboardPage), email);
        }
        private bool ValidateLoginInputs(string email, string password)
        {
            bool flag = true;

            if (string.IsNullOrWhiteSpace(email))
            {
                EmailError.Text = "Email is required.";
                EmailError.Visibility = Visibility.Visible;
                flag = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                EmailError.Text = "Enter a valid email (example: name@example.com).";
                EmailError.Visibility = Visibility.Visible;
                flag = false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordError.Text = "Password is required.";
                PasswordError.Visibility = Visibility.Visible;
                flag = false;
            }
            else if (password.Length < 6)
            {
                PasswordError.Text = "Password must be at least 6 characters.";
                PasswordError.Visibility = Visibility.Visible;
                flag = false;
            }

            return flag;
        }

        private void ClearErrors()
        {
            EmailError.Visibility = Visibility.Collapsed;
            PasswordError.Visibility = Visibility.Collapsed;
            LoginError.Visibility = Visibility.Collapsed;
        }

        private void ShowLoginError(string message)
        {
            LoginError.Text = message;
            LoginError.Visibility = Visibility.Visible;
        }

    }
}
