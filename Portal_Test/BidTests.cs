using Portal_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal_Test
{
    public class BidTests
    {
        [Fact]
        public void ErrorIfPriceBeNegative()
        {
            Bid bid;

            Assert.Throws<ArgumentOutOfRangeException>(() => bid = new Bid(-1));
        }

        [Fact]
        public void PriceCheck()
        {
            // Arrange
            Bid bid1;
            Bid bid2;

            // Act
            bid1 = new Bid(0);
            bid2 = new Bid(1);

            // Assert
            Assert.Equal(0, bid1.Price);
            Assert.Equal(1, bid2.Price);
        }
    }
}