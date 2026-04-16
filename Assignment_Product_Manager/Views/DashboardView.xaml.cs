using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Linq;


namespace Assignment_Product_Manager.Views
{
    
    public sealed partial class DashboardView : Page
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<DashboardViewModel>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.InitializeAsync();
        }

        private void AddProduct_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
            => Frame.Navigate(typeof(AddEditProductView));

        private void Edit_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int productId)
            {
                var product = ViewModel.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                    Frame.Navigate(typeof(AddEditProductView), product);
            }
        }

        private async void Delete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int productId)
            {
                var product = ViewModel.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                    await ViewModel.DeleteProductCommand.ExecuteAsync(product);
            }
        }
    }
}
