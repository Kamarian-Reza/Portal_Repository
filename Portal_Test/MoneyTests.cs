using Portal_Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal_Test
{
    public class MoneyTests
    {
        [Fact]
        public void NotSame()
        {
            // Arrange & Act
            Money m1 = new Money(new Measure(1));
            Money m2 = new Money(new Measure(1));

            // Assert
            Assert.NotSame(m1, m2);
        }

        [Fact]
        public void EqualOperator()
        {
            // Arrange & Act
            Money m1 = new Money(new Measure(1));
            Money m2 = new Money(new Measure(1));

            // Assert
            Assert.True(m1 == m2);
        }

        [Fact]
        public void NotEqualOperator()
        {
            // Arrange & Act
            Money m1 = new Money(new Measure(1));
            Money m2 = new Money(new Measure(2));

            // Assert
            Assert.True(m1 != m2);
        }

        [Fact]
        public void AddOperator()
        {
            // Arrange
            Money m1 = new Money(new Measure(1));
            Money m2 = new Money(new Measure(2));

            // Act
            Money m3 = m1 + m2;

            // Assert
            Assert.True(m3.Value.Value == 3);
        }

        [Fact]
        public void SubmissionOperator()
        {
            // Arrange
            Money m1 = new Money(new Measure(2));
            Money m2 = new Money(new Measure(1));

            // Act
            Money m3 = m1 - m2;

            // Assert
            Assert.True(m3.Value.Value == 1);
        }
    }
}