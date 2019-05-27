using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using lab5.Models;
using lab5.ViewModels;
using lab5.Services;
using lab5.Filters;

namespace lab5.Controllers
{
    [CatchExceptionFilter]
    public class HomeController : Controller
    {
        //private IMemoryCache _cache;
        public HomeController()
        {
            //_cache = memoryCache;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View(homeViewModel);
        }

        public IActionResult ToError()
        {
            return View("~/Views/Home/About.cshtml");
        }

       // [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult About3()
        {

            HomeViewModel homeViewModel = TakeLast.GetHomeViewModel();
            return View("~/Views/Home/About.cshtml", homeViewModel);
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Disease()
        {
            if (HttpContext.Session.Keys.Contains("name"))
            {
                ViewBag.DiseaseName = HttpContext.Session.GetString("name");
            }
            if (HttpContext.Session.Keys.Contains("symptoms"))
            {
                ViewBag.DiseaseSymptoms = HttpContext.Session.GetString("symptoms");
            }
            if (HttpContext.Session.Keys.Contains("duration"))
            {
                ViewBag.DiseaseDuration = HttpContext.Session.GetString("duration");
            }
            if (HttpContext.Session.Keys.Contains("consequences"))
            {
                ViewBag.DiseaseConsequences = HttpContext.Session.GetString("consequences");
            }
            return View("~/Views/Disease/Index.cshtml");
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Treatment()
        {
            return View("~/Views/Treatment/Index.cshtml");
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Patient()
        {
            return View("~/Views/Patient/Index.cshtml");
        }
        [HttpGet]
        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Medicine()
        {
            string medicineName = "", medicineDosage = "";
            if (HttpContext.Request.Cookies.ContainsKey("medicineName"))
            {
                medicineName = HttpContext.Request.Cookies["medicineName"];
            }
            if (HttpContext.Request.Cookies.ContainsKey("medicineDosage"))
            {
                medicineDosage = HttpContext.Request.Cookies["medicineDosage"];
            }
            ViewBag.MedicineName = medicineName;
            ViewBag.MedicineDosage = medicineDosage;
            return View("~/Views/Medicine/Index.cshtml");
        }

        [HttpPost]
        public string Disease(string name, string symptoms,
            string duration, string consequences)
        {
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("symptoms", symptoms);
            HttpContext.Session.SetString("duration", duration);
            HttpContext.Session.SetString("consequences", consequences);
            return "Болезнь \"" + name + "\"" + " c симптомами \"" + symptoms +
                 "\" и последствиями \"" + consequences + "\" добавлена в базу";
        }

        [HttpPost]
        public string Treatment(string nameDisease, string nameMedicine, string duration)
        {
            return "Лечение болезни \"" + nameDisease + "\" с помощью лекарства \"" + nameMedicine + "\" добавлено в базу. \nДлительность лечения: \"" + duration + "\"";
        }

        [HttpPost]
        public string Medicine(string medicineName, string medicineManufacturer, string medicineDosage)
        {
            //HttpContext.Response.Cookies.Append("medicineName", medicineName);
            //HttpContext.Response.Cookies.Append("medicineManufacturer", medicineManufacturer);
            //HttpContext.Response.Cookies.Append("medicineDosage", medicineDosage);
            return "Лекарство \"" + medicineName + "\" от \"" + medicineManufacturer + "\" добавлено в базу.\nДозировка: \"" + medicineDosage + "\"";
        }

        [HttpPost]
        public string Patient(string name, string disease, string physician)
        {
            return "Пациент: " + name + ". Болезнь: " + disease + ". Лечащий врач: " + physician;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
