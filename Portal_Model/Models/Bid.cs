using Portal_Model.Common;
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
        public Bid(int bidId, int productId, string userId, Money price, Bid_Mode_Enum mode)
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

            return brokenRules;
        }

        public int BidID { get; }

        public int ProductID { get; }

        public string UserID { get; }

        public Money Price { get; }
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