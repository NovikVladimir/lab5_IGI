using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using lab5.ViewModels;
using lab5.Models;
using lab5.Data;

namespace lab5.Services
{
    public class TakeLast
    {
        public static HomeViewModel GetHomeViewModel()
        {
            HomeViewModel homeViewModel = null;
            using (Context _context = new Context())
            {
                List<Disease> diseases = _context.Diseases.OrderByDescending(p => p.DiseaseID).Take(10).ToList();
                List<Medicine> medicines = _context.Medicines.OrderByDescending(p => p.MedicineID).Take(10).ToList();
                List<Treatment> treatments = _context.Treatments.OrderByDescending(p => p.TreatmentID).Take(10).ToList();
                List<Patient> patients = _context.Patients.OrderByDescending(p => p.PatientID).Take(10).ToList();
                homeViewModel = new HomeViewModel {
                    Patients = patients,
                    Diseases = diseases,
                    Medicines = medicines,
                    Treatments = treatments
                };
            }

            return homeViewModel;
        }
    }
}
