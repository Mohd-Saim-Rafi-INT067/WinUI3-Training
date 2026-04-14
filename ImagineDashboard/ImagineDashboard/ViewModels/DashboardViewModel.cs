using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImagineDashboard.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineDashboard.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private PatientsViewModel patientsViewModel;

        [ObservableProperty]
        private PatientDetailsViewModel patientDetailsViewModel;

        [ObservableProperty]
        private string dashboardTitle = "Charge Central Dashboard";

        [ObservableProperty]
        private bool isInitialized;

        // Bound to CustomReportBox — updates whenever a patient is selected
        [ObservableProperty]
        private string selectedPatientDescription = string.Empty;

        public DashboardViewModel(IDataService dataService)
        {
            PatientsViewModel = new PatientsViewModel(dataService);
            PatientDetailsViewModel = new PatientDetailsViewModel(dataService);
            // When a patient is selected in the list, load their details
            PatientsViewModel.PropertyChanged += PatientsViewModel_PropertyChanged;
        }
        [RelayCommand]
        public async Task InitializeDashboard()
        {
            try
            {

                await PatientsViewModel.LoadDataCommand.ExecuteAsync(null);

                IsInitialized = true;
            }
            catch (Exception ex)
            {
                PatientsViewModel.HasError = true;
                PatientsViewModel.ErrorMessage = $"Failed to initialize dashboard: {ex.Message}";
            }
        }

        private void PatientsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //this will only run when the selected patient changes
            if (e.PropertyName == nameof(PatientsViewModel.SelectedPatient))
            {
                var selectedPatient = PatientsViewModel.SelectedPatient;
                if (selectedPatient != null)
                {
                    // Pull description straight from the selected PatientModel
                    SelectedPatientDescription = selectedPatient.Description ?? string.Empty;
                    // Filter ChargesGridView to this patient's charges
                    PatientDetailsViewModel.LoadPatientDetailsCommand.Execute(selectedPatient.PatientId);
                }
                else
                {
                    // Patient deselected — clear everything
                    SelectedPatientDescription = string.Empty;
                    PatientDetailsViewModel.ClearPatientDetailsCommand.Execute(null);
                }
            }
        }

        //collection for filters
        public ObservableCollection<string> BatchNumbers { get; } = new(){
            "(All)", "Batch 1", "Batch 2", "Batch 3"
        };

        public ObservableCollection<string> Buckets { get; } = new()
        {
            "(All) - 3626 Charges" , "Bucket A - 1200 Charges", "Bucket B - 1500 Charges", "Bucket C - 926 Charges"
        };

        public ObservableCollection<string> Filters { get; } = new()
        {
           "(None)" , "Filter 1", "Filter 2", "Filter 3"
        };

    }
}
