using System.Linq;
using lab5.Models;

namespace lab5.ViewModels
{
    public class DiseasesViewModel
    {
        public Disease DiseaseViewModel { get; set; }
        public IQueryable<Disease> PageViewModel { get; set; }
        public PageViewModel Pages { get; set; }
        public int PageNumber { get; set; }
        public enum SortState
        {
            No,
            NameAsc,
            NameDesc,
        }
    }
}
