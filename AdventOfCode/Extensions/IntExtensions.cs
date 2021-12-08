using System.Collections.Generic;

namespace AdventOfCode.Extensions
{
    static class IntExtensions
    {
        public static IEnumerable<int> SelectDivisors(this int num)
        {
            yield return 1;

            int incr = num % 2 == 0 ? 1 : 2;
            var largeDivisors = new List<int>();
            for (int divisor = incr + 1; divisor * divisor <= num; divisor += incr)
            {
                int quotient = num / divisor;
                if (quotient * divisor == num)
                {
                    yield return divisor;
                    if (quotient != divisor)
                    {
                        largeDivisors.Add(quotient);
                    }
                }
            }

            largeDivisors.Reverse();
            for (int k = 0; k < largeDivisors.Count; k++)
            {
                yield return largeDivisors[k];
            }

            yield return num;
        }
    }
}
