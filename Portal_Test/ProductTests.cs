using System;
using Xunit;
using Portal_Model.Models;

namespace Portal_Test
{
    public class ProductTets
    {
        [Fact]
        public void ErrorIfNameBeNull()
        {
            Product product;

            Assert.Throws<ArgumentException>(() => product = new Product(null, 100));
        }

        [Fact]
        public void ErrorIfNameBeEmpty()
        {
            Product product;

            Assert.Throws<ArgumentException>(() => product = new Product("", 100));
        }

        [Fact]
        public void ErrorIfNameLenghtMoreThan50Chars()
        {
            Product product;

            Assert.Throws<ArgumentException>(() => product = new Product("012345678901234567890123456789012345678901234567890", 100));
        }

        [Fact]
        public void NameCheck()
        {
            // Arrange
            Product product1;
            Product product2;

            // Act
            product1 = new Product("Book", 100);
            product2 = new Product("01234567890123456789012345678901234567890123456789", 100);

            // Assert
            Assert.Equal("Book", product1.Name);
            Assert.Equal("01234567890123456789012345678901234567890123456789", product2.Name);
        }

        [Fact]
        public void ErrorIfPriceBeNegative()
        {
            Product product;

            Assert.Throws<ArgumentOutOfRangeException>(() => product = new Product("Book", -1));
        }

        [Fact]
        public void ErrorIfPriceMoreThan1000()
        {
            Product product;

            Assert.Throws<ArgumentOutOfRangeException>(() => product = new Product("Book", 1001));
        }

        [Fact]
        public void PriceCheck()
        {
            // Arrange
            Product product1;
            Product product2;
            Product product3;

            // Act
            product1 = new Product("Book", 0);
            product2 = new Product("Book", 1);
            product3 = new Product("Book", 1000);

            // Assert
            Assert.Equal(0, product1.BasePrice);
            Assert.Equal(1, product2.BasePrice);
            Assert.Equal(1000, product3.BasePrice);
        }

        [Fact]
        public void ChangeStatusToCancel()
        {
            // Arrange
            Product product = new Product("Book", 100);

            // Act
            product.ProductCanceled();

            // Assert
            Assert.Equal(Product_Status_Enum.Canceled, product.Status);
        }

        [Fact]
        public void ChangeStatusToSaled()
        {
            // Arrange
            Product product = new Product("Book", 100);

            // Act
            product.ProductSaled();

            // Assert
            Assert.Equal(Product_Status_Enum.Saled, product.Status);
        }
    }
}