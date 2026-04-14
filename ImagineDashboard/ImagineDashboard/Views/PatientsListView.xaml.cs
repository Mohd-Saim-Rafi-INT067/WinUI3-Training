using CommunityToolkit.WinUI.UI.Controls;
using ImagineDashboard.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

namespace ImagineDashboard.Views
{
    public sealed partial class PatientsListView : UserControl
    {
        public PatientsViewModel ViewModel => DataContext as PatientsViewModel;

        public PatientsListView()
        {
            this.InitializeComponent();
            this.DataContextChanged += PatientsListView_DataContextChanged;
        }

        //view can react when its viewModel is changed
        private async void PatientsListView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (ViewModel != null && (ViewModel.Patients == null || ViewModel.Patients.Count == 0))
            {
                await ViewModel.LoadDataCommand.ExecuteAsync(null);
            }
        }

        //tell viewModel which patient was selected
        private void PatientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem != null)
            {
                ViewModel?.SelectPatientCommand?.Execute(grid.SelectedItem);
            }
        }
    }
}