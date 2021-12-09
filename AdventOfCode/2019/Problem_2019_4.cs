using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2019_4 : Problem_2019
    {
        public override int Number => 4;

        public override void Run()
        {
            string[] inputs = Text.Split('-');
            int min = int.Parse(inputs[0]);
            int max = int.Parse(inputs[1]);

            int countFirstStar = 0;
            int countSecondStar = 0;

            for (int i = min; i <= max; i++)
            {
                if (IsValidFirstStar(i))
                {
                    countFirstStar++;
                }

                if (IsValidSecondStar(i))
                {
                    countSecondStar++;
                }
            }

            Console.WriteLine($"First star: {countFirstStar}");
            Console.WriteLine($"Second star: {countSecondStar}");
        }

        private bool IsValidFirstStar(int n)
        {
            return AtLeastTwoAdjacentDigits(n) && AlwaysIncreases(n);
        }

        private bool IsValidSecondStar(int n)
        {
            return IsValidFirstStar(n) && GetPairIndexes(n).Length > 0;
        }

        private bool AtLeastTwoAdjacentDigits(int n)
        {
            string nStr = n.ToString();

            for (int i = 1; i < nStr.Length; i++)
            {
                if (nStr[i] == nStr[i - 1]) return true;
            }

            return false;
        }

        private bool AlwaysIncreases(int n)
        {
            string nStr = n.ToString();

            for (int i = 1; i < nStr.Length; i++)
            {
                if (nStr[i] < nStr[i - 1]) return false;
            }

            return true;
        }

        private int[] GetPairIndexes(int n)
        {
            List<int> indexes = new List<int>();
            string nStr = n.ToString();

            int idx = 0;
            int count = 1;

            for (int i = 1; i < nStr.Length; i++)
            {
                if (nStr[i] == nStr[i - 1])
                {
                    count++;
                }
                else
                {
                    if (count == 2) // Pair
                    {
                        indexes.Add(idx);
                    }

                    idx = i;
                    count = 1;
                }
            }

            if (count == 2)
            {
                indexes.Add(idx);
            }

            return indexes.ToArray();
        }
    }
}
