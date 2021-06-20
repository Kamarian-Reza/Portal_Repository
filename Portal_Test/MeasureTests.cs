using Portal_Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal_Test
{
    public class MeasureTests
    {
        [Fact]
        public void OutOfRange()
        {
            // Arrange
            Measure measure;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => measure = new Measure(-1));
        }

        [Fact]
        public void NotSame()
        {
            // Arrange & Act
            Measure m1 = new Measure(1);
            Measure m2 = new Measure(1);

            // Assert
            Assert.NotSame(m1, m2);
        }

        [Fact]
        public void EqualOperator()
        {
            // Arrange & Act
            Measure m1 = new Measure(1);
            Measure m2 = new Measure(1);

            // Assert
            Assert.True(m1 == m2);
        }

        [Fact]
        public void NotEqualOperator()
        {
            // Arrange & Act
            Measure m1 = new Measure(1);
            Measure m2 = new Measure(2);

            // Assert
            Assert.True(m1 != m2);
        }

        [Fact]
        public void AddOperator()
        {
            // Arrange
            Measure m1 = new Measure(1);
            Measure m2 = new Measure(2);

            // Act
            Measure m3 = m1 + m2;

            // Assert
            Assert.True(m3.Value == 3);
        }

        [Fact]
        public void SubmissionOperator()
        {
            // Arrange
            Measure m1 = new Measure(2);
            Measure m2 = new Measure(1);

            // Act
            Measure m3 = m1 - m2;

            // Assert
            Assert.True(m3.Value == 1);
        }
    }
}