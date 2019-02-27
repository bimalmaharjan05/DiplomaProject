using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class OrderIndexViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int AddressId { get; set; }
        public List<OrderLineItem> Hampers { get; set; }
    }
}
