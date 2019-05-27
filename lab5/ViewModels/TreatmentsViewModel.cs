using System.Linq;
using lab5.Models;

namespace lab5.ViewModels
{
    public class TreatmentsViewModel
    {
        public Treatment TreatmentViewModel { get; set; }
        public IQueryable<Treatment> PageViewModel { get; set; }
        public PageViewModel Pages { get; set; }
        public int PageNumber { get; set; }
    }
}
