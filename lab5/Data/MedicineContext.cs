using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Data
{
    public class MedicineContext
    {
        public static List<Medicine> GetAll()
        {
            List<Medicine> all = new List<Medicine>();
            using (Context db = new Context())
            {
                all = db.Medicines.ToList();
            }

            return all;
        }

        public static List<Medicine> GetPage(int pageNumber, int sizeOfPage)
        {
            List<Medicine> all = new List<Medicine>();
            using (Context db = new Context())
            {
                all = db.Medicines.Include(t => t.MedicineName).
                    Skip(pageNumber * sizeOfPage).Take(sizeOfPage).ToList();
            }
            return all;
        }

        public static void AddMedicine(Medicine medicine)
        {
            using (Context db = new Context())
            {
                db.Medicines.Add(medicine);
                db.SaveChanges();
            }
        }

        public static void UpdataMedicine(Medicine medicine)
        {
            using (Context db = new Context())
            {
                if (medicine != null)
                {
                    db.Medicines.Update(medicine);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteMedicine(Medicine medicine)
        {
            using (Context db = new Context())
            {
                if (medicine != null)
                {
                    db.Medicines.Remove(medicine);
                    db.SaveChanges();
                }
            }
        }

        public static List<Medicine> FindMedicine(string medicineName, string medicineIndications,
            string medicineManufacturer, string medicineDosage)
        {
            List<Medicine> medicine = new List<Medicine>();
            using (Context db = new Context())
            {
                if (medicineName != null)
                {
                    medicine = db.Medicines.Where(k => k.MedicineName == medicineName).ToList();
                }
                if (medicineIndications != null)
                {
                    if (medicine.Count != 0)
                    {
                        medicine = medicine.Where(k => k.MedicineIndications == medicineIndications).ToList();
                    }
                    else
                    {
                        medicine = db.Medicines.Where(k => k.MedicineIndications == medicineIndications).ToList();
                    }
                }
                if (medicineManufacturer != null)
                {
                    if (medicine.Count != 0)
                    {
                        medicine = medicine.Where(k => k.MedicineManufacturer == medicineManufacturer).ToList();
                    }
                    else
                    {
                        medicine = db.Medicines.Where(k => k.MedicineManufacturer == medicineManufacturer).ToList();
                    }
                }
                if (medicineDosage != null)
                {
                    if (medicine.Count != 0)
                    {
                        medicine = medicine.Where(k => k.MedicineDosage == medicineDosage).ToList();
                    }
                    else
                    {
                        medicine = db.Medicines.Where(k => k.MedicineDosage == medicineDosage).ToList();
                    }
                }
            }
            return medicine;
        }

        public static Medicine FindMedicineById(int id)
        {
            Medicine medicine = null;
            using (Context db = new Context())
            {
                medicine = db.Medicines.Where(k => k.MedicineID == id).ToList().FirstOrDefault();
            }
            return medicine;
        }
    }
}
