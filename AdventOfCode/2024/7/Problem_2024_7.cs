using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_7 : Problem
    {
        public override int Year => 2024;
        public override int Number => 7;

        delegate long Compute(long a, long b);
        static long Add(long a, long b) => a + b;
        static long Multiply(long a, long b) => a * b;
        static long Concatenate(long a, long b) => a * ((long)Math.Pow(10, (long)Math.Log10(b) + 1)) + b;

        public override void Run()
        {
            //UseTestInput();
            long firstStar = 0;
            long secondStar = 0;

            foreach (string line in Lines)
            {
                string[] strings = line.Split(": ");
                long total = long.Parse(strings[0]);
                int[] numbers = strings[1].Split(' ').Select(int.Parse).ToArray();
                if (IsWorkingCombi(total, numbers, Add, Multiply))
                    firstStar += total;
                if (IsWorkingCombi(total, numbers, Add, Multiply, Concatenate))
                    secondStar += total;
            }

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar}");
        }

        static bool IsWorkingCombi(long total, int[] numbers, params Compute[] computers)
        {
            List<int[]> combinations = Combination.Generate(numbers.Length - 1, computers.Length);
            foreach (int[] operations in combinations)
            {
                long currentTotal = numbers[0];

                for (int j = 0; j < operations.Length; j++)
                {
                    currentTotal = computers[operations[j]](currentTotal, numbers[j + 1]);

                    if (currentTotal > total)
                        break;
                }

                if (currentTotal == total)
                    return true;
            }
            return false;
        }
    }
}
