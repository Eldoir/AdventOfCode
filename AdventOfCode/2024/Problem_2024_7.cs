using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_7 : Problem_2024
    {
        public override int Number => 7;

        enum Operation
        {
            Add = 0,
            Multiply = 1,
            Concatenate = 2
        }

        public override void Run()
        {
            //UseTestInput();
            long firstStar = 0;
            foreach (string line in Lines)
            {
                string[] strings = line.Split(": ");
                long total = long.Parse(strings[0]);
                int[] numbers = strings[1].Split(' ').Select(int.Parse).ToArray();
                if (IsWorkingCombi(total, numbers))
                    firstStar += total;
            }
            Console.WriteLine($"First star: {firstStar}");
        }

        static bool IsWorkingCombi(long total, int[] numbers)
        {
            int boolArrayLength = numbers.Length - 1;
            int n = (int)Math.Pow(2, boolArrayLength);
            List<int[]> combinations = Combination.Generate(numbers.Length - 1, 2);
            foreach (int[] operations in combinations)
            {
                long currentTotal = numbers[0];

                for (int j = 0; j < operations.Length; j++)
                {
                    if (operations[j] == (int)Operation.Multiply)
                        currentTotal *= numbers[j + 1];
                    else if (operations[j] == (int)Operation.Add)
                        currentTotal += numbers[j + 1];

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
