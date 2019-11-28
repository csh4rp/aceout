using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Domain.Model
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        protected abstract IEnumerable<object> GetPropertiesForEqualityCheck();

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            foreach (var item in GetPropertiesForEqualityCheck())
            {
                hash = hash * 31 + (item == null ? 0 : item.GetHashCode());
            }

            return hash;
        }

        public bool Equals(T obj)
        {
            if(obj == null)
            {
                return false;
            }

            return GetPropertiesForEqualityCheck()
                .SequenceEqual(obj.GetPropertiesForEqualityCheck());
        }

        public static bool operator == (ValueObject<T> left, ValueObject<T> right)
        {
            return Equals(left, right);
        }

        public static bool operator != (ValueObject<T> left, ValueObject<T> right)
        {
            return !Equals(left, right);
        }
    }
}
