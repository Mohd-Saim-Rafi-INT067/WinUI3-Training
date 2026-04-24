using Assignment_ProductManager_WithoutBinding.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace Assignment_ProductManager_WithoutBinding.Views
{
    public sealed partial class SignUpView : Page
    {
        public SignUpViewModel ViewModel { get; }

        public SignUpView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<SignUpViewModel>();

            // Push user input into ViewModel
            FullNameBox.TextChanged += (s, e) => ViewModel.FullName = FullNameBox.InputBox.Text;
            UsernameBox.TextChanged += (s, e) => ViewModel.Username = UsernameBox.InputBox.Text;
            EmailBox.TextChanged += (s, e) => ViewModel.Email = EmailBox.InputBox.Text;
            PhoneBox.TextChanged += (s, e) => ViewModel.PhoneNumber = PhoneBox.InputBox.Text;
            PasswordBox.PwdBox.PasswordChanged += (s, e) => ViewModel.Password = PasswordBox.PwdBox.Password;
            ConfirmPasswordBox.PwdBox.PasswordChanged += (s, e) => ViewModel.ConfirmPassword = ConfirmPasswordBox.PwdBox.Password;

            // Wire button commands
            RegisterBtn.Click += async (s, e) => await ViewModel.RegisterCommand.ExecuteAsync(null);
            GoToLoginBtn.Click += (s, e) => ViewModel.GoToLoginCommand.Execute(null);

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
                case nameof(ViewModel.SuccessMessage):
                    SuccessBanner.Message = ViewModel.SuccessMessage;
                    break;
                case nameof(ViewModel.IsBusy):
                    RegisterBtn.IsLoading = ViewModel.IsBusy;
                    break;
            }
        }
    }

}

