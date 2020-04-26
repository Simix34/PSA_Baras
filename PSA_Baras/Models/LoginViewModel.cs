using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSA_Baras.Models
{
    public class LoginViewModel
    {
        [Required]
        public string login { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string password { get; set; }
    }
}
