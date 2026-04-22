using Assignment_Product_Manager.Services.Interfaces;
using Assignment_Product_Manager.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Assignment_Product_Manager.Views;

namespace Assignment_Product_Manager.ViewModels
{
    public partial class SignUpViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty] private string _fullName = string.Empty;
        [ObservableProperty] private string _username = string.Empty;
        [ObservableProperty] private string _email = string.Empty;
        [ObservableProperty] private string _phoneNumber = string.Empty;
        [ObservableProperty] private string _password = string.Empty;
        [ObservableProperty] private string _confirmPassword = string.Empty;

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
                var (success, message) = await _authService.RegisterAsync(
                    Username, Password, Email, FullName, PhoneNumber);

                if (success)
                {
                    SetSuccess(message);
                    await Task.Delay(1500); // Let user read success msg
                    GoToLogin();
                }
                else
                    SetError(message);
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
