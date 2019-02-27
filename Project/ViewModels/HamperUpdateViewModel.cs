using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class HamperUpdateViewModel
    {
        public string HamperName { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public string HamperDetails { get; set; }
        public int HamperId { get; set; }
        public int CategoryId { get; set; }
        public bool Discontinued { get; set; }
    }
}
