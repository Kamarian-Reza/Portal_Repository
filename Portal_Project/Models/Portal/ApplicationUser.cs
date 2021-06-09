using Microsoft.AspNetCore.Identity;
using Portal_Project.Models.Portal.DMC;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(100, ErrorMessage = "{0} Lenght Must Be 100 Character Maximume")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(100, ErrorMessage = "{0} Lenght Must Be 100 Character Maximume")]
        public string LastName { get; set; }

        // Navigation Properties
        public IEnumerable<Product> Products { get; set; }
    }
}