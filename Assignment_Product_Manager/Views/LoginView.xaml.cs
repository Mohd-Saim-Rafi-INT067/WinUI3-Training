using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml.Controls;


namespace Assignment_Product_Manager.Views
{
    public sealed partial class LoginView : Page
    {
        public LoginViewModel ViewModel { get; }

        public LoginView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<LoginViewModel>();
            //give me an instance of loginviewmodel from the DI container(transient)
            DataContext = ViewModel;
            //use the viewmodel as the datacontext for this view
        }
    }
}
