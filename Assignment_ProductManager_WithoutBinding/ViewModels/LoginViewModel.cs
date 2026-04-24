using Assignment_ProductManager_WithoutBinding.Services.Interfaces;
using Assignment_ProductManager_WithoutBinding.ViewModels.Base;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Assignment_ProductManager_WithoutBinding.Views;

namespace Assignment_ProductManager_WithoutBinding.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        // Plain auto-properties — no PropertyChanged needed here because
        // these are written to by the code-behind (input to VM), never read back by the view.
        public string Username = string.Empty;
        public string Password = string.Empty;

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
                var result = await _authService.LoginAsync(Username, Password);
                if (result.Success)
                {
                    if (App.MainWindow.Content is Microsoft.UI.Xaml.Controls.Frame frame)
                        frame.Navigate(typeof(NavigationShellView));
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
        private void GoToSignUp()
        {
            if (App.MainWindow.Content is Microsoft.UI.Xaml.Controls.Frame frame)
                frame.Navigate(typeof(SignUpView));
        }
    }
}
