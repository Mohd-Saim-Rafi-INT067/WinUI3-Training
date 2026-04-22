using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
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
            NavigationCacheMode = NavigationCacheMode.Required;
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
            if (sender is not Button btn || btn.Tag is not int productId) return;
            var product = ViewModel.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product == null) return;

            var dialog = new ContentDialog
            {
                Title = "Delete Product",
                Content = $"Are you sure you want to delete \"{product.ProductName}\"?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                await ViewModel.DeleteProductCommand.ExecuteAsync(product);
        }
    }
}
