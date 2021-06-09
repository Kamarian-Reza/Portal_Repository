using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal.VMC
{
    public class LoginVM
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter {0}")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me!")]
        public bool RememberMe { get; set; }
    }
}