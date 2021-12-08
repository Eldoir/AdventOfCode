using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Extensions;

namespace AdventOfCode
{
    class Problem_2015_17 : Problem_2015
    {
        public override int Number => 17;

        public override void Run()
        {
            int[] containers = new int[Lines.Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                containers[i] = int.Parse(Lines[i]);
            }

            List<string> fittingCombinations = new List<string>();

            int firstStar = GetCombinations(containers, 150, (combination) =>
            {
                fittingCombinations.Add(combination);
            });

            int min = fittingCombinations.Min(c => c.NbOccurrences('1'));

            int secondStar = fittingCombinations.Where(c => c.NbOccurrences('1') == min).Count();

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar}");
        }

        private int GetCombinations(int[] containers, int n, Action<string> onFittingCombination = null)
        {
            string slots = "";

            for (int i = 0; i < containers.Length; i++)
                slots += '0';

            int combinations = 0;

            while ((slots = Next(slots)) != null)
            {
                if (Fits(containers, slots, n))
                {
                    combinations++;

                    if (onFittingCombination != null)
                        onFittingCombination(slots);
                }
            }

            return combinations;
        }

        private string Next(string str)
        {
            int idx = str.Length - 1;
            bool retenue = false;

            StringBuilder strB = new StringBuilder(str);

            do
            {
                if (str[idx] == '1')
                {
                    strB[idx] = '0';
                    retenue = true;
                    idx--;

                    if (idx == -1)
                    {
                        return null;
                    }
                }
                else
                {
                    strB[idx] = '1';
                    retenue = false;
                }
            }
            while (retenue);

            return strB.ToString();
        }

        private bool Fits(int[] containers, string slots, int n)
        {
            int total = 0;

            for (int i = 0; i < containers.Length; i++)
            {
                total += containers[i] * (slots[i] == '1' ? 1 : 0);
            }

            return total == n;
        }
    }
}
