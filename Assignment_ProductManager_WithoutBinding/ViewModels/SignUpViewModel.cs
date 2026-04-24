using Assignment_ProductManager_WithoutBinding.Services.Interfaces;
using Assignment_ProductManager_WithoutBinding.ViewModels.Base;
using Assignment_ProductManager_WithoutBinding.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using Assignment_ProductManager_WithoutBinding.DTOs;

namespace Assignment_ProductManager_WithoutBinding.ViewModels
{
    public partial class SignUpViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        // Plain auto-properties — written to by code-behind events, never pushed back to the view.
        public string FullName = string.Empty;
        public string Username = string.Empty;
        public string Email = string.Empty;
        public string PhoneNumber = string.Empty;
        public string Password = string.Empty;
        public string ConfirmPassword = string.Empty;

        public SignUpViewModel(IAuthService authService) => _authService = authService;

        [RelayCommand]
        private async Task RegisterAsync()
        {
            ClearMessages();


            if (string.IsNullOrWhiteSpace(FullName)) { SetError("Full name is required."); return; }
            if (string.IsNullOrWhiteSpace(Username)) { SetError("Username is required."); return; }
            if (string.IsNullOrWhiteSpace(Email)) { SetError("Email is required."); return; }
            if (string.IsNullOrWhiteSpace(Password)) { SetError("Password is required."); return; }
            if (Password != ConfirmPassword) { SetError("Passwords do not match."); return; }

            IsBusy = true;
            try
            {
                var request = new RegisterRequest
                {
                    Username = Username,
                    Password = Password,
                    Email = Email,
                    FullName = FullName,
                    PhoneNumber = PhoneNumber
                };
                var result = await _authService.RegisterAsync(request);

                if (result.Success)
                {
                    SetSuccess(result.Message);
                    await Task.Delay(1500); // Let user read success msg
                    GoToLogin();
                }
                else
                    SetError(result.Message);
            }
            catch (Exception ex)
            {
                SetError($"Connection error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private void GoToLogin()
        {
            if (App.MainWindow.Content is Microsoft.UI.Xaml.Controls.Frame frame)
                frame.Navigate(typeof(LoginView));
        }
    }
}
