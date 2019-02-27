using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.ViewModels
{
    public class AddressIndexViewModel
    { 
        public Profile Profile { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}
