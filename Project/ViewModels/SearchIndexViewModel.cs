using Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class SearchIndexViewModel
    {
        //[Required(ErrorMessage ="Please enter category name")]
        public string CategoryName { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
    }
}
