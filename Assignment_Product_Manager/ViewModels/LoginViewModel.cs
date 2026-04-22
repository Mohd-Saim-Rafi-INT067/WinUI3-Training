using Assignment_Product_Manager.Services.Interfaces;
using Assignment_Product_Manager.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Assignment_Product_Manager.Views;

namespace Assignment_Product_Manager.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty] private string _username = string.Empty;
        [ObservableProperty] private string _password = string.Empty;

        public LoginViewModel(IAuthService authService) => _authService = authService;

        [RelayCommand]
        private async Task LoginAsync()
        {
            ClearMessages();
            if (string.IsNullOrWhiteSpace(Username)) { SetError("Username is required."); return; }
            if (string.IsNullOrWhiteSpace(Password)) { SetError("Password is required."); return; }

            IsBusy = true;
            try
            {
                var (success, message, _) = await _authService.LoginAsync(Username, Password);
                if (success)
                {
                    if (App.MainWindow.Content is Microsoft.UI.Xaml.Controls.Frame frame)
                        frame.Navigate(typeof(NavigationShellView));
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
        private void GoToSignUp()
        {
            if (App.MainWindow.Content is Microsoft.UI.Xaml.Controls.Frame frame)
                frame.Navigate(typeof(SignUpView));
        }
    }
}
