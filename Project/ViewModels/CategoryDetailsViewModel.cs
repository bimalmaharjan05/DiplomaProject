using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public int Total { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }

    }
}
