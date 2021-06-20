using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal_Model.Common
{
    public class Measure : ValueObject
    {
        public Measure(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("Measure can not be negative");
            }

            Value = value;
        }
        
        public int Value { get; }

        public static bool operator ==(Measure left, Measure right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(Measure left, Measure right)
        {
            return NotEqualOperator(left, right);
        }

        public static Measure operator +(Measure left, Measure right)
        {
            return new Measure(left.Value + right.Value);
        }

        public static Measure operator -(Measure left, Measure right)
        {
            return new Measure(left.Value - right.Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}