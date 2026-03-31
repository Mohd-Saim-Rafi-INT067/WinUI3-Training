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
    public partial class SettingViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userInfo = "";

        [ObservableProperty]
        private bool isSaveMessageVisible = false;

        [ObservableProperty]
        private string saveMessage = "";

        [RelayCommand]
        private void OnSaveSettings()
        {
            SaveMessage = "Saved!";
            IsSaveMessageVisible = true;
        }

        public void Initialize(string username)
        {
            UserInfo = string.IsNullOrWhiteSpace(username)
                ? "Logged in as: (unknown)"
                : $"Logged in as: {username}";
        }
    }
}
