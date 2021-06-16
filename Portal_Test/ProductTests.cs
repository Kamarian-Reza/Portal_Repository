using System;
using Xunit;
using Portal_Model.Models;
using System.Collections.Generic;

namespace Portal_Test
{
    public class ProductTets
    {
        [Theory]
        [InlineData(-1, Product_Mode_Enum.Create, false)]
        [InlineData(0, Product_Mode_Enum.Create, true)]
        [InlineData(1, Product_Mode_Enum.Create, false)]
        [InlineData(-1, Product_Mode_Enum.Update, false)]
        [InlineData(0, Product_Mode_Enum.Update, false)]
        [InlineData(1, Product_Mode_Enum.Update, true)]
        [InlineData(-1, Product_Mode_Enum.Read, false)]
        [InlineData(0, Product_Mode_Enum.Read, false)]
        [InlineData(1, Product_Mode_Enum.Read, true)]
        public void ProductIDRules(int id, Product_Mode_Enum mode, bool expectedResult)
        {
            // Arrange
            Product product;

            // Act
            product = new Product(id, "1", "Book", 1, DateTime.Now, Product_Status_Enum.OnSale, mode);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void UserIDRules(string userId, bool expectedResult)
        {
            // Arrange
            Product product;

            // Act
            product = new Product(1, userId, "Book", 1, DateTime.Now, Product_Status_Enum.OnSale, Product_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("Book", true)]
        [InlineData("01234567890123456789012345678901234567890123456789", true)]
        [InlineData("012345678901234567890123456789012345678901234567890", false)]
        public void NameRules(string name, bool expectedResult)
        {
            // Arrange
            Product product;
            
            // Act
            product = new Product(1, "1", name, 1, DateTime.Now, Product_Status_Enum.OnSale, Product_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(1000, true)]
        [InlineData(1001, false)]
        public void BasePriceRules(decimal basePrice, bool expectedResult)
        {
            // Arrange
            Product product;

            // Act
            product = new Product(1, "1", "Book", basePrice, DateTime.Now, Product_Status_Enum.OnSale, Product_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(Product_Status_Enum.OnSale, true)]
        [InlineData(Product_Status_Enum.Sold, false)]
        [InlineData(Product_Status_Enum.Canceled, false)]
        public void StatusRules(Product_Status_Enum status, bool expectedResult)
        {
            // Arrange
            Product product;

            // Act
            product = new Product(0, "1", "Book", 1, DateTime.Now, status, Product_Mode_Enum.Create);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(Product_Status_Enum.OnSale, true)]
        [InlineData(Product_Status_Enum.Sold, false)]
        [InlineData(Product_Status_Enum.Canceled, false)]
        public void ChangingStatus(Product_Status_Enum status, bool expectedResult)
        {
            // Arrange
            Product product;

            // Act
            product = new Product(0, "1", "Book", 1, DateTime.Now, status, Product_Mode_Enum.Create);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }
    }
}