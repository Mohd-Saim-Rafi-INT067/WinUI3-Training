using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using NavigationAppWithMVVM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NavigationAppWithMVVM.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        //[ObservableProperty] = automatic property generation

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string emailError = "";

        [ObservableProperty]
        private string passwordError = "";

        [ObservableProperty]
        private string loginError = "";

        [ObservableProperty]
        private bool isEmailErrorVisible = false;

        [ObservableProperty]
        private bool isPasswordErrorVisible = false;

        [ObservableProperty]
        private bool isLoginErrorVisible = false;

        [ObservableProperty]
        private bool isLoading = false;

        
        public event Action<string>? NavigateToDashboard;
        public event Action? NavigateToSignUp;

        [RelayCommand]
        private void OnLogin()
        {
            ClearErrors();

            string email = Email.Trim();
            string password = Password;

            if (!ValidateInputs(email, password))
            {
                return;
            }

            if (!UserStore.ValidateLogin(email, password, out var user))
            {
                ShowLoginError("User not found or password is incorrect");
                return;
            }

            NavigateToDashboard?.Invoke(user!.Email);
        }

        [RelayCommand]
        private void OnSignUp()
        {
            NavigateToSignUp?.Invoke();
        }
        private bool ValidateInputs(string email, string password)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(email))
            {
                EmailError = "Email is required.";
                IsEmailErrorVisible = true;
                isValid = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                EmailError = "Enter a valid email (example: name@example.com).";
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

            return isValid;
        }

        private void ClearErrors()
        {
            IsEmailErrorVisible = false;
            IsPasswordErrorVisible = false;
            IsLoginErrorVisible = false;
        }

        private void ShowLoginError(string message)
        {
            LoginError = message;
            IsLoginErrorVisible = true;
        }
    }
}
