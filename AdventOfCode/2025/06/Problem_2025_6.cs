using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2025_6 : Problem2
    {
        public override long GetFirstStar()
        {
            int height = Lines.Length - 1;
            int width = Lines[0].Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Length;
            var numbers = new long[height, width]; 
            for (int i = 0; i < height; i++)
            {
                long[] n = Lines[i]
                    .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray();

                for (int j = 0; j < width; j++)
                {
                    numbers[i, j] = n[j];
                }
            }
            char[] operators = Lines[Lines.Length - 1]
                .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s[0])
                .ToArray();

            long grandTotal = 0;
            for (int j = 0; j < width; j++)
            {
                long total = operators[j] == '+' ? 0 : 1;
                Func<long, long, long> op = operators[j] == '+'
                    ? (a, b) => a + b
                    : (a, b) => a * b;
                for (int i = 0; i < height; i++)
                {
                    total = op(total, numbers[i, j]);
                }
                grandTotal += total;
            }

            return grandTotal;
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 4277556)
        ];
    }
}
