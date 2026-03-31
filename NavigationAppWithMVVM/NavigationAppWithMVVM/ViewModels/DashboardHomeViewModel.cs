using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavigationAppWithMVVM.ViewModels
{
    public partial class DashboardHomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private string welcomeText = "";

        [ObservableProperty]
        private int selectedCategoryIndex = 0;

        [ObservableProperty]
        private string statusText = "";

        private ObservableCollection<string> _items = new();

        public ObservableCollection<string> Items => _items;

        private readonly List<string> _allItems = new()
        {
            "Important: Pay bills",
            "Normal: Buy groceries",
            "Important: Finish assignment",
            "Normal: Clean desk",
            "Normal: Read documentation"
        };

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(SelectedCategoryIndex))
            {
                LoadItemsForCategory();
            }
        }

        public void Initialize(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            string displayName = GetNameFromEmail(email);
            WelcomeText = $"Welcome, {displayName}!";
            SelectedCategoryIndex = 0;
            LoadItemsForCategory();
        }

        private void LoadItemsForCategory()
        {
            string[] categories = { "All", "Important", "Normal" };
            string selectedCategory = SelectedCategoryIndex >= 0 && SelectedCategoryIndex < categories.Length
                ? categories[SelectedCategoryIndex]
                : "All";

            LoadItems(selectedCategory);
        }

        private void LoadItems(string category)
        {
            _items.Clear();

            foreach (string item in _allItems)
            {
                bool shouldAdd = category == "All"
                    || (category == "Important" && item.StartsWith("Important:"))
                    || (category == "Normal" && item.StartsWith("Normal:"));

                if (shouldAdd)
                {
                    _items.Add(item);
                }
            }

            StatusText = $"Showing {_items.Count} item(s) for: {category}";
        }

        private static string GetNameFromEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "";

            int atIndex = email.IndexOf('@');
            return atIndex > 0 ? email.Substring(0, atIndex).Trim() : email.Trim();
        }
    }
}
