using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portal_Model.Models
{
    public class Bid
    {
        public Bid(int bidId, int productId, string userId, decimal price, Bid_Mode_Enum mode)
        {
            BidID = bidId;
            ProductID = productId;
            UserID = userId;
            Price = price;
            Mode = mode;
        }

        List<string> brokenRules = new List<string>();

        public IReadOnlyList<string> IsValid()
        {
            // Bid ID
            if ((Mode != Bid_Mode_Enum.Create) && (BidID <= 0))
                brokenRules.Add("Invalid BidID");

            if ((Mode == Bid_Mode_Enum.Create) && (BidID != 0))
                brokenRules.Add("Invalid BidID");

            // User ID
            if (string.IsNullOrEmpty(UserID))
                brokenRules.Add("Invalid UserID");

            // Price
            if (Price < 0)
                brokenRules.Add("Bid price coud not be negative");

            return brokenRules;
        }

        public int BidID { get; private set; }

        public int ProductID { get; private set; }

        public string UserID { get; private set; }

        public decimal Price { get; private set; }
        public Bid_Mode_Enum Mode { get; private set; }

        // Navigation Properties
    }

    public enum Bid_Mode_Enum
    {
        [Display(Name = "Create")]
        Create = 1,

        [Display(Name = "Update")]
        Update = 2,

        [Display(Name = "Read")]
        Read = 3
    }
}