using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2025_5 : Problem2
    {
        public override long GetFirstStar()
        {
            List<(long, long)> ranges = GetRanges(out int lineIndex);
            lineIndex++; // go to first line of IDs
            int sum = 0;
            for (int i = lineIndex; i < Lines.Length; i++)
            {
                long n = long.Parse(Lines[i]);
                if (ranges.Any(r => r.Item1 <= n && r.Item2 >= n)) sum++;
            }
            return sum;
        }

        public override long GetSecondStar()
        {
            List<(long, long)> ranges = GetRanges(out _);
            ranges.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            long sum = 0;
            long lastMax = 0;
            long allMin = long.MaxValue;
            long allMax = long.MinValue;
            foreach ((long, long) range in ranges)
            {
                if (range.Item1 >= allMin && range.Item2 <= allMax)
                    continue; // this range is already included in another range
                
                sum += range.Item2 - range.Item1 + 1;
                if (lastMax >= range.Item1)
                {
                    sum -= (lastMax - range.Item1 + 1);
                }
                lastMax = range.Item2;

                // Update extremums
                if (range.Item1 < allMin)
                {
                    allMin = range.Item1;
                }
                if (range.Item2 > allMax)
                {
                    allMax = range.Item2;
                }
            }
            return sum;
        }

        List<(long, long)> GetRanges(out int lineIndex)
        {
            lineIndex = 0;
            List<(long, long)> ranges = [];
            while (!string.IsNullOrEmpty(Lines[lineIndex]))
            {
                string[] inputs = Lines[lineIndex].Split('-');
                ranges.Add((long.Parse(inputs[0]), long.Parse(inputs[1])));
                lineIndex++;
            }
            return ranges;
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 3)
        ];

        protected override Test[] TestsSecondStar =>
        [
            new Test("example", 14),
            new Test("3-5\n5-6\n", 4, "Overlap pile"),
            new Test("3-5\n3-6\n", 4, "Same start, different end"),
            new Test("2-5\n3-4\n", 4, "Range inside other range")
        ];
    }
}
