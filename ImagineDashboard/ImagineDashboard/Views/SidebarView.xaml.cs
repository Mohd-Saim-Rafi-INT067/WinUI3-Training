using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using ImagineDashboard.ViewModels;
using System.Collections.Generic;

namespace ImagineDashboard.Views
{
    public sealed partial class SidebarView : UserControl
    {
        public SidebarViewModel ViewModel { get; }

        public SidebarView()
        {
            this.InitializeComponent();
        }

        public SidebarView(SidebarViewModel viewModel) : this()
        {
            ViewModel = viewModel;
            this.Loaded += SidebarView_Loaded;
            //event which fires when UI element is fully created and added to the visual tree
        }

        private void SidebarView_Loaded(object sender, RoutedEventArgs e)
        {
            // Build DashboardView once with its ViewModel
            var dashboardView = new DashboardView(ViewModel.DashboardViewModel);

            var viewFactory = new Dictionary<string, System.Func<object>> //string to function
            {
                { "home",     () => dashboardView },
                { "charge",   () => dashboardView },
                { "billing",  () => MakePlaceholder("Billing Page ") },
                { "patient",  () => MakePlaceholder("Patient Page ") },
                { "payment",  () => MakePlaceholder("Payment Page ") },
                { "followup", () => MakePlaceholder("Follow Up Page ") },
                { "contract", () => MakePlaceholder("Contract Page ") },
                { "copilot",  () => MakePlaceholder("Imagine Co-Pilot Page ") },
                { "system",   () => MakePlaceholder("System Page ") },
            };

            ViewModel.RegisterViewFactory(viewFactory);
        }

        private static TextBlock MakePlaceholder(string text) =>
            new TextBlock { Text = text, Margin = new Thickness(24) };
    }
}