using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Model.Models
{
    public class Product
    {
        public Product(string name, int price)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("You can not pass empty string value to product name");
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("Product name lenght can not be more than 50 characters");
            }

            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("Product price can not be negative.");
            }

            if (price > 1000)
            {
                throw new ArgumentOutOfRangeException("Product price can not be more than 1000$.");
            }

            Name = name;
            BasePrice = price;
            CreateTime = DateTime.Now;
            Status = Product_Status_Enum.OnSale;
        }

        public void ProductSaled()
        {
            Status = Product_Status_Enum.Saled;
        }

        public void ProductCanceled()
        {
            if (DateTime.Now > CreateTime.AddDays(1))
            {
                throw new Exception("You cand cancel your product, after 24h!");
            }
            
            Status = Product_Status_Enum.Canceled;
        }

        public int ProductID { get; set; }
        public string UserID { get; set; }
        public string Name { get; private set; }
        public decimal BasePrice { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Product_Status_Enum Status { get; private set; }

        // Navigation Properties
    }

    public enum Product_Status_Enum
    {
        [Display(Name = "On Sale")]
        OnSale = 1,

        [Display(Name = "Saled")]
        Saled = 2,

        [Display(Name = "Canceled")]
        Canceled = 3
    }
}