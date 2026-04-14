using CommunityToolkit.WinUI.UI.Controls;
using ImagineDashboard.Models;
using ImagineDashboard.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ImagineDashboard.Views
{
    public sealed partial class ChargesGridView : UserControl
    {
        public PatientDetailsViewModel ViewModel => DataContext as PatientDetailsViewModel;
        public ChargesGridView() { this.InitializeComponent(); }

        private void ChargesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem is PatientDetailsModel detail && ViewModel != null)
            {
                // Set the selected detail into the ViewModel so the details form binds to it
                ViewModel.CurrentPatientDetails = detail;

                // Try to parse DateOfService (string) into FromDate (DateTimeOffset?) used by CustomDateInput
                if (DateTimeOffset.TryParse(detail.DateOfService, out var dto))
                {
                    ViewModel.FromDate = dto;
                }
                else
                {
                    // keep existing FromDate or set to now
                    ViewModel.FromDate = ViewModel.FromDate == default ? DateTimeOffset.Now : ViewModel.FromDate;
                }
            }
            else if (sender is DataGrid g && g.SelectedItem == null && ViewModel != null)
            {
                // Clear selection
                ViewModel.CurrentPatientDetails = new PatientDetailsModel();
            }
        }
    }
}