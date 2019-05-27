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
    public class DiseaseController : Controller
    {
        private int pageSize = 5;
        private Context _context;
        private Disease _disease = new Disease
        {
            DiseaseName = "",
            DiseaseSymptoms = "",
            DiseaseDuration = "",
            DiseaseConsequences = ""
        };

        public DiseaseController(Context diseaseContext)
        {
            _context = diseaseContext;
        }

        [HttpGet]
        public IActionResult Index(int page, SortState sortOrder)
        {
            Disease sessionDisease = HttpContext.Session.GetObject<Disease>("Disease");
            string sessionSortState = HttpContext.Session.GetString("SortStateDisease");

            if (sessionDisease != null)
            {
                _disease = sessionDisease;
            }

            if (sessionSortState != null)
                if (sortOrder == SortState.No)
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);
            ViewData["NameSort"] = sortOrder == SortState.NameDesc ? SortState.NameAsc : SortState.NameDesc;
            HttpContext.Session.SetString("SortStateDisease", sortOrder.ToString());

            IQueryable<Disease> diseases = Sort(_context.Diseases, sortOrder,
                _disease.DiseaseName, (int)page);
            PageViewModel pageViewModel = new PageViewModel(NoteCount(_context.Diseases,
                _disease.DiseaseName), page, pageSize);
            DiseasesViewModel diseasesView = new DiseasesViewModel
            {
                DiseaseViewModel = _disease,
                PageViewModel = diseases,
                Pages = pageViewModel
            };

            return View(diseasesView);
        }

        [HttpPost]
        public IActionResult Index(Disease disease, int page)
        {
            var sessionSortState = HttpContext.Session.GetString("SortStateDisease");
            SortState sortOrder = new SortState();
            if (sessionSortState != null)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState);
            
            IQueryable<Disease> diseases = Sort(_context.Diseases, sortOrder,
                disease.DiseaseName, (int)page);
            HttpContext.Session.SetObject("Disease", disease);

            PageViewModel pageViewModel = new PageViewModel(NoteCount(_context.Diseases,
                   _disease.DiseaseName), page, pageSize);

            DiseasesViewModel diseasesView = new DiseasesViewModel
            {
                DiseaseViewModel = disease,
                PageViewModel = diseases,
                Pages = pageViewModel
            };

            return View(diseasesView);
        }

        private IQueryable<Disease> Sort(IQueryable<Disease> diseases,
            SortState sortOrder, string name, int page)
        {
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    diseases = diseases.OrderBy(s => s.DiseaseName);
                    break;
                case SortState.NameDesc:
                    diseases = diseases.OrderByDescending(s => s.DiseaseName);
                    break;
            }
            diseases = diseases.Where(o => o.DiseaseName.Contains(name ?? ""))
                .Skip(page * pageSize).Take(pageSize);
            return diseases;
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = _context.Diseases.Where(p => p.DiseaseID == id).Single();
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disease = _context.Diseases.Where(p => p.DiseaseID == id).Single();
            if (disease == null)
            {
                return NotFound();
            }

            return View(disease);
        }

        [HttpPost]
        public IActionResult Edit(Disease disease)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disease);
                    _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseaseExists(disease.DiseaseID))
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
            return View(disease);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var disease = _context.Diseases.Where(p => p.DiseaseID == id).FirstOrDefault(); ;
            if (disease == null)
            {
                return NotFound();
            }

            try
            {
                _context.Diseases.Remove(disease);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(disease.DiseaseID))
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
        public async Task<IActionResult> Add(Disease disease)
        {
            if (disease == null)
            {
                return View();
            }

            try
            {
                _context.Diseases.Add(disease);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseExists(disease.DiseaseID))
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

        private bool DiseaseExists(int id)
        {
            return _context.Diseases.Any(e => e.DiseaseID == id);
        }

        private int NoteCount(IQueryable<Disease> diseases, string name)
        {
            return diseases.Where(o => o.DiseaseName.Contains(name ?? ""))
                .Count();
        }
    }
}