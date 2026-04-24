using Assignment_ProductManager_WithoutBinding.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace Assignment_ProductManager_WithoutBinding.Views
{
    public sealed partial class LoginView : Page
    {
        public LoginViewModel ViewModel { get; }

        public LoginView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<LoginViewModel>();

            // Push user input into ViewModel
            UsernameBox.TextChanged += (s, e) => ViewModel.Username = UsernameBox.InputBox.Text;
            PasswordBox.PwdBox.PasswordChanged += (s, e) => ViewModel.Password = PasswordBox.PwdBox.Password;

            // Wire button commands
            LoginBtn.Click += async (s, e) => await ViewModel.LoginCommand.ExecuteAsync(null);
            GoToSignUpBtn.Click += (s, e) => ViewModel.GoToSignUpCommand.Execute(null);

            // Reflect ViewModel state changes into UI
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.ErrorMessage):
                    ErrorBanner.Message = ViewModel.ErrorMessage;
                    break;
                case nameof(ViewModel.IsBusy):
                    LoginBtn.IsLoading = ViewModel.IsBusy;
                    break;
            }
        }
    }
}
