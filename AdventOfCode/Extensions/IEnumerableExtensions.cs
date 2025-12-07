using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Extensions
{
    static class IEnumerableExtensions
    {
        public static IEnumerable<T> Subset<T>(this IEnumerable<T> source, int start, int count)
        {
            return source.Skip(start).Take(count);
        }

        public static T[] Sort<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(a => a).ToArray();
        }

        public static (T maxValue, int maxIndex) GetMaxValueAndIndex<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            ArgumentNullException.ThrowIfNull(sequence);

            using IEnumerator<T> enumerator = sequence.GetEnumerator();

            if (!enumerator.MoveNext())
                throw new ArgumentException("Sequence contains no elements.");

            T maxValue = enumerator.Current;
            int maxIndex = 0;
            int currentIndex = 0;

            while (enumerator.MoveNext())
            {
                currentIndex++;

                if (enumerator.Current.CompareTo(maxValue) > 0)
                {
                    maxValue = enumerator.Current;
                    maxIndex = currentIndex;
                }
            }

            return (maxValue, maxIndex);
        }
    }
}
