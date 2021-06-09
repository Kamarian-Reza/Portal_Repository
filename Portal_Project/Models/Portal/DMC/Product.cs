using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Project.Models.Portal.DMC
{
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        [Display(Name = "User ID")]
        [Required(ErrorMessage = "Please enter {0}")]
        public string UserID { get; set; }

        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Please enter {0}")]
        [StringLength(100, ErrorMessage = "{0} lenght must be {1} character maximume")]
        public string Name { get; set; }

        [Display(Name = "Base Price")]
        [Required(ErrorMessage = "Please enter {0}")]
        public decimal BasePrice { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Please select {0}")]
        public Product_Status Status { get; set; }

        [Display(Name = "Status")]
        [NotMapped]
        public string StatusTitle
        {
            get
            {
                string result = null;

                switch (this.Status)
                {
                    case Product_Status.OnSale:
                        result = "On Sale";
                        break;

                    case Product_Status.Saled:
                        result = "Saled";
                        break;

                    case Product_Status.Canceled:
                        result = "Canceled";
                        break;
                }

                return result;
            }

            set
            {
                string result = null;

                switch (this.Status)
                {
                    case Product_Status.OnSale:
                        result = "On Sale";
                        break;

                    case Product_Status.Saled:
                        result = "Saled";
                        break;

                    case Product_Status.Canceled:
                        result = "Canceled";
                        break;
                }

                this.StatusTitle = result;
            }
        }


        // Navigation Properties
        [ForeignKey("UserID")]
        [Display(Name = "Product ID")]
        public ApplicationUser ApplicationUser { get; set; }
    }

    public enum Product_Status
    {
        OnSale = 1,
        Saled = 2,
        Canceled = 3
    }
}