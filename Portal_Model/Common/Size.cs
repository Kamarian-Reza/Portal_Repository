using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal_Model.Common
{
    public class Size : ValueObject
    {
        public Size(Measure lenght, Measure width, Measure height)
        {
            Lenght = lenght;
            Width = width;
            Height = height;
        }

        public Measure Lenght { get; }
        public Measure Width { get; }
        public Measure Height { get; }

        public static bool operator ==(Size left, Size right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(Size left, Size right)
        {
            return NotEqualOperator(left, right);
        }

        public static Size operator +(Size left, Size right)
        {
            return new Size(left.Lenght + right.Lenght, left.Width + right.Width, left.Height + right.Height);
        }

        public static Size operator -(Size left, Size right)
        {
            return new Size(left.Lenght - right.Lenght, left.Width - right.Width, left.Height - right.Height);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Lenght;
            yield return Width;
            yield return Lenght;
        }
    }
}