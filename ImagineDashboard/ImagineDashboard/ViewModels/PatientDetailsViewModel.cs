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
    public partial class PatientDetailsViewModel : ObservableObject
    {
        private readonly IDataService _dataService;

        // Single selected patient's details (used by ChargeDetailsView form)
        [ObservableProperty]
        private PatientDetailsModel currentPatientDetails;

        // Collection bound to ChargesGridView DataGrid
        [ObservableProperty]
        private ObservableCollection<PatientDetailsModel> patientDetailsList;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private bool hasPatientSelected;

        [ObservableProperty]
        private DateTimeOffset fromDate = DateTimeOffset.Now;

        // Validation
        [ObservableProperty]
        private bool hasValidationError;

        [ObservableProperty]
        private string validationMessage = string.Empty;

        // Search text (used by Dept Code / ID fields)
        [ObservableProperty]
        private string searchText = string.Empty;

        // These values are computed on demand very time the UI asks for them. They return true if the field is Null.
        // ?. is no patient is selected then we will safely return True
        public bool IsProcedureEmpty =>
           HasPatientSelected && string.IsNullOrWhiteSpace(CurrentPatientDetails?.Procedure);

        public bool IsUnitsEmpty =>
           HasPatientSelected && string.IsNullOrWhiteSpace(CurrentPatientDetails?.Units);

        public bool IsOrderNumberEmpty =>
           HasPatientSelected && string.IsNullOrWhiteSpace(CurrentPatientDetails?.OrderNumber);


        // label for the ChargesGridView header, eg:"5 Charges"
        public string ChargeCountText => $"{PatientDetailsList?.Count ?? 0} Charge{(PatientDetailsList?.Count == 1 ? "" : "s")}";

        public PatientDetailsViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            CurrentPatientDetails = new PatientDetailsModel();
            PatientDetailsList = new ObservableCollection<PatientDetailsModel>();

            // Keep ChargeCountText in sync whenever the list changes
            PatientDetailsList.CollectionChanged += (s, e) => OnPropertyChanged(nameof(ChargeCountText));
        }

        //The UI also needs to know when the HasSelectedPatient value changed, so that it can re-read the value
        partial void OnHasPatientSelectedChanged(bool value)
        {
            // Refresh all red-border flags whenever selection state change
            OnPropertyChanged(nameof(IsProcedureEmpty));
            OnPropertyChanged(nameof(IsUnitsEmpty));
            OnPropertyChanged(nameof(IsOrderNumberEmpty));
        }

        // This runs whenever a new patient is loaded
        partial void OnCurrentPatientDetailsChanged(PatientDetailsModel value)
        {
            
            OnPropertyChanged(nameof(IsProcedureEmpty));
            OnPropertyChanged(nameof(IsUnitsEmpty));
            OnPropertyChanged(nameof(IsOrderNumberEmpty));

            // When the user will type in an empty field we will call this again, so that the border will turn grey again from red
            if (value != null)
            {
                value.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(PatientDetailsModel.Procedure))
                        OnPropertyChanged(nameof(IsProcedureEmpty));
                    else if (e.PropertyName == nameof(PatientDetailsModel.Units))
                        OnPropertyChanged(nameof(IsUnitsEmpty));
                    else if (e.PropertyName == nameof(PatientDetailsModel.OrderNumber))
                        OnPropertyChanged(nameof(IsOrderNumberEmpty));
                };
            }
        }

        private void ValidateCurrentRecord(PatientDetailsModel model)
        {
            if (model == null || !model.HasValidationErrors)
            {
                HasValidationError = false;
                ValidationMessage = string.Empty;
                return;
            }
            HasValidationError = true;
            ValidationMessage = $"Charge Info \u2013 {model.ValidationErrorText}";
        }

        [RelayCommand]
        public async Task LoadAllPatientDetails()
        {
            try
            {
                IsLoading = true;
                HasError = false;
                ErrorMessage = string.Empty;

                var details = await _dataService.LoadPatientDetailsAsync();

                PatientDetailsList.Clear();
                foreach (var detail in details)
                {
                    PatientDetailsList.Add(detail);
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Failed to load patient details: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task LoadPatientDetails(string patientId)
        {
            if (string.IsNullOrWhiteSpace(patientId))
            {
                // No patient selected — grid stays empty
                ClearPatientDetails();
                return;
            }

            try
            {
                IsLoading = true;
                HasError = false;
                ErrorMessage = string.Empty;

                var details = await _dataService.GetPatientDetailsByIdAsync(patientId);

                if (details != null && !string.IsNullOrEmpty(details.PatientId))
                {
                    CurrentPatientDetails = details;
                    HasPatientSelected = true;

                    // Show only this one record in the charges grid
                    PatientDetailsList.Clear();
                    PatientDetailsList.Add(details);

                    // Run validation — shows banner if required fields are missing
                    ValidateCurrentRecord(details);
                }
                else
                {
                    // Patient exists in patients.json but has no charge record
                    ClearPatientDetails();
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Failed to load patient details: {ex.Message}";
                HasPatientSelected = false;
            }
            finally { IsLoading = false; }
        }


        [RelayCommand]
        public async Task SavePatientDetails()
        {
            if (CurrentPatientDetails == null)
            {
                HasError = true;
                ErrorMessage = "No patient details to save.";
                return;
            }

            try
            {
                IsLoading = true;
                HasError = false;
                ErrorMessage = string.Empty;

                await Task.Delay(100); // Replace with actual save logic
                HasError = false;
                ErrorMessage = "Patient details saved successfully.";
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Failed to save patient details: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public void ClearPatientDetails()
        {
            CurrentPatientDetails = new PatientDetailsModel();
            PatientDetailsList.Clear();
            HasPatientSelected = false;
            HasError = false;
            ErrorMessage = string.Empty;
            HasValidationError = false;
            ValidationMessage = string.Empty;
        }
    }
}
