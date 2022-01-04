using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2021_7 : Problem_2021
    {
        public override int Number => 7;

        public override void Run()
        {
            int[] nbs = Text.Split(',').Select(l => int.Parse(l)).ToArray();
            int min = nbs.Min();
            int max = nbs.Max();

            int minDistance = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                int distance = 0;

                foreach (int nb in nbs)
                {
                    distance += Math.Abs(nb - i);
                }

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            Console.WriteLine($"First star: {minDistance}");
        }
    }
}
