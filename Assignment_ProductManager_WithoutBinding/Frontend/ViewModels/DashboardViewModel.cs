using Assignment_ProductManager_WithoutBinding.Models;
using Assignment_ProductManager_WithoutBinding.Services.Interfaces;
using Assignment_ProductManager_WithoutBinding.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_ProductManager_WithoutBinding.ViewModels
{
    public partial class DashboardViewModel : BaseViewModel, IDisposable
    {
        private readonly IProductService _productService;
        private readonly IAuthService _authService;

        // These properties need SetProperty so PropertyChanged fires —
        // the code-behind handler reads them to update the corresponding UI elements.

        private ObservableCollection<Product> _products = new();
        public ObservableCollection<Product> Products
        {
            get => _products;
            private set => SetProperty(ref _products, value);
        }

        private string _welcomeText = string.Empty;
        public string WelcomeText
        {
            get => _welcomeText;
            private set => SetProperty(ref _welcomeText, value);
        }

        private int _totalProducts;
        public int TotalProducts
        {
            get => _totalProducts;
            private set => SetProperty(ref _totalProducts, value);
        }

        private decimal _totalValue;
        public decimal TotalValue
        {
            get => _totalValue;
            private set => SetProperty(ref _totalValue, value);
        }

        private int _lowStockCount;
        public int LowStockCount
        {
            get => _lowStockCount;
            private set => SetProperty(ref _lowStockCount, value);
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get => _isEmpty;
            private set => SetProperty(ref _isEmpty, value);
        }

        // SearchQuery is written by the code-behind (search box TextChanged) and
        // immediately consumed — no need to notify the view back.
        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                RefreshProductList();
            }
        }

        // SelectedProduct is internal state only — not reflected in UI via PropertyChanged.
        private Product? _selectedProduct;

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

        private void RefreshProductList()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchQuery)
                ? _allProducts
                : _allProducts.Where(p =>
                    p.ProductName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    p.SKU.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            Products = new ObservableCollection<Product>(filtered);
            IsEmpty = Products.Count == 0;
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
            var result = await _productService.DeleteProductAsync(product.ProductId);
            if (result.Success)
            {
                _allProducts.Remove(product);
                RefreshProductList();
                UpdateStats();
                SetSuccess(result.Message);
            }
            else SetError(result.Message);
            IsBusy = false;
        }

        public void Dispose()
        {
            _allProducts.Clear();
            Products.Clear();
        }
    }
}
