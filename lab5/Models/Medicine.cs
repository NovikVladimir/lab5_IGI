using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }
        [Display(Name = "Название")]
        public string MedicineName { get; set; }
        [Display(Name = "Показания")]
        public string MedicineIndications { get; set; }
        [Display(Name = "Противопоказания")]
        public string MedicineContraindications { get; set; }
        [Display(Name = "Производитель")]
        public string MedicineManufacturer { get; set; }
        [Display(Name = "Упаковка")]
        public string MedicinePackaging { get; set; }
        [Display(Name = "Доза")]
        public string MedicineDosage { get; set; }

        public ICollection<Treatment> Treatments { get; set; }

        public Medicine() { }

        public Medicine(int MedicineID, string MedicineName, string MedicineIndications, string MedicineContraindications, string MedicineManufacturer,
            string MedicinePackaging, string MedicineDosage)
        {
            this.MedicineID = MedicineID;
            this.MedicineName = MedicineName;
            this.MedicineIndications = MedicineIndications;
            this.MedicineContraindications = MedicineContraindications;
            this.MedicineManufacturer = MedicineManufacturer;
            this.MedicinePackaging = MedicinePackaging;
            this.MedicineDosage = MedicineDosage;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Medicine;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.MedicineID == item.MedicineID;
        }

        public override int GetHashCode()
        {
            return this.MedicineID.GetHashCode();
        }
    }
}
