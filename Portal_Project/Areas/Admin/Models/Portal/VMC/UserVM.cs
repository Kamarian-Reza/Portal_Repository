using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Areas.Admin.Models.Portal.VMC
{
    public class UserVM
    {
        public string Id { get; set; }

        [Display(Name = "National ID")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(10, ErrorMessage = "{0} Lenght Must Be 10 Character Lenght", MinimumLength = 10)]
        public string NationalID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(100, ErrorMessage = "{0} Lenght Must Be 100 Character Maximume")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(100, ErrorMessage = "{0} Lenght Must Be 100 Character Maximume")]
        public string LastName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(100, ErrorMessage = "{0} Lenght Must Be 100 Character Maximume")]
        public string Password { get; set; }
    }
}