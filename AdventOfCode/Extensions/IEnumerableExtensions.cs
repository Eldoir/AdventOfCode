using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Extensions
{
    static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> Subset<TSource>(this IEnumerable<TSource> source, int start, int count)
        {
            return source.Skip(start).Take(count);
        }
    }
}
