using Portal_Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal_Test
{
    public class WeightTests
    {
        [Fact]
        public void Immutable()
        {
            // Arrange
            Weight w1 = new Weight(new Measure(10));

            // Act
            Weight w2 = w1.AddBoxWeight(new Weight(new Measure(2)));

            // Assert
            Assert.NotSame(w1, w2);
        }

        [Fact]
        public void NotSame()
        {
            // Arrange & Act
            Weight w1 = new Weight(new Measure(1));
            Weight w2 = new Weight(new Measure(1));

            // Assert
            Assert.NotSame(w1, w2);
        }

        [Fact]
        public void EqualOperator()
        {
            // Arrange & Act
            Weight w1 = new Weight(new Measure(1));
            Weight w2 = new Weight(new Measure(1));

            // Assert
            Assert.True(w1 == w2);
        }

        [Fact]
        public void NotEqualOperator()
        {
            // Arrange & Act
            Weight w1 = new Weight(new Measure(1));
            Weight w2 = new Weight(new Measure(2));

            // Assert
            Assert.True(w1 != w2);
        }

        [Fact]
        public void AddOperator()
        {
            // Arrange
            Weight w1 = new Weight(new Measure(1));
            Weight w2 = new Weight(new Measure(2));

            // Act
            Weight w3 = w1 + w2;

            // Assert
            Assert.True(w3.Value.Value == 3);
        }

        [Fact]
        public void SubmissionOperator()
        {
            // Arrange
            Weight w1 = new Weight(new Measure(2));
            Weight w2 = new Weight(new Measure(1));

            // Act
            Weight w3 = w1 - w2;

            // Assert
            Assert.True(w3.Value.Value == 1);
        }
    }
}