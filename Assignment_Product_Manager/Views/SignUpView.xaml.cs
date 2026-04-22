using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace Assignment_Product_Manager.Views
{
    
    public sealed partial class SignUpView : Page
    {
        public SignUpViewModel ViewModel { get; }

        public SignUpView()
        {
            ViewModel = App.GetService<SignUpViewModel>();
            this.InitializeComponent();
            DataContext = ViewModel;
        }
    }

}

