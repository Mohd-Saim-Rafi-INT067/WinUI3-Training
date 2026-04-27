using Assignment_ProductManager_WithoutBinding.Models;
using Assignment_ProductManager_WithoutBinding.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.ComponentModel;
using System.IO;
using Windows.Storage.Pickers;


namespace Assignment_ProductManager_WithoutBinding.Views
{

    public sealed partial class AddEditProductView : Page
    {
        public AddEditProductViewModel ViewModel { get; }

        public AddEditProductView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<AddEditProductViewModel>();
            PageTitleText.Text = ViewModel.PageTitle;

            // Populate category combo from ViewModel
            CategoryCombo.ItemsSource = ViewModel.Categories;

            // Push user input into ViewModel
            ProductNameBox.TextChanged += (s, e) => ViewModel.ProductName = ProductNameBox.InputBox.Text;
            DescriptionBox.TextChanged += (s, e) => ViewModel.Description = DescriptionBox.InputBox.Text;
            SkuBox.TextChanged += (s, e) => ViewModel.Sku = SkuBox.InputBox.Text;
            PriceBox.TextChanged += (s, e) => ViewModel.PriceText = PriceBox.InputBox.Text;
            StockBox.TextChanged += (s, e) => ViewModel.StockText = StockBox.InputBox.Text;
            QuantityBox.TextChanged += (s, e) => ViewModel.QuantityText = QuantityBox.InputBox.Text;
            CategoryCombo.SelectionChanged += (s, e) =>
                ViewModel.Category = CategoryCombo.SelectedItem as string ?? string.Empty;

            // Wire buttons
            BackBtn.Click += (s, e) => { if (Frame.CanGoBack) Frame.GoBack(); };
            CancelBtn.Click += (s, e) => { if (Frame.CanGoBack) Frame.GoBack(); };
            SaveBtn.Click += async (s, e) => await ViewModel.SaveCommand.ExecuteAsync(null);
            PickImageBtn.Click += PickImage_Click;
            RemoveImageBtn.Click += (s, e) => ViewModel.SetImage(null);

            // Reflect ViewModel state changes into UI
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            ViewModel.NavigationRequested += (s, e) => { if (Frame.CanGoBack) Frame.GoBack(); };
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.PageTitle):
                    PageTitleText.Text = ViewModel.PageTitle;
                    break;

                case nameof(ViewModel.ErrorMessage):
                    ErrorBanner.Message = ViewModel.ErrorMessage;
                    break;

                case nameof(ViewModel.SuccessMessage):
                    SuccessBanner.Message = ViewModel.SuccessMessage;
                    break;

                case nameof(ViewModel.IsBusy):
                    SaveBtn.IsLoading = ViewModel.IsBusy;
                    break;

                case nameof(ViewModel.HasImage):
                    var hasImage = ViewModel.HasImage;
                    ImagePlaceholderIcon.Visibility = hasImage ? Visibility.Collapsed : Visibility.Visible;
                    ProductImagePreview.Visibility = hasImage ? Visibility.Visible : Visibility.Collapsed;
                    RemoveImageBtn.Visibility = hasImage ? Visibility.Visible : Visibility.Collapsed;
                    break;

                case nameof(ViewModel.PreviewImage):
                    ProductImagePreview.Source = ViewModel.PreviewImage;
                    break;

                case nameof(ViewModel.Category):
                    if (CategoryCombo.SelectedItem as string != ViewModel.Category)
                        CategoryCombo.SelectedItem = ViewModel.Category;
                    break;

                case nameof(ViewModel.ProductName):
                    if (ProductNameBox.InputBox.Text != ViewModel.ProductName)
                        ProductNameBox.InputBox.Text = ViewModel.ProductName;
                    break;
                case nameof(ViewModel.Description):
                    if (DescriptionBox.InputBox.Text != ViewModel.Description)
                        DescriptionBox.InputBox.Text = ViewModel.Description;
                    break;
                case nameof(ViewModel.Sku):
                    if (SkuBox.InputBox.Text != ViewModel.Sku)
                        SkuBox.InputBox.Text = ViewModel.Sku;
                    break;
                case nameof(ViewModel.PriceText):
                    if (PriceBox.InputBox.Text != ViewModel.PriceText)
                        PriceBox.InputBox.Text = ViewModel.PriceText;
                    break;
                case nameof(ViewModel.StockText):
                    if (StockBox.InputBox.Text != ViewModel.StockText)
                        StockBox.InputBox.Text = ViewModel.StockText;
                    break;
                case nameof(ViewModel.QuantityText):
                    if (QuantityBox.InputBox.Text != ViewModel.QuantityText)
                        QuantityBox.InputBox.Text = ViewModel.QuantityText;
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is Product product)
                ViewModel.LoadProduct(product);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            ViewModel.Dispose();
        }

        private async void PickImage_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            var windowHandler = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandler);

            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");

            var file = await picker.PickSingleFileAsync();
            if (file == null) return;

            var props = await file.GetBasicPropertiesAsync();
            if (props.Size > 5 * 1024 * 1024)
            {
                ViewModel.SetImageError("Image must be 5 MB or smaller.");
                return;
            }

            using var stream = await file.OpenStreamForReadAsync();
            var bytes = new byte[stream.Length];
            await stream.ReadAsync(bytes);
            ViewModel.SetImage(bytes);

        }
    }
}