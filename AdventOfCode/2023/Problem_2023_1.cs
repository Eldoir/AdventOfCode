using AdventOfCode.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2023_1 : Problem_2023
    {
        public override int Number => 1;

        public override void Run()
        {
            List<(int Index, int Value)> digits = new();
            string[] words = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            Console.WriteLine($"First star: {Lines.Sum(line => GetNumber(line, false))}");
            Console.WriteLine($"Second star: {Lines.Sum(line => GetNumber(line, true))}");

            #region Local methods

            int GetNumber(string line, bool identifyWords)
            {
                digits.Clear();

                // Try identify digits
                for (int i = 0; i < line.Length; i++)
                {
                    int d = line[i] - '0';
                    if (d >= 0 && d <= 9)
                        digits.Add((i, d));
                }

                if (identifyWords)
                {
                    // Try identify words
                    for (int i = 0; i < words.Length; i++)
                    {
                        foreach (int idx in line.IndexesOf(words[i]))
                        {
                            digits.Add((idx, i + 1));
                        }
                    }
                }

                digits = digits.OrderBy(tuple => tuple.Index).ToList();
                return 10 * digits.First().Value + digits.Last().Value;
            }

            #endregion
        }
    }
}
