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
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using NavigationApp.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NavigationApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            ClearErrors();

            string fullName = FullNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirm = ConfirmPasswordBox.Password;

            if (!ValidateSignUpInputs(fullName, email, password, confirm))
                return;

            var user = new User
            {
                Fullname = fullName,
                Email = email,
                Password = password
            };

            bool ok = UserStore.RegisterUser(user, out string error);

            if (!ok)
            {
                ShowSignUpError(error);
                return;
            }

            // Success -> back to login
            Frame.Navigate(typeof(LoginPage));
        }

        private void BackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
        private bool ValidateSignUpInputs(string fullName, string email, string password, string confirm)
        {
            bool flag = true;

            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3)
            {
                FullNameError.Text = "Full name must be at least 3 characters.";
                FullNameError.Visibility = Visibility.Visible;
                flag = false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                EmailError.Text = "Email is required.";
                EmailError.Visibility = Visibility.Visible;
                flag = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                EmailError.Text = "Enter a valid email.";
                EmailError.Visibility = Visibility.Visible;
                flag = false;
            }
            else if (UserStore.EmailExists(email))
            {
                EmailError.Text = "Email already registered.";
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
            else if (!ContainsLetterAndNumber(password))
            {
                PasswordError.Text = "Password must contain at least 1 letter and 1 number.";
                PasswordError.Visibility = Visibility.Visible;
                flag = false;
            }

            if (string.IsNullOrWhiteSpace(confirm))
            {
                ConfirmPasswordError.Text = "Confirm your password.";
                ConfirmPasswordError.Visibility = Visibility.Visible;
                flag = false;
            }
            else if (confirm != password)
            {
                ConfirmPasswordError.Text = "Passwords do not match.";
                ConfirmPasswordError.Visibility = Visibility.Visible;
                flag = false;
            }

            return flag;
        }
        private bool ContainsLetterAndNumber(string value)
        {
            bool hasLetter = false;
            bool hasNumber = false;

            foreach (char c in value)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasNumber = true;
            }

            return hasLetter && hasNumber;
        }

        private void ClearErrors()
        {
            FullNameError.Visibility = Visibility.Collapsed;
            EmailError.Visibility = Visibility.Collapsed;
            PasswordError.Visibility = Visibility.Collapsed;
            ConfirmPasswordError.Visibility = Visibility.Collapsed;
            SignUpError.Visibility = Visibility.Collapsed;
        }

        private void ShowSignUpError(string message)
        {
            SignUpError.Text = message;
            SignUpError.Visibility = Visibility.Visible;
        }
    }
}
