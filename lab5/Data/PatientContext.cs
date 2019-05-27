using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Data
{
    public class PatientContext
    {
        public static List<Patient> GetAll()
        {
            List<Patient> all = new List<Patient>();
            using (Context db = new Context())
            {
                all = db.Patients.ToList();
            }

            return all;
        }

        public static List<Patient> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Patient> all = new List<Patient>();
            using (Context db = new Context())
            {
                all = db.Patients.Include(t => t.PatientName).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddPatient(Patient patientToAdd)
        {
            using (Context db = new Context())
            {
                db.Patients.Add(patientToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataPatient(Patient patient)
        {
            using (Context db = new Context())
            {
                if (patient != null)
                {
                    db.Patients.Update(patient);
                    db.SaveChanges();
                }
            }
        }

        public static void DeletePatient(Patient patientToDelete)
        {
            using (Context db = new Context())
            {
                if (patientToDelete != null)
                {
                    db.Patients.Remove(patientToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<Patient> FindPatients(int? patientID, string sex,
            string phone, string date)
        {
            List<Patient> patients = new List<Patient>();
            using (Context db = new Context())
            {
                if (patientID != null)
                {
                    patients = db.Patients.Where(k => k.PatientID == patientID).ToList();
                }
                if (sex != null)
                {
                    if (patients.Count != 0)
                    {
                        patients = patients.Where(k => k.PatientGender == sex).ToList();
                    }
                    else
                    {
                        patients = db.Patients.Where(k => k.PatientGender == sex).ToList();
                    }
                }
                if (phone != null)
                {
                    if (patients.Count != 0)
                    {
                        patients = patients.Where(k => k.PatientTelephone == phone).ToList();
                    }
                    else
                    {
                        patients = db.Patients.Where(k => k.PatientTelephone == phone).ToList();
                    }
                }
                if (date != null)
                {
                    if (patients.Count != 0)
                    {
                        patients = patients.Where(k => k.PatientDateOfHospitalization == date).ToList();
                    }
                    else
                    {
                        patients = db.Patients.Where(k => k.PatientDateOfHospitalization == date).ToList();
                    }
                }
            }
            return patients;
        }

        public static Patient FindPatientById(int id)
        {
            Patient patient = null;
            using (Context db = new Context())
            {
                patient = db.Patients.Where(k => k.PatientID == id).ToList().FirstOrDefault();
            }
            return patient;
        }
    }
}
