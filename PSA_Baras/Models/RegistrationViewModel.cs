using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSA_Baras.Models
{
    public class RegistrationViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        //[StringLength(15, ErrorMessage = "Name length can't be more than 15.")]
        public string login { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 6)]
        public string password { get; set; }
        [Required]
        [NotMapped] // Does not effect with your database
        [Compare("password")]
        public string confirmPassword { get; set; }
    }
}
