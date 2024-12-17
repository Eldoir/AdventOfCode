using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Problem_2024_9 prob = new();

            long firstStar = prob.GetFirstStar();
            if (firstStar != 0)
            {
                Console.WriteLine($"First star: {firstStar}");
            }

            long secondStar = prob.GetSecondStar();
            if (secondStar != 0)
            {
                Console.WriteLine($"Second star: {secondStar}");
            }
        }
    }
}
