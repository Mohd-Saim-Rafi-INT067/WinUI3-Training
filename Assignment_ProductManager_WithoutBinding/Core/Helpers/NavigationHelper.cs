using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Assignment_ProductManager_WithoutBinding.Core.Helpers
{
    public static class NavigationHelper
    {
        private static Frame? _rootFrame;

        public static void Initialize(Frame frame)
            => _rootFrame = frame;

        public static void Navigate<T>(object? parameter = null) where T : Microsoft.UI.Xaml.Controls.Page
            => _rootFrame?.Navigate(typeof(T), parameter);

        public static void GoBack()
        {
            if (_rootFrame?.CanGoBack == true)
                _rootFrame.GoBack();
        }

        public static bool CanGoBack => _rootFrame?.CanGoBack ?? false;
    }
}
