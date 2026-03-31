using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using NavigationAppWithMVVM.Commands;
using NavigationAppWithMVVM.Models;
using NavigationAppWithMVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NavigationAppWithMVVM.ViewModels
{
    public partial class SignUpViewModel : ObservableObject
    {
        [ObservableProperty]
        private string fullName = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string confirmPassword = "";

        [ObservableProperty]
        private string fullNameError = "";

        [ObservableProperty]
        private string emailError = "";

        [ObservableProperty]
        private string passwordError = "";

        [ObservableProperty]
        private string confirmPasswordError = "";

        [ObservableProperty]
        private string signUpError = "";

        [ObservableProperty]
        private bool isFullNameErrorVisible = false;

        [ObservableProperty]
        private bool isEmailErrorVisible = false;

        [ObservableProperty]
        private bool isPasswordErrorVisible = false;

        [ObservableProperty]
        private bool isConfirmPasswordErrorVisible = false;

        [ObservableProperty]
        private bool isSignUpErrorVisible = false;

        [ObservableProperty]
        private bool isLoading = false;

        public event Action? NavigateToLogin;
        public event Action? RegistrationSuccess;

        [RelayCommand]
        private void OnCreateAccount()
        {
            ClearErrors();

            string fullName = FullName.Trim();
            string email = Email.Trim();
            string password = Password;
            string confirm = ConfirmPassword;

            if (!ValidateInputs(fullName, email, password, confirm))
                return;

            var user = new User
            {
                Fullname = fullName,
                Email = email,
                Password = password
            };

            if (!UserStore.RegisterUser(user, out string error))
            {
                ShowSignUpError(error);
                return;
            }

            RegistrationSuccess?.Invoke();
            NavigateToLogin?.Invoke();
        }

        [RelayCommand]
        private void OnBackToLogin()
        {
            NavigateToLogin?.Invoke();
        }

        private bool ValidateInputs(string fullName, string email, string password, string confirm)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(fullName) || fullName.Length < 3)
            {
                FullNameError = "Full name must be at least 3 characters.";
                IsFullNameErrorVisible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                EmailError = "Email is required.";
                IsEmailErrorVisible = true;
                isValid = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                EmailError = "Enter a valid email.";
                IsEmailErrorVisible = true;
                isValid = false;
            }
            else if (UserStore.EmailExists(email))
            {
                EmailError = "Email already registered.";
                IsEmailErrorVisible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                PasswordError = "Password is required.";
                IsPasswordErrorVisible = true;
                isValid = false;
            }
            else if (password.Length < 6)
            {
                PasswordError = "Password must be at least 6 characters.";
                IsPasswordErrorVisible = true;
                isValid = false;
            }
            else if (!ContainsLetterAndNumber(password))
            {
                PasswordError = "Password must contain at least 1 letter and 1 number.";
                IsPasswordErrorVisible = true;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(confirm))
            {
                ConfirmPasswordError = "Confirm your password.";
                IsConfirmPasswordErrorVisible = true;
                isValid = false;
            }
            else if (confirm != password)
            {
                ConfirmPasswordError = "Passwords do not match.";
                IsConfirmPasswordErrorVisible = true;
                isValid = false;
            }

            return isValid;
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
            IsFullNameErrorVisible = false;
            IsEmailErrorVisible = false;
            IsPasswordErrorVisible = false;
            IsConfirmPasswordErrorVisible = false;
            IsSignUpErrorVisible = false;
        }

        private void ShowSignUpError(string message)
        {
            SignUpError = message;
            IsSignUpErrorVisible = true;
        }
    }
}
