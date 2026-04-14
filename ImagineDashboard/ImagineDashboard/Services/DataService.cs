using ImagineDashboard.Models;
using ImagineDashboard.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImagineDashboard.Services
{
    public class DataService : IDataService
    {
        private readonly string _basePath; //folder where the json files are there
        private const string PatientsFileName = "patients.json";
        private const string PatientDetailsFileName = "patientDetails.json";

        public DataService()
        {
            _basePath = Path.Combine(AppContext.BaseDirectory, "Assets", "Data"); //builds a path like this
        }

        public async Task<List<PatientModel>> LoadPatientsAsync()
        {
            try
            {
                var filePath = Path.Combine(_basePath, PatientsFileName);

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Patients data file not found at: {filePath}");
                }

                var jsonContent = await File.ReadAllTextAsync(filePath);
                var patients = JsonSerializer.Deserialize<List<PatientModel>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return patients ?? new List<PatientModel>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load patients data", ex);
            }
        }

        public async Task<List<PatientDetailsModel>> LoadPatientDetailsAsync()
        {
            try
            {
                var filePath = Path.Combine(_basePath, PatientDetailsFileName);

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Patient details data file not found at: {filePath}");
                }

                var jsonContent = await File.ReadAllTextAsync(filePath);
                var patientDetails = JsonSerializer.Deserialize<List<PatientDetailsModel>>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return patientDetails ?? new List<PatientDetailsModel>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to load patient details data", ex);
            }
        }

        public async Task<PatientDetailsModel> GetPatientDetailsByIdAsync(string patientId)
        {
            try
            {
                var allDetails = await LoadPatientDetailsAsync();
                return allDetails.FirstOrDefault(p => p.PatientId == patientId) ?? new PatientDetailsModel();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get patient details for ID: {patientId}", ex);
            }
        }
    }
}
