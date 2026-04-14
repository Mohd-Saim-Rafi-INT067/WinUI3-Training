using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImagineDashboard.Services.Interface;
using System;
using System.Collections.Generic;

namespace ImagineDashboard.ViewModels
{
    public partial class SidebarViewModel : ObservableObject
    {
        [ObservableProperty]
        private object currentView;

        [ObservableProperty]
        private DashboardViewModel dashboardViewModel;

        private readonly IDataService _dataService;
        private Dictionary<string, Func<object>> _viewFactory;

        public SidebarViewModel(DashboardViewModel dashboardViewModel, IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            DashboardViewModel = dashboardViewModel ?? throw new ArgumentNullException(nameof(dashboardViewModel));
            
        }

        // Called by SidebarView after it builds the factory with real UserControl instances
        public void RegisterViewFactory(Dictionary<string, Func<object>> factory)
        {
            _viewFactory = factory;
            NavigateToPage("Home"); 
        }

        [RelayCommand]
        public void NavigateToPage(string pageTag)
        {
            if (string.IsNullOrEmpty(pageTag) || _viewFactory == null)
                return;

            if (_viewFactory.TryGetValue(pageTag.ToLower(), out var viewFactory))
            {
                CurrentView = viewFactory();
            }
        }
    }
}