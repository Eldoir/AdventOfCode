using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2025_5 : Problem2
    {
        public override long GetFirstStar()
        {
            int line = 0;
            List<(long, long)> ranges = [];
            while (!string.IsNullOrEmpty(Lines[line]))
            {
                string[] inputs = Lines[line].Split('-');
                ranges.Add((long.Parse(inputs[0]), long.Parse(inputs[1])));
                line++;
            }
            line++; // go to first line of IDs
            int sum = 0;
            for (int i = line; i < Lines.Length; i++)
            {
                long n = long.Parse(Lines[i]);
                if (ranges.Any(r => r.Item1 <= n && r.Item2 >= n)) sum++;
            }
            return sum;
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 3)
        ];
    }
}
