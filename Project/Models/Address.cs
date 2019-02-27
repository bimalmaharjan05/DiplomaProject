using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        //FK - one customer with many address
        public int ProfileId { get; set; }
    }
}
