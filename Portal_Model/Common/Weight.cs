using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal_Model.Common
{
    public class Weight : ValueObject
    {
        public Weight(Measure value)
        {
            Value = value;
        }

        public Measure Value { get; }

        public Weight KiloGrams()
        {
            return new Weight(new Measure(Value.Value / 1000));
        }

        public Weight AddBoxWeight(Weight boxWeight)
        {
            return new Weight(Value + boxWeight.Value);
        }

        public static bool operator ==(Weight left, Weight right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(Weight left, Weight right)
        {
            return NotEqualOperator(left, right);
        }

        public static Weight operator +(Weight left, Weight right)
        {
            return new Weight(left.Value + right.Value);
        }

        public static Weight operator -(Weight left, Weight right)
        {
            return new Weight(left.Value - right.Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}