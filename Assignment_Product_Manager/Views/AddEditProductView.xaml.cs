using Assignment_Product_Manager.Models;
using Assignment_Product_Manager.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;


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
            // Unsubscribe to avoid memory leaks if the page is cached
            ViewModel.NavigationRequested -= ViewModel_NavigationRequested;
        }

        private void Back_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }
    }
}
