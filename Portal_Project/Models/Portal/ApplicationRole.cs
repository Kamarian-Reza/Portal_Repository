using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal
{
    public class ApplicationRole : IdentityRole
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [StringLength(100, ErrorMessage = "{0} Lenght Must Be 100 Character Maximume")]
        public string Title { get; set; }
    }
}