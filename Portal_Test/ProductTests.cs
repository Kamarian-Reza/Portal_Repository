using System;
using Xunit;
using Portal_Model.Models;
using System.Collections.Generic;
using Portal_Model.Common;

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

            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            // Act

            product = new Product(id, "1", "Book", basePrice, DateTime.Now, size, weight, Product_Status_Enum.OnSale, mode);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1", true)]
        public void UserIDRules(string userId, bool expectedResult)
        {
            // Arrange
            Product product;

            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            // Act
            product = new Product(1, userId, "Book", basePrice, DateTime.Now, size, weight, Product_Status_Enum.OnSale, Product_Mode_Enum.Read);

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

            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            // Act
            product = new Product(1, "1", name, basePrice, DateTime.Now, size, weight, Product_Status_Enum.OnSale, Product_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Fact]
        public void BasePriceException()
        {
            // Arrange
            Measure measure;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => measure = new Measure(-1));
        }
        
        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(1000, true)]
        [InlineData(1001, false)]
        public void BasePriceRules(int basePrice, bool expectedResult)
        {
            // Arrange
            Product product;

            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            // Act
            product = new Product(1, "1", "Book", new Money(new Measure(basePrice)), DateTime.Now, size, weight, Product_Status_Enum.OnSale, Product_Mode_Enum.Read);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(Product_Status_Enum.OnSale, true)]
        [InlineData(Product_Status_Enum.Sold, false)]
        [InlineData(Product_Status_Enum.Canceled, false)]
        public void OnSaleStatus(Product_Status_Enum status, bool expectedResult)
        {
            // Arrange
            Product product;

            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            // Act
            product = new Product(0, "1", "Book", basePrice, DateTime.Now, size, weight, status, Product_Mode_Enum.Create);

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(Product_Status_Enum.OnSale, true)]
        [InlineData(Product_Status_Enum.Sold, false)]
        [InlineData(Product_Status_Enum.Canceled, false)]
        public void SoldStatus(Product_Status_Enum status, bool expectedResult)
        {
            // Arrange
            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            Product product = new Product(1, "1", "Book", basePrice, DateTime.Now, size, weight, status, Product_Mode_Enum.Update); ;

            // Act
            product.Sold();

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Theory]
        [InlineData(Product_Status_Enum.OnSale, true)]
        [InlineData(Product_Status_Enum.Sold, false)]
        [InlineData(Product_Status_Enum.Canceled, false)]
        public void CancelStatus(Product_Status_Enum status, bool expectedResult)
        {
            // Arrange
            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));

            Product product = new Product(1, "1", "Book", basePrice, DateTime.Now, size, weight, status, Product_Mode_Enum.Update); ;

            // Act
            product.Cancel();

            // Assert
            Assert.Equal(expectedResult, product.IsValid().Count == 0);
        }

        [Fact]
        public void CancelTime()
        {
            // Arrange
            Money basePrice = new Money(new Measure(1));
            Size size = new Size(new Measure(1), new Measure(1), new Measure(1));
            Weight weight = new Weight(new Measure(1));
            
            Product product1 = new Product(1, "1", "Book", basePrice, DateTime.Now, size, weight, Product_Status_Enum.OnSale, Product_Mode_Enum.Update);
            Product product2 = new Product(1, "1", "Book", basePrice, DateTime.Now.AddHours(-25), size, weight, Product_Status_Enum.OnSale, Product_Mode_Enum.Update);

            // Act
            product1.Cancel();
            product2.Cancel();

            // Assert
            Assert.True(product1.IsValid().Count == 0);
            Assert.False(product2.IsValid().Count == 0);
        }
    }
}