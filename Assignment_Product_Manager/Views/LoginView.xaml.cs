using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Windows.System;


namespace Assignment_Product_Manager.Views
{
    public sealed partial class LoginView : Page
    {
        public LoginViewModel ViewModel { get; }

        public LoginView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<LoginViewModel>();
            DataContext = ViewModel;
        }
    }
}
