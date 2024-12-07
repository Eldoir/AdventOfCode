using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_7 : Problem_2024
    {
        public override int Number => 7;

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
            for (int i = 0; i < n; i++)
            {
                bool[] operations = IntToBoolArray(i, boolArrayLength);
                long currentTotal = numbers[0];

                for (int j = 0; j < operations.Length; j++)
                {
                    if (operations[j]) // multiplication
                        currentTotal *= numbers[j + 1];
                    else
                        currentTotal += numbers[j + 1];

                    if (currentTotal > total)
                        break;
                }

                if (currentTotal == total)
                    return true;
            }
            return false;
        }

        // Not used; maybe in part2?
        static long ComputeWithPrecedence(long[] numbers, bool[] operations)
        {
            if (numbers.Length - 1 != operations.Length)
                throw new ArgumentException("The operations array must have one less element than the numbers array.");

            long[] numbersCopy = (long[])numbers.Clone();

            // Process multiplication first
            for (int i = 0; i < operations.Length; i++)
            {
                if (operations[i]) // true = multiplication
                {
                    numbersCopy[i + 1] = numbersCopy[i] * numbersCopy[i + 1]; // Merge multiplication result
                    numbersCopy[i] = 0; // Nullify the previous number (as addition won't affect result)
                }
            }

            // Process addition
            long result = 0;
            for (int i = 0; i < numbersCopy.Length; i++)
            {
                result += numbersCopy[i];
            }

            return result;
        }

        static bool[] IntToBoolArray(int number, int expectedArrayLength)
        {
            List<bool> bits = new();

            while (number > 0)
            {
                bits.Add((number % 2) == 1);
                number /= 2;
            }

            bits.Reverse();

            int pad = expectedArrayLength - bits.Count;

            for (int i = 0; i < pad; i++)
            {
                bits.Insert(0, false);
            }

            return bits.ToArray();
        }
    }
}
