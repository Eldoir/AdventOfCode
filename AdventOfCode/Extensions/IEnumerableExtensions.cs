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
    }
}
