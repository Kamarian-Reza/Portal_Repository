using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal_Model.Common
{
    public class Money : ValueObject
    {
        public Money(Measure value)
        {
            Value = value;
        }

        public Measure Value { get; }

        public static bool operator ==(Money left, Money right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return NotEqualOperator(left, right);
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Value + right.Value);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Value - right.Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}