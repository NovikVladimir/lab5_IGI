using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Data
{
    public class TreatmentsContext
    {
        public static List<Treatment> GetAll()
        {
            List<Treatment> all = new List<Treatment>();
            using (Context db = new Context())
            {
                all = db.Treatments.ToList();
            }

            return all;
        }

        public static List<Treatment> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Treatment> all = new List<Treatment>();
            using (Context db = new Context())
            {
                all = db.Treatments.Include(t => t.TreatmentDisease).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddTreatment(Treatment treatmentToAdd)
        {
            using (Context db = new Context())
            {
                db.Treatments.Add(treatmentToAdd);
                db.SaveChanges();
            }
        }

        public static void UpdataTreatment(Treatment treatment)
        {
            using (Context db = new Context())
            {
                if (treatment != null)
                {
                    db.Treatments.Update(treatment);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteTreatment(Treatment treatmentToDelete)
        {
            using (Context db = new Context())
            {
                if (treatmentToDelete != null)
                {
                    db.Treatments.Remove(treatmentToDelete);
                    db.SaveChanges();
                }
            }
        }

        public static List<Treatment> FindTreatment(string treatmentDisease, string treatmentMedication)
        {
            List<Treatment> treatments = new List<Treatment>();
            using (Context db = new Context())
            {
                if (treatmentDisease != null && treatmentDisease != "")
                {
                    treatments = db.Treatments.Where(k => k.TreatmentDisease == treatmentDisease).ToList();
                }
                if (treatmentMedication != null)
                {
                    if (treatments.Count != 0)
                    {
                        treatments = treatments.Where(k => k.TreatmentMedication == treatmentMedication).ToList();
                    }
                    else
                    {
                        treatments = db.Treatments.Where(k => k.TreatmentMedication == treatmentMedication).ToList();
                    }
                }
            }
            return treatments;
        }

        public static Treatment FindTreatment(int id)
        {
            Treatment treatment = null;
            using (Context db = new Context())
            {
                treatment = db.Treatments.Where(k => k.TreatmentID == id).ToList().FirstOrDefault();
            }
            return treatment;
        }
    }
}
