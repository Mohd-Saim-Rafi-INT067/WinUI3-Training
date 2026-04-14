using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineDashboard.Models
{
    public partial class PatientModel : ObservableObject
    {
        [ObservableProperty]
        private string audited;

        [ObservableProperty]
        private string pos;

        [ObservableProperty]
        private string patientId;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string middleName;

        // Loaded from patients.json and displayed in the CustomReportBox
        [ObservableProperty]
        private string description;

        public PatientModel()
        {

        }

        public PatientModel(string audited, string pos, string patientId,  string firstName, string middleName, string description = "")
        {
            Audited = audited;
            Pos = pos;
            PatientId = patientId;
            FirstName = firstName;
            MiddleName = middleName;
            Description = description;
        }


    }
}
