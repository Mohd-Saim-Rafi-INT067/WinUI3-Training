using Assignment_Product_Manager.Models;
using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.IO;
using Windows.Storage.Pickers;


namespace Assignment_Product_Manager.Views
{

    public sealed partial class AddEditProductView : Page
    {
        public AddEditProductViewModel ViewModel { get; }

        public AddEditProductView()
        {
            this.InitializeComponent();
            ViewModel = App.GetService<AddEditProductViewModel>();
            ViewModel.NavigationRequested += ViewModel_NavigationRequested;
        }
        private void ViewModel_NavigationRequested(object? sender, EventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
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
            ViewModel.NavigationRequested -= ViewModel_NavigationRequested;
            ViewModel.Dispose();
        }

        private void Back_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
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

        private void RemoveImage_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetImage(null);
        }
    }
}