using ImagineDashboard.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ImagineDashboard.Views
{
    public sealed partial class DashboardView : UserControl
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardView()
        {
            this.InitializeComponent();
        }

        public DashboardView(DashboardViewModel viewModel) : this()
        {
            ViewModel = viewModel;
            this.Loaded += DashboardView_Loaded;
        }

        private async void DashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= DashboardView_Loaded;
            if (ViewModel != null && !ViewModel.IsInitialized)
            {
                await ViewModel.InitializeDashboardCommand.ExecuteAsync(null);
            }

            // Initialize WebView2 and render the initial (empty) report
            await ReportWebView.EnsureCoreWebView2Async();
            RenderReport(ViewModel?.SelectedPatientDescription ?? string.Empty);

            // Re-render whenever the selected patient changes
            if (ViewModel != null)
                ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.SelectedPatientDescription))
                RenderReport(ViewModel.SelectedPatientDescription ?? string.Empty);
        }

        
        private void RenderReport(string reportText)
        {
            // Escape HTML special characters so <, > and & in the text are safe
            var escaped = System.Net.WebUtility.HtmlEncode(reportText);

            var html = $@"<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'/>
<style>
  body {{
    margin: 12px;
    font-family: 'Courier New', monospace;
    font-size: 12px;
    white-space: pre;
    background: #ffffff;
    color: #1a1a1a;
    line-height: 1.5;
  }}
</style>
</head>
<body>{escaped}</body>
</html>";

            ReportWebView.NavigateToString(html);
        }


    }
}