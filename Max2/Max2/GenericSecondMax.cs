using System;
using System.Collections.Generic;
using System.Linq;

namespace Max2
{
    public static class GenericSecondMax
    {
        public static T Max2<T>(this IEnumerable<T> collection) where T : IComparable<T>, IEquatable<T>
        {
            if (collection.Count() == 0)
                throw new ArgumentException("Sequence contains no element");

            T max, secondMax;
            max = collection.ElementAt(0);

            if (collection.Count() < 2 || !collection.Where(x => !x.Equals(max)).Any())
                throw new ArgumentException("Sequence contains only one element or all elements are equal in sequence");

            secondMax = !collection.ElementAt(1).Equals(max) ? collection.ElementAt(1) : collection.Where(x => !x.Equals(max)).First();
            (max, secondMax) = max.CompareTo(secondMax) == 1 ? (max, secondMax) : (secondMax, max);

            foreach (T item in collection)
            {
                if (item.CompareTo(max) == 1)
                {
                    secondMax = max;
                    max = item;
                }

                if (item.CompareTo(secondMax) == 1 && !item.Equals(max))
                    secondMax = item;
            }

            return secondMax;
        }
    }
}
