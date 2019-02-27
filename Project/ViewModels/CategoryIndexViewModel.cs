using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class CategoryIndexViewModel
    {
        public int Total { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
