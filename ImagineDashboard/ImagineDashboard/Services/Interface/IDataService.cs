using ImagineDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagineDashboard.Services.Interface
{
    public interface IDataService
    {
        Task<List<PatientModel>> LoadPatientsAsync();
        Task<List<PatientDetailsModel>> LoadPatientDetailsAsync();
        Task<PatientDetailsModel> GetPatientDetailsByIdAsync(string patientId);

    }
}
