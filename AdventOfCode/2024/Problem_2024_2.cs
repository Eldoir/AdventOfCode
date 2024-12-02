using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_2 : Problem_2024
    {
        public override int Number => 2;

        public override void Run()
        {
            int safeReportsFirstStar = 0;
            int safeReportsSecondStar = 0;
            foreach (string line in Lines)
            {
                int[] nbs = line.Split(' ').Select(int.Parse).ToArray();
                safeReportsFirstStar += IsSafeFirstStar(nbs) ? 1 : 0;
                safeReportsSecondStar += IsSafeSecondStar(nbs) ? 1 : 0;
            }
            Console.WriteLine($"First star: {safeReportsFirstStar}");
            Console.WriteLine($"Second star: {safeReportsSecondStar}");
        }

        static bool IsSafeFirstStar(int[] report)
        {
            return IsSafe(report);
        }

        static bool IsSafeSecondStar(int[] report)
        {
            if (IsSafe(report))
                return true;

            for (int i = 0; i < report.Length; i++)
            {
                if (IsSafe(WithoutIndex(report, i)))
                    return true;
            }
            return false;
        }

        static int[] WithoutIndex(int[] arr, int index)
        {
            return arr.Where((_, i) => i != index).ToArray();
        }

        static bool IsSafe(int[] report)
        {
            bool? increase = null;
            for (int i = 1; i < report.Length; i++)
            {
                int delta = report[i] - report[i - 1];
                int absDelta = Math.Abs(delta);
                if (absDelta < 1 || absDelta > 3)
                    return false;

                if (increase is null)
                    increase = delta > 0;
                else if (increase.Value != delta > 0)
                    return false;
            }
            return true;
        }
    }
}
