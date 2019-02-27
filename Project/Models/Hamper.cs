using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public string HamperCategory { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public string HamperDetails { get; set; }
        public bool Discontinued { get; set; }
        //one category has many hampers
        public int CategoryId { get; set; }

        public List<OrderLineItem> Orders { get; set; }
    }
}
