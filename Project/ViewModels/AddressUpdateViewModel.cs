using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class AddressUpdateViewModel
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int PostCode { get; set; }
        public int ProfileId { get; set; }
    }
}
