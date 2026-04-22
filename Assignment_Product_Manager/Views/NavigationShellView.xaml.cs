using Assignment_Product_Manager.Services.Interfaces;
using Microsoft.UI.Xaml.Controls;


namespace Assignment_Product_Manager.Views
{
    
    public sealed partial class NavigationShellView : Page
    {
        private readonly IAuthService _authService;

        public NavigationShellView()
        {
            this.InitializeComponent();
            _authService = App.GetService<IAuthService>();
            ContentFrame.Navigate(typeof(DashboardView));
            NavView.SelectedItem = NavView.MenuItems[0];
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected) return;

            var tag = (args.SelectedItem as NavigationViewItem)?.Tag?.ToString();
            switch (tag)
            {
                case "dashboard":
                case "products":
                    ContentFrame.Navigate(typeof(DashboardView));
                    break;
                case "addproduct":
                    ContentFrame.Navigate(typeof(AddEditProductView));
                    break;
                case "logout":
                    _authService.Logout();
                    if (App.MainWindow.Content is Frame rootFrame)
                        rootFrame.Navigate(typeof(LoginView));
                    break;
            }
        }
    }
}
