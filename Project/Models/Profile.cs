using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Profile
    {
        public int ProfileId { get; set; }//PK

        [Required]
        public string UserId { get; set; }//logicaly is a FK

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        // one customer with many addresses
        public IEnumerable<Address> Addresses { get; set; }
    }
}
