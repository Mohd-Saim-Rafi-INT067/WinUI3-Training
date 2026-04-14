using Microsoft.UI.Xaml;
using ImagineDashboard.ViewModels;
using ImagineDashboard.Views;

namespace ImagineDashboard
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var sidebarViewModel = App.ServiceProvider.GetService(typeof(SidebarViewModel)) as SidebarViewModel;
            var sidebarView = new SidebarView(sidebarViewModel);
            this.Content = sidebarView;
        }
    }
}