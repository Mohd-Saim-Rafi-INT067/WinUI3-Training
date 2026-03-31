using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NavigationAppWithMVVM.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NavigationAppWithMVVM.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username = "";

        public event Action? NavigateToLogin;
        public event Action? ShowDashboardHome;
        public event Action? ShowSettings;

        [RelayCommand]
        private void OnShowDashboardHome()
        {
            ShowDashboardHome?.Invoke();
        }

        [RelayCommand]
        private void OnLogout()
        {
            NavigateToLogin?.Invoke();
        }

        public void Initialize(string username)
        {
            Username = username;
            ShowDashboardHome?.Invoke();
        }
    }
}
