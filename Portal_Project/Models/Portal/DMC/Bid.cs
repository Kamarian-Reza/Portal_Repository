using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal.DMC
{
    public class Bid
    {
        [Key]
        [Display(Name = "Bid ID")]
        public int BidID { get; set; }

        [Display(Name = "Product ID")]
        [Required(ErrorMessage = "Please select product")]
        public int ProductID { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "Please select user")]
        public string UserID { get; set; }

        [Display(Name = "Proposed Price")]
        [Required(ErrorMessage = "Please enter proposed price")]
        public decimal Price { get; set; }

        // Navigation Properties
        [ForeignKey("ProductID")]
        [Display(Name = "Product ID")]
        public Product Product { get; set; }

        [ForeignKey("UserID")]
        [Display(Name = "User ID")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}