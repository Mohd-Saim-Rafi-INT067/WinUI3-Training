using Assignment_Product_Manager.Models;
using Assignment_Product_Manager.Services.Interfaces;
using Assignment_Product_Manager.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Assignment_Product_Manager.ViewModels
{
    public partial class AddEditProductViewModel : BaseViewModel, IDisposable
    {
        private readonly IProductService _productService;

        [ObservableProperty] private int _productId;
        [ObservableProperty] private string _productName = string.Empty;
        [ObservableProperty] private string _description = string.Empty;
        [ObservableProperty] private string _category = string.Empty;
        [ObservableProperty] private string _sku = string.Empty;
        [ObservableProperty] private string _priceText = string.Empty;
        [ObservableProperty] private string _stockText = string.Empty;
        [ObservableProperty] private string _quantityText = string.Empty;
        [ObservableProperty] private bool _isEditMode;
        [ObservableProperty] private string _pageTitle = "Add Product";
        [ObservableProperty] private decimal _discountPct;
        [ObservableProperty] private decimal _finalPrice;

        public List<string> Categories { get; } = new()
        { "Electronics", "Clothing", "Food & Beverages", "Books", "Health & Beauty",
          "Home & Garden", "Sports", "Toys", "Automotive", "Other" };

        public event EventHandler? NavigationRequested;

        public AddEditProductViewModel(IProductService productService)
            => _productService = productService;

        public void LoadProduct(Product product)
        {
            IsEditMode = true;
            PageTitle = "Edit Product";
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Description = product.Description;
            Category = product.Category;
            Sku = product.SKU;
            PriceText = product.Price.ToString("F2");
            StockText = product.Stock.ToString();
            QuantityText = product.Quantity.ToString();
            DiscountPct = product.DiscountPct;
            FinalPrice = product.FinalPrice;
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            ClearMessages();

            if (!decimal.TryParse(PriceText, out var price) || price <= 0)
            { SetError("Enter a valid price greater than 0."); return; }
            if (!int.TryParse(StockText, out var stock) || stock < 0)
            { SetError("Enter a valid stock quantity (0 or more)."); return; }
            if (!int.TryParse(QuantityText, out var qty) || qty < 0)
            { SetError("Enter a valid quantity (0 or more)."); return; }

            var product = new Product
            {
                ProductId = ProductId,
                ProductName = ProductName.Trim(),
                Description = Description.Trim(),
                Category = Category,
                SKU = Sku.Trim(),
                Price = price,
                Stock = stock,
                Quantity = qty,
            };

            var context = new ValidationContext(product);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(product,context, results, validateAllProperties: true))
            {
                SetError(results[0].ErrorMessage ?? "Validation Failed");
                return;
            }

            IsBusy = true;
            try
            {
                var (success, message) = IsEditMode
                    ? await _productService.UpdateProductAsync(product)
                    : await _productService.AddProductAsync(product);

                if (success)
                {
                    SetSuccess(message);
                    await Task.Delay(1000);
                    GoBack();
                }
                else SetError(message);
            }
            catch (Exception ex) { SetError($"Error: {ex.Message}"); }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private void GoBack() => NavigationRequested?.Invoke(this, EventArgs.Empty);

        public void Dispose()
        {
            NavigationRequested = null;
        }
    }
}
