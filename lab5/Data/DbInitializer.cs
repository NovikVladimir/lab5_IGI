using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using lab5.Models;

namespace lab5.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context db)
        {
            db.Database.EnsureCreated();

            if (db.Patients.Any())
            {
                return;  
            }

            Random rand = new Random();
            string[] medicine = { "Глицин", "Валидол", "Валерьянка", "Аскорбинка", "Мезим" };
            string[] medicineIndications = { "детям до 18", "детям после 18", "всем", "беременные", "студенты" };
            string[] medicineContraindications = { "пенсионеры", "беременные", "спокойные", "не беременные", "отсутствует" };
            string[] medicineManufacturers = { "ОАО \"Зорька\"", "ООО \"Красный желтый\"", "ОАО \"ЛИИТ\"", "ОАО \"ЗОЖ\"", "ОАО \"ГГТУ\"" };
            for (int i = 0; i < 30; i++)
            {
                db.Medicines.Add(new Medicine
                {
                    MedicineName = medicine[rand.Next(5)],
                    MedicineIndications = medicineIndications[rand.Next(5)],
                    MedicineContraindications = medicineContraindications[rand.Next(5)],
                    MedicineManufacturer = medicineManufacturers[rand.Next(5)],
                    MedicinePackaging = (rand.Next(10) + 1).ToString() + "шт.",
                    MedicineDosage = (rand.Next(10) + 1).ToString() + "шт. каждые " + (rand.Next(10) + 1).ToString() + "ч."
                });
            }
            db.SaveChanges();

            string[] diseases = { "Анемия", "ВИЧ", "Пневномия", "Грипп", "Ангина" };
            string[] symptoms = { "слабость", "никаких", "кашель", "насморк", "ослабление", "боль", "голос", "сыпь", "чувствительность", "температура" };
            for (int i = 0; i < 30; i++)
            {
                db.Diseases.Add(new Disease
                {
                    DiseaseName = diseases[rand.Next(1, 6) - 1],
                    DiseaseSymptoms = symptoms[rand.Next(1, 6) - 1] + " " + symptoms[rand.Next(1, 6) + 4],
                    DiseaseDuration = (rand.Next(50) + 1).ToString() + " дн.",
                    DiseaseConsequences = symptoms[rand.Next(5)]
                });
            }
            db.SaveChanges();

            string[] patientNames = { "Владимир", "Станислав", "Дмитрий", "Адам", "Никита" };
            string[] patientGenders = { "м", "ж" };
            string[] patientAdresses = { "пр. Октября", "пр. Речицкий", "ул. Барыкина", "пр. Ленина", "ул. Советская" };
            string[] patientAttendingPhysicians = { "П.О. Сухой", "К.С. Курочка", "Г.П. Косинов", "А.А. Саприко", "Н.В. Самовендюк" };
            string[] patientResultsOfTreatment = { "положительный", "отрицательный" };
            for (int i = 0; i < 30; i++)
            {
                int disID = rand.Next(1, 6) - 1;
                db.Patients.Add(new Patient
                {
                    PatientName = patientNames[rand.Next(5)],
                    PatientAge = rand.Next(10) + 10,
                    PatientGender = patientGenders[rand.Next(2)],
                    PatientAdress = patientAdresses[rand.Next(5)],
                    PatientTelephone = "+37533" + (rand.Next(8999999) + 100000).ToString(),
                    PatientDateOfHospitalization = "13.02.2019",
                    PatientDischargeDate = "18.03.2019",
                    PatientDisease = diseases[disID],
                    PatientDepartment = (rand.Next(5) + 1).ToString(),
                    PatientAttendingPhysician = patientAttendingPhysicians[rand.Next(5)],
                    PatientResultOfTreatment = patientResultsOfTreatment[rand.Next(2)],
                    DiseaseID = db.Diseases.Where(o => o.DiseaseName == diseases[disID]).First().DiseaseID,
                    Disease = db.Diseases.Where(o => o.DiseaseName == diseases[disID]).FirstOrDefault()
                });
            }
            db.SaveChanges();

            for (int i = 0; i < 30; i++)
            {
                int disID = rand.Next(1, 6) - 1;
                db.Treatments.Add(new Treatment
                {
                    TreatmentDisease = diseases[disID],
                    TreatmentMedication = medicine[disID],
                    TreatmentDate = "13.02.2019",
                    TreatmentDosage = (rand.Next(20) + 1).ToString() + "шт.",
                    TreatmentDurationOfTreatment = (rand.Next(20) + 1).ToString() + " дн.",
                    DiseaseID = db.Diseases.Where(o => o.DiseaseName == diseases[disID]).First().DiseaseID,
                    Disease = db.Diseases.Where(o => o.DiseaseName == diseases[disID]).FirstOrDefault(),
                    MedicineID = db.Medicines.Where(o => o.MedicineName == medicine[disID]).First().MedicineID,
                    Medicine = db.Medicines.Where(o => o.MedicineName == medicine[disID]).FirstOrDefault()
                });
            }
            db.SaveChanges();
        }
    }
}
