using Assignment_Product_Manager.Models;
using Assignment_Product_Manager.Services.Interfaces;
using Assignment_Product_Manager.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Product_Manager.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        [ObservableProperty] private ObservableCollection<Product> _products = new();
        [ObservableProperty] private Product? _selectedProduct;
        [ObservableProperty] private string _searchQuery = string.Empty;
        [ObservableProperty] private string _welcomeText = string.Empty;
        [ObservableProperty] private int _totalProducts;
        [ObservableProperty] private decimal _totalValue;
        [ObservableProperty] private int _lowStockCount;

        private List<Product> _allProducts = new();

        public DashboardViewModel(IProductService productService, IAuthService authService)
        {
            _productService = productService;
            _authService = authService;
            WelcomeText = $"Welcome, {_authService.CurrentUser?.FullName ?? "User"}!";
        }

        public async Task InitializeAsync()
        {
            await LoadProductsAsync();
        }

        [RelayCommand]
        private async Task LoadProductsAsync()
        {
            IsBusy = true;
            ClearMessages();
            try
            {
                var list = await _productService.GetMyProductsAsync();
                _allProducts = list.ToList();
                RefreshProductList();
                UpdateStats();
            }
            catch (Exception ex)
            {
                SetError($"Failed to load products: {ex.Message}");
            }
            finally { IsBusy = false; }
        }

        partial void OnSearchQueryChanged(string value)
            => RefreshProductList();

        private void RefreshProductList()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchQuery)
                ? _allProducts
                : _allProducts.Where(p =>
                    p.ProductName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.SKU.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            Products = new ObservableCollection<Product>(filtered);
        }

        private void UpdateStats()
        {
            TotalProducts = _allProducts.Count;
            TotalValue = _allProducts.Sum(p => p.FinalPrice * p.Quantity);
            LowStockCount = _allProducts.Count(p => p.Stock < 10);
        }

        [RelayCommand]
        private async Task DeleteProductAsync(Product product)
        {
            if (product == null) return;
            IsBusy = true;
            var (success, message) = await _productService.DeleteProductAsync(product.ProductId);
            if (success)
            {
                _allProducts.Remove(product);
                RefreshProductList();
                UpdateStats();
                SetSuccess(message);
            }
            else SetError(message);
            IsBusy = false;
        }
    }
}
