using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineDashboard.Models
{
    public partial class PatientDetailsModel : ObservableObject
    {
        [ObservableProperty]
        private string patientId;

        [ObservableProperty]
        private string patientName;

        [ObservableProperty]
        private string visitNumber;

        [ObservableProperty]
        private string location;

        [ObservableProperty]
        private string dateOfService;

        [ObservableProperty]
        private string procedure;

        [ObservableProperty]
        private string modifier;

        [ObservableProperty]
        private string units;

        [ObservableProperty]
        private string orderNumber;

        [ObservableProperty]
        private string chargeReport;


        //Validation Logic 
        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Procedure))
                errors.Add("Proc Code is required.");

            if (string.IsNullOrWhiteSpace(Units))
                errors.Add("Units is required.");

            if (string.IsNullOrWhiteSpace(OrderNumber))
                errors.Add("Order Number is required.");

            return errors;
        }

        //True when any required field is missing
        public bool HasValidationErrors => GetValidationErrors().Any();

        //Single string shown in the UI banner
        public string ValidationErrorText => string.Join("  ", GetValidationErrors());


        public PatientDetailsModel()
        {
        }

        public PatientDetailsModel(string patientId, string patientName, string visitNumber, string location, string dateOfService, string procedure, string modifier, string units, string orderNumber, string chargeReport)
        {
            PatientId = patientId;
            PatientName = patientName;
            VisitNumber = visitNumber;
            Location = location;
            DateOfService = dateOfService;
            Procedure = procedure;
            Modifier = modifier;
            Units = units;
            OrderNumber = orderNumber;
            ChargeReport = chargeReport;
        }
    }
}
