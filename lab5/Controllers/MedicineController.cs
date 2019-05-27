using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using lab5.Data;
using lab5.Models;
using lab5.ViewModels;
using lab5.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace lab5.Controllers
{
    [CatchExceptionFilter]
    [Authorize(Roles = "user, admin")]
    public class MedicineController : Controller
    {
        private int pageSize = 5;
        private Context _context;
        private Medicine _medicine = new Medicine
        {
            MedicineName = "",
            MedicineIndications = "",
            MedicineContraindications = "",
            MedicineManufacturer = "",
            MedicinePackaging = "",
            MedicineDosage = ""
        };

        public MedicineController(Context medicineContext) {
            _context = medicineContext;
        }

        [HttpGet]
        public IActionResult Index(int page, SortState sortOrder)
        {
            Medicine sessionMedicine = HttpContext.Session.GetObject<Medicine>("Medicine");
            string sessionSortState = HttpContext.Session.GetString("SortStateMedicine");

            if (sessionMedicine != null)
            {
                _medicine = sessionMedicine;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);
            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortStateMedicine", sortOrder.ToString());

            IQueryable<Medicine> medicines = Sort(_context.Medicines, sortOrder,
                _medicine.MedicineName, (int)page);
            PageViewModel pageViewModel = new PageViewModel(NoteCount(_context.Medicines,
                _medicine.MedicineName), page, pageSize);
            MedicinesViewModel medicinesView = new MedicinesViewModel
            {
                MedicineViewModel = _medicine,
                PageViewModel = medicines,
                Pages = pageViewModel
            };

            return View(medicinesView);
        }

        [HttpPost]
        public IActionResult Index(Medicine medicine, int page)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateMedicine");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);

            IQueryable<Medicine> medicines = Sort(_context.Medicines, sortOrder,
                _medicine.MedicineName, (int)page);
            PageViewModel pageViewModel = new PageViewModel(NoteCount(_context.Medicines,
                _medicine.MedicineName), page, pageSize);
            MedicinesViewModel medicinesView = new MedicinesViewModel
            {
                MedicineViewModel = _medicine,
                PageViewModel = medicines,
                PageNumber = page
            };

            return View(medicinesView);
        }

        private IQueryable<Medicine> Sort(IQueryable<Medicine> medicines,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    medicines = medicines.OrderBy(s => s.MedicineName);
                    break;
                case SortState.NameDesc:
                    medicines = medicines.OrderByDescending(s => s.MedicineName);
                    break;
            }
            medicines = medicines.Where(o => o.MedicineName.Contains(name ?? "")).Skip(page * pageSize).Take(pageSize);
            return medicines;
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = _context.Medicines.Where(p => p.MedicineID == id).Single();
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicine = _context.Medicines.Where(p => p.MedicineID == id).Single();
            if (medicine == null)
            {
                return NotFound();
            }

            return View(medicine);
        }

        [HttpPost]
        public IActionResult Edit(Medicine medicine)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicine);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicineExists(medicine.MedicineID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(medicine);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var medicine = _context.Medicines.Where(p => p.MedicineID == id).FirstOrDefault(); ;
            if (medicine == null)
            {
                return NotFound();
            }

            try
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(medicine.MedicineID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Medicine medicine)
        {
            if (medicine == null)
            {
                return View();
            }

            try
            {
                _context.Medicines.Add(medicine);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(medicine.MedicineID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.MedicineID == id);
        }

        private int NoteCount(IQueryable<Medicine> medicines, string name)
        {
            return medicines.Where(o => o.MedicineName.Contains(name ?? ""))
                .Count();
        }
    }
}