using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ImagineDashboard.Models;
using ImagineDashboard.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineDashboard.ViewModels
{
    public partial class PatientsViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        [ObservableProperty]
        private ObservableCollection<PatientModel> patients;

        [ObservableProperty]
        private PatientModel selectedPatient;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool hasError;

        public PatientsViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            patients = new ObservableCollection<PatientModel>();
        }

        [RelayCommand]
        public async Task LoadData()
        {
            try
            {
                IsLoading = true;
                HasError = false;
                ErrorMessage = string.Empty;
                var patientsList = await _dataService.LoadPatientsAsync();
                Patients.Clear();
                foreach(var patient in patientsList)
                {
                    Patients.Add(patient);
                }

            }
            catch(Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Failed to Load Patients {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task SelectPatient(PatientModel patient)
        {
            if (patient == null)
            {
                SelectedPatient = null;
                return;
            }
            SelectedPatient = patient;
            HasError = false;
            ErrorMessage = string.Empty;
        }

        [RelayCommand]
        public void ClearData()
        {
            Patients.Clear();
            SelectedPatient = null;
            HasError = false;
            ErrorMessage = string.Empty;
        }
    }
}
