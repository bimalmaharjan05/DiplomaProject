using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
    public class AccountAddRoleViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
