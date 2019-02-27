using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class OrderLineItem
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Hamper Hamper { get; set; }
        public int HamperId { get; set; }
        public int Quantity { get; set; }
    }
}
