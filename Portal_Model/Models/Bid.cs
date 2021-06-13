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
        public Bid(int price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("Bids price can not be negative");
            }

            Price = price;
        }

        public int BidID { get; set; }

        public int ProductID { get; set; }

        public string UserID { get; set; }

        public decimal Price { get; private set; }

        // Navigation Properties
    }
}