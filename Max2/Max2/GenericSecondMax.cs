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

            T max, secondMax, first, second;
            first = collection.ElementAt(0);
            try
            {
                second = !collection.ElementAt(1).Equals(first) ? collection.ElementAt(1) : collection.Where(x => !x.Equals(first)).First();
            }
            catch (Exception)
            {
                throw new ArgumentException("Sequence contains only one element or all elements are equal in sequence");
            }

            (max, secondMax) = first.CompareTo(second) == 1 ? (first, second) : (second, first);

            foreach (T item in collection)
            {
                if (item.CompareTo(max) == 1)
                {
                    secondMax = max;
                    max = item;
                }

                if (item.CompareTo(second) == 1 && !item.Equals(max))
                    secondMax = item;
            }

            return secondMax;
        }
    }
}
