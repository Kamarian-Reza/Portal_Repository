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
        public Product(int productId,
                       string userId,
                       string name,
                       decimal basePrice,
                       DateTime createDate,
                       Product_Status_Enum status,
                       Product_Mode_Enum mode)
        {
            ProductID = productId;
            UserID = userId;
            Name = name;
            BasePrice = basePrice;
            CreateTime = mode == Product_Mode_Enum.Create ? DateTime.Now : createDate;
            Status = status;
            Mode = mode;
        }

        List<string> brokenRules = new List<string>();

        public IReadOnlyList<string> IsValid()
        {
            // Product ID
            if ((Mode != Product_Mode_Enum.Create) && (ProductID <= 0))
                brokenRules.Add("Invalid ProductID");

            if ((Mode == Product_Mode_Enum.Create) && (ProductID != 0))
                brokenRules.Add("Invalid ProductID");

            // User ID
            if (string.IsNullOrEmpty(UserID))
                brokenRules.Add("Invalid UserID");

            // Name
            if (string.IsNullOrEmpty(Name))
                brokenRules.Add("Product name can not be null / empty");

            if (!string.IsNullOrEmpty(Name) && Name.Length > 50)
                brokenRules.Add("Product name lengh coud not be more than 50 charachters");

            // Price
            if (BasePrice < 0)
                brokenRules.Add("Product base price coud not be negative");

            if (BasePrice > 1000)
                brokenRules.Add("Product base price coud not more than 1000");

            // Status
            if ((Mode == Product_Mode_Enum.Create) && (Status != Product_Status_Enum.OnSale))
                brokenRules.Add("On create product, status must be On Sale");

            return brokenRules;
        }

        public IReadOnlyList<string> Sold()
        {
            if (Status == Product_Status_Enum.OnSale)
            {
                Status = Product_Status_Enum.Sold;
            }
            else
            {
                brokenRules.Add("You cant sold the product/s that has already Solded / canceled");
            }

            return brokenRules;
        }

        public IReadOnlyList<string> Cancel()
        {
            if (DateTime.Now > CreateTime.AddHours(24))
            {
                brokenRules.Add("On create product, status must be On Sale");
            }
            else if (Status != Product_Status_Enum.OnSale)
            {
                brokenRules.Add("You cant cancel the product/s that has already Solded / canceled");
            }
            else
            {
                Status = Product_Status_Enum.Canceled;
            }

            return brokenRules;
        }

        public int ProductID { get; private set; }
        public string UserID { get; private set; }
        public string Name { get; private set; }
        public decimal BasePrice { get; private set; }
        public DateTime CreateTime { get; private set; }
        public Product_Status_Enum Status { get; private set; }
        public Product_Mode_Enum Mode { get; private set; }
        
        // Navigation Properties
    }

    public enum Product_Status_Enum
    {
        [Display(Name = "On Sale")]
        OnSale = 1,

        [Display(Name = "Sold")]
        Sold = 2,

        [Display(Name = "Canceled")]
        Canceled = 3
    }

    public enum Product_Mode_Enum
    {
        [Display(Name = "Create")]
        Create = 1,

        [Display(Name = "Update")]
        Update = 2,

        [Display(Name = "Read")]
        Read = 3
    }
}