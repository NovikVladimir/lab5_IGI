using System.Linq;
using lab5.Models;

namespace lab5.ViewModels
{
    public class PatientsViewModel
    {
        public Patient PatientViewModel { get; set; }
        public IQueryable<Patient> PageViewModel { get; set; }
        public PageViewModel Pages { get; set; }
        public int PageNumber { get; set; }     
    }
}
