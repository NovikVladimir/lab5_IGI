using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using lab5.Data;
using lab5.Models;
using lab5.ViewModels;
using Microsoft.AspNetCore.Authorization;
using lab5.Filters;

namespace lab5.Controllers
{
    [CatchExceptionFilter]
    [Authorize(Roles = "user, admin")]
    public class TreatmentController : Controller
    {
        private int pageSize = 5;
        private Context db;
        private Treatment _treatment = new Treatment
        {
            TreatmentDisease = "",
            TreatmentMedication = "",
            TreatmentDate = "",
            TreatmentDosage = "",
            TreatmentDurationOfTreatment = "",
            DiseaseID = null,
            Disease = null,
            MedicineID = null,
            Medicine = null
        };

        public TreatmentController(Context TreatmentContext)
        {
            db = TreatmentContext;
        }

        [HttpGet]
        public IActionResult Index(SortState sortOrder)
        {
            Treatment sessionTreatment = HttpContext.Session.GetObject<Treatment>("Treatment");
            string sessionSortState = HttpContext.Session.GetString("SortStateTreatment");
            int? page = HttpContext.Session.GetInt32("TreatmentPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("TreatmentPage", 0);
            }

            if (sessionTreatment != null)
            {
                _treatment = sessionTreatment;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortState", sortOrder.ToString());
            IQueryable<Treatment> Treatments = Sort(db.Treatments, sortOrder,
                _treatment.TreatmentDisease, (int)page);
            foreach (var item in Treatments)
            {
                item.Disease = db.Diseases.Where(o => o.DiseaseID == item.DiseaseID).FirstOrDefault();
                item.Medicine = db.Medicines.Where(o => o.MedicineID == item.MedicineID).FirstOrDefault();
            }
            TreatmentsViewModel TreatmentsView = new TreatmentsViewModel
            {
                TreatmentViewModel = _treatment,
                PageViewModel = Treatments,
                PageNumber = (int)page
            };

            return View(TreatmentsView);
        }

        [HttpPost]
        public IActionResult Index(Treatment treatment)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateTreatment");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            int? page = HttpContext.Session.GetInt32("TreatmentPage");
            if (page == null)
            {
                page = 0;
                HttpContext.Session.SetInt32("TreatmentPage", 0);
            }

            IQueryable<Treatment> treatments = Sort(db.Treatments, sortOrder,
                treatment.TreatmentDisease, (int)page);
            HttpContext.Session.SetObject("Treatment", treatment);

            TreatmentsViewModel treatmentsView = new TreatmentsViewModel
            {
                TreatmentViewModel = treatment,
                PageViewModel = treatments,
                PageNumber = (int)page
            };

            return View(treatmentsView);
        }

        private IQueryable<Treatment> Sort(IQueryable<Treatment> Treatments,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    Treatments = Treatments.OrderBy(s => s.TreatmentDisease);
                    break;
                case SortState.NameDesc:
                    Treatments = Treatments.OrderByDescending(s => s.TreatmentDisease);
                    break;
            }
            Treatments = Treatments.Where(o => o.TreatmentDisease.Contains(name ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return Treatments;
        }
    }
}