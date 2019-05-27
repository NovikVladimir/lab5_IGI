using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentID { get; set; }
        [Display(Name = "Болезнь")]
        public string TreatmentDisease { get; set; }
        [Display(Name = "Лекарство")]
        public string TreatmentMedication { get; set; }
        [Display(Name = "Дата")]
        public string TreatmentDate { get; set; }
        [Display(Name = "Доза")]
        public string TreatmentDosage { get; set; }
        [Display(Name = "Длительность лечения")]
        public string TreatmentDurationOfTreatment { get; set; }
        [Display(Name = "DiseaseID")]
        public int? DiseaseID { get; set; }
        [Display(Name = "MedicineID")]
        public int? MedicineID { get; set; }
        [Display(Name = "Болезнь")]
        public Disease Disease { get; set; }
        [Display(Name = "Лекарство")]
        public Medicine Medicine { get; set; }

        public Treatment() { }

        public Treatment(string TreatmentDisease, string TreatmentMedication, string TreatmentDate, string TreatmentDosage,
             string TreatmentDurationOfTreatment, int? diseaseID, Disease disease, int? medicineID, Medicine medicine)
        {
            this.TreatmentDisease = TreatmentDisease;
            this.TreatmentMedication = TreatmentMedication;
            this.TreatmentDate = TreatmentDate;
            this.TreatmentDosage = TreatmentDosage;
            this.TreatmentDurationOfTreatment = TreatmentDurationOfTreatment;
            this.DiseaseID = diseaseID;
            this.Disease = disease;
            this.MedicineID = medicineID;
            this.Medicine = medicine;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Treatment;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.TreatmentID == item.TreatmentID;
        }

        public override int GetHashCode()
        {
            return this.TreatmentID.GetHashCode();
        }
    }
}
