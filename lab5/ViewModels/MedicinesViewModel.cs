using System.Linq;
using lab5.Models;

namespace lab5.ViewModels
{
    public class MedicinesViewModel
    {
        public Medicine MedicineViewModel { get; set; }
        public IQueryable<Medicine> PageViewModel { get; set; }
        public PageViewModel Pages { get; set; }
        public int PageNumber { get; set; }
    }
    public enum SortState
    {
        No,
        NameAsc,
        NameDesc
    }
}
