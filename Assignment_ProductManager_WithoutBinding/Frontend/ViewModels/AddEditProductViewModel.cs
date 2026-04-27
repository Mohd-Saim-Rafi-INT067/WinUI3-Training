using Assignment_ProductManager_WithoutBinding.Models;
using Assignment_ProductManager_WithoutBinding.Services.Interfaces;
using Assignment_ProductManager_WithoutBinding.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.UI.Xaml.Media.Imaging;
using System.IO;
using Assignment_ProductManager_WithoutBinding.Core.Validators;

namespace Assignment_ProductManager_WithoutBinding.ViewModels
{
    public partial class AddEditProductViewModel : BaseViewModel, IDisposable
    {
        private readonly IProductService _productService;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public string PriceText { get; set; } = string.Empty;
        public string StockText { get; set; } = string.Empty;
        public string QuantityText { get; set; } = string.Empty;

        
        private bool _isEditMode;
        public bool IsEditMode
        {
            get => _isEditMode;
            private set => SetProperty(ref _isEditMode, value);
        }

        private string _pageTitle = "Add Product";
        public string PageTitle
        {
            get => _pageTitle;
            private set => SetProperty(ref _pageTitle, value);
        }

        private decimal _discountPct;
        public decimal DiscountPct
        {
            get => _discountPct;
            set => SetProperty(ref _discountPct, value);
        }

        private decimal _finalPrice;
        public decimal FinalPrice
        {
            get => _finalPrice;
            set => SetProperty(ref _finalPrice, value);
        }

        private bool _hasImage;
        public bool HasImage
        {
            get => _hasImage;
            set => SetProperty(ref _hasImage, value);
        }

        private BitmapImage? _previewImage;
        public BitmapImage? PreviewImage
        {
            get => _previewImage;
            set => SetProperty(ref _previewImage, value);
        }

        private byte[]? _imageData;

        private void NotifyInputsLoaded()
        {
            OnPropertyChanged(nameof(ProductId));
            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Sku));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(StockText));
            OnPropertyChanged(nameof(QuantityText));
        }

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
            // Notify code-behind to sync input TextBox values
            NotifyInputsLoaded();
            if (product.ImageData != null)
            {
                SetImage(product.ImageData);
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            ClearMessages();

            var inputError = ProductInputValidator.Validate(PriceText, StockText, QuantityText, out var price, out var stock, out var qty);
            if (inputError != null)
            {
                SetError(inputError);
                return;
            }

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
                ImageData = _imageData
            };

            var context = new ValidationContext(product);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(product, context, results, validateAllProperties: true))
            {
                SetError(results[0].ErrorMessage ?? "Validation Failed");
                return;
            }

            IsBusy = true;
            try
            {
                var result = IsEditMode
                    ? await _productService.UpdateProductAsync(product)
                    : await _productService.AddProductAsync(product);

                if (result.Success)
                {
                    SetSuccess(result.Message);
                    await Task.Delay(1000);
                    GoBack();
                }
                else SetError(result.Message);
            }
            catch (Exception ex) { SetError($"Error: {ex.Message}"); }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private void GoBack() => NavigationRequested?.Invoke(this, EventArgs.Empty);

        public void Dispose()
        {
            NavigationRequested = null;
            PreviewImage = null;
            _imageData = null;
        }

        public void SetImage(byte[]? bytes)
        {
            _imageData = bytes;
            HasImage = bytes != null && bytes.Length > 0;

            if (HasImage)
            {
                
                var bitmap = new BitmapImage();
                using var ms = new MemoryStream(bytes!);
                bitmap.SetSource(ms.AsRandomAccessStream());
                PreviewImage = bitmap;
            }
            else
            {
                PreviewImage = null;
            }
        }

        public void SetImageError(string message) => SetError(message);
    }
}
