using Portal_Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal_Test
{
    public class SizeTests
    {
        [Fact]
        public void NotSame()
        {
            // Arrange & Act
            Size s1 = new Size(new Measure(1), new Measure(2), new Measure(3));
            Size s2 = new Size(new Measure(1), new Measure(2), new Measure(3));

            // Assert
            Assert.NotSame(s1, s2);
        }

        [Fact]
        public void EqualOperator()
        {
            // Arrange & Act
            Size s1 = new Size(new Measure(1), new Measure(2), new Measure(3));
            Size s2 = new Size(new Measure(1), new Measure(2), new Measure(3));

            // Assert
            Assert.True(s1 == s2);
        }

        [Fact]
        public void NotEqualOperator()
        {
            // Arrange & Act
            Size s1 = new Size(new Measure(1), new Measure(2), new Measure(3));
            Size s2 = new Size(new Measure(4), new Measure(5), new Measure(6));

            // Assert
            Assert.True(s1 != s2);
        }

        [Fact]
        public void AddOperator()
        {
            // Arrange
            Size s1 = new Size(new Measure(1), new Measure(2), new Measure(3));
            Size s2 = new Size(new Measure(4), new Measure(5), new Measure(6));

            // Act
            Size s3 = s1 + s2;

            // Assert
            Assert.True(s1.Lenght.Value + s2.Lenght.Value == 5);
            Assert.True(s1.Width.Value + s2.Width.Value == 7);
            Assert.True(s1.Height.Value + s2.Height.Value == 9);
        }

        [Fact]
        public void SubmissionOperator()
        {
            // Arrange
            Size s1 = new Size(new Measure(4), new Measure(5), new Measure(6));
            Size s2 = new Size(new Measure(1), new Measure(2), new Measure(3));

            // Act
            Size s3 = s1 + s2;

            // Assert
            Assert.True(s1.Lenght.Value - s2.Lenght.Value == 3);
            Assert.True(s1.Width.Value - s2.Width.Value == 3);
            Assert.True(s1.Height.Value - s2.Height.Value == 3);
        }
    }
}