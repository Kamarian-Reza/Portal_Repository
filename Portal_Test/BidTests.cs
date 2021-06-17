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
        [Theory]
        [InlineData(-1, Bid_Mode_Enum.Create, false)]
        [InlineData(0, Bid_Mode_Enum.Create, true)]
        [InlineData(1, Bid_Mode_Enum.Create, false)]
        [InlineData(-1, Bid_Mode_Enum.Update, false)]
        [InlineData(0, Bid_Mode_Enum.Update, false)]
        [InlineData(1, Bid_Mode_Enum.Update, true)]
        [InlineData(-1, Bid_Mode_Enum.Read, false)]
        [InlineData(0, Bid_Mode_Enum.Read, false)]
        [InlineData(1, Bid_Mode_Enum.Read, true)]
        public void BidIDRules(int bidId, Bid_Mode_Enum mode, bool expectedResult)
        {
            // Arrange
            Bid bid;

            // Act
            bid = new Bid(bidId, 1, "1", 1, mode);

            // Assert
            Assert.Equal(expectedResult, bid.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1", true)]
        public void UserIDRules(string userId, bool expectedResult)
        {
            // Arrange
            Bid bid;

            // Act
            bid = new Bid(1, 1, userId, 1, Bid_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, bid.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        public void PriceRules(decimal price, bool expectedResult)
        {
            // Arrange
            Bid bid;

            // Act
            bid = new Bid(1, 1, "1", price, Bid_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, bid.IsValid().Count == 0);
        }
    }
}