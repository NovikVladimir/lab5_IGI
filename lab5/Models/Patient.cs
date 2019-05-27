using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lab5.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        [Display(Name = "Имя")]
        public string PatientName { get; set; }
        [Display(Name = "Возраст")]
        public int PatientAge { get; set; }
        [Display(Name = "Пол")]
        public string PatientGender { get; set; }
        [Display(Name = "Адрес")]
        public string PatientAdress { get; set; }
        [Display(Name = "Телефон")]
        public string PatientTelephone { get; set; }
        [Display(Name = "Дата госпитализации")] 
        public string PatientDateOfHospitalization { get; set; }
        [Display(Name = "Дата выписки")]
        public string PatientDischargeDate { get; set; }
        [Display(Name = "Болезнь")]
        public string PatientDisease { get; set; }
        [Display(Name = "Отделение")]
        public string PatientDepartment { get; set; }
        [Display(Name = "Врач")]
        public string PatientAttendingPhysician { get; set; }
        [Display(Name = "Результаты лечения")]
        public string PatientResultOfTreatment { get; set; }
        [Display(Name = "DiseaseID")]
        public int? DiseaseID { get; set; }
        public Disease Disease { get; set; }

        public Patient() { }

        public Patient(int PatientID, string PatientName, int PatientAge, string PatientGender, string PatientAdress,
            string PatientTelephone, string PatientDateOfHospitalization, string PatientDischargeDate, string PatientDisease,
            string PatientDepartment, string PatientAttendingPhysician, string PatientResultOfTreatment, int? diseaseID)
        {
            this.PatientID = PatientID;
            this.PatientName = PatientName;
            this.PatientAge = PatientAge;
            this.PatientGender = PatientGender;
            this.PatientAdress = PatientAdress;
            this.PatientTelephone = PatientTelephone;
            this.PatientDateOfHospitalization = PatientDateOfHospitalization;
            this.PatientDischargeDate = PatientDischargeDate;
            this.PatientDisease = PatientDisease;
            this.PatientDepartment = PatientDepartment;
            this.PatientAttendingPhysician = PatientAttendingPhysician;
            this.PatientResultOfTreatment = PatientResultOfTreatment;
            this.DiseaseID = diseaseID;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Patient;

            if (obj == null)
            {
                return false;
            }
            if (obj == this)
            {
                return true;
            }

            return this.PatientID == item.PatientID;
        }

        public override int GetHashCode()
        {
            return this.PatientID.GetHashCode();
        }
    }
}
