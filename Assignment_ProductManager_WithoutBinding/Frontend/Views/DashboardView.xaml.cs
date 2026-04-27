using Assignment_ProductManager_WithoutBinding.Models;
using Assignment_ProductManager_WithoutBinding.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.ComponentModel;
using System.Linq;


namespace Assignment_ProductManager_WithoutBinding.Views
{
    public sealed partial class DashboardView : Page
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<DashboardViewModel>();
            NavigationCacheMode = NavigationCacheMode.Required;

            WelcomeTextBlock.Text = ViewModel.WelcomeText;
            AddProductBtn.Click += (s, e) => Frame.Navigate(typeof(AddEditProductView));
            SearchBox.TextChanged += (s, e) => ViewModel.SearchQuery = SearchBox.Text;
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await ViewModel.InitializeAsync();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.WelcomeText):
                    WelcomeTextBlock.Text = ViewModel.WelcomeText;
                    break;
                case nameof(ViewModel.TotalProducts):
                    TotalProductsCard.Value = ViewModel.TotalProducts.ToString();
                    break;
                case nameof(ViewModel.TotalValue):
                    TotalValueCard.Value = ViewModel.TotalValue.ToString("F2");
                    break;
                case nameof(ViewModel.LowStockCount):
                    LowStockCard.Value = ViewModel.LowStockCount.ToString();
                    break;
                case nameof(ViewModel.Products):
                    BuildProductRows();
                    break;
                case nameof(ViewModel.IsEmpty):
                    EmptyStatePanel.Visibility = ViewModel.IsEmpty ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case nameof(ViewModel.ErrorMessage):
                    ErrorBanner.Message = ViewModel.ErrorMessage;
                    break;
                case nameof(ViewModel.SuccessMessage):
                    SuccessBanner.Message = ViewModel.SuccessMessage;
                    break;
                case nameof(ViewModel.IsBusy):
                    BusyRing.IsActive = ViewModel.IsBusy;
                    BusyRing.Visibility = ViewModel.IsBusy ? Visibility.Visible : Visibility.Collapsed;
                    break;
            }
        }

        private void BuildProductRows()
        {
            ProductRowsPanel.Children.Clear();

            foreach (var product in ViewModel.Products)
            {
                var row = BuildRow(product);
                ProductRowsPanel.Children.Add(row);
            }
        }

        private Grid BuildRow(Product product)
        {
            var borderBrush = (SolidColorBrush)Application.Current.Resources["BorderBrush"];
            var textPrimary = (SolidColorBrush)Application.Current.Resources["TextPrimaryBrush"];
            var textSecondary = (SolidColorBrush)Application.Current.Resources["TextSecondaryBrush"];
            var textMuted = (SolidColorBrush)Application.Current.Resources["TextMutedBrush"];
            var radiusSM = (CornerRadius)Application.Current.Resources["RadiusSM"];

            var grid = new Grid
            {
                Padding = new Thickness(16, 12, 16, 12),
                BorderBrush = borderBrush,
                BorderThickness = new Thickness(0, 0, 0, 1)
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(64) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });

            // Col 0 — Name + SKU
            var nameStack = new StackPanel();
            nameStack.Children.Add(new TextBlock { Text = product.ProductName, FontSize = 13, FontWeight = Microsoft.UI.Text.FontWeights.Medium, Foreground = textPrimary });
            nameStack.Children.Add(new TextBlock { Text = product.SKU, FontSize = 11, Foreground = textMuted });
            Grid.SetColumn(nameStack, 0);
            grid.Children.Add(nameStack);

            // Col 1 — Category
            var categoryText = new TextBlock { Text = product.Category, FontSize = 13, Foreground = textSecondary, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(categoryText, 1);
            grid.Children.Add(categoryText);

            // Col 2 — Price
            var priceText = new TextBlock { FontSize = 13, Foreground = textSecondary, VerticalAlignment = VerticalAlignment.Center };
            priceText.Inlines.Add(new Microsoft.UI.Xaml.Documents.Run { Text = "₹" });
            priceText.Inlines.Add(new Microsoft.UI.Xaml.Documents.Run { Text = product.Price.ToString("F2") });
            Grid.SetColumn(priceText, 2);
            grid.Children.Add(priceText);

            // Col 3 — Discount
            var discountText = new TextBlock { FontSize = 13, FontWeight = Microsoft.UI.Text.FontWeights.Medium, Foreground = textSecondary, VerticalAlignment = VerticalAlignment.Center };
            discountText.Inlines.Add(new Microsoft.UI.Xaml.Documents.Run { Text = product.DiscountPct.ToString("F0") });
            discountText.Inlines.Add(new Microsoft.UI.Xaml.Documents.Run { Text = "%" });
            Grid.SetColumn(discountText, 3);
            grid.Children.Add(discountText);

            // Col 4 — Final Price
            var finalPriceText = new TextBlock { FontSize = 13, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Foreground = textSecondary, VerticalAlignment = VerticalAlignment.Center };
            finalPriceText.Inlines.Add(new Microsoft.UI.Xaml.Documents.Run { Text = "₹" });
            finalPriceText.Inlines.Add(new Microsoft.UI.Xaml.Documents.Run { Text = product.FinalPrice.ToString("F2") });
            Grid.SetColumn(finalPriceText, 4);
            grid.Children.Add(finalPriceText);

            // Col 5 — Stock
            var stockStack = new StackPanel { VerticalAlignment = VerticalAlignment.Center };
            stockStack.Children.Add(new TextBlock { Text = product.Stock.ToString(), FontSize = 13, Foreground = textPrimary });
            Grid.SetColumn(stockStack, 5);
            grid.Children.Add(stockStack);

            // Col 6 — Image
            var imageBorder = new Border
            {
                Width = 44,
                Height = 44,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 248, 250, 252)),
                BorderBrush = borderBrush,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6)
            };
            var img = new Image { Width = 44, Height = 44, Stretch = Stretch.UniformToFill };
            img.Source = product.GetOrCreateBitmap();
            imageBorder.Child = img;
            Grid.SetColumn(imageBorder, 6);
            grid.Children.Add(imageBorder);

            // Col 7 — Actions
            var actionsPanel = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center, Spacing = 6 };

            var editBtn = new Button
            {
                Width = 32,
                Height = 32,
                Padding = new Thickness(0),
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 239, 246, 255)),
                CornerRadius = radiusSM,
                Tag = product.ProductId,
                Content = new FontIcon { FontSize = 13, Glyph = "\uE70F" }
            };
            ToolTipService.SetToolTip(editBtn, "Edit");
            editBtn.Click += Edit_Click;

            var deleteBtn = new Button
            {
                Width = 32,
                Height = 32,
                Padding = new Thickness(0),
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 254, 242, 242)),
                CornerRadius = radiusSM,
                Tag = product.ProductId,
                Content = new FontIcon { FontSize = 13, Glyph = "\uE74D" }
            };
            ToolTipService.SetToolTip(deleteBtn, "Delete");
            deleteBtn.Click += Delete_Click;

            actionsPanel.Children.Add(editBtn);
            actionsPanel.Children.Add(deleteBtn);
            Grid.SetColumn(actionsPanel, 7);
            grid.Children.Add(actionsPanel);

            return grid;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int productId)
            {
                var product = ViewModel.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                    Frame.Navigate(typeof(AddEditProductView), product);
            }
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
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
