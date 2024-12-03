using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2024_3 : Problem_2024
    {
        public override int Number => 3;

        public override void Run()
        {
            Dictionary<int, bool> dosAndDonts = new();
            foreach (int doIndex in IndexesOf(Text, "do()"))
            {
                dosAndDonts.Add(doIndex, true);
            }
            foreach (int dontIndex in IndexesOf(Text, "don't()"))
            {
                dosAndDonts.Add(dontIndex, false);
            }

            Regex regex = new("mul\\((\\d{1,3}),(\\d{1,3})\\)");
            int sumFirstStar = 0;
            int sumSecondStar = 0;
            foreach (Match match in regex.Matches(Text))
            {
                int value = int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                sumFirstStar += value;
                if (ShouldDo(dosAndDonts, match.Index))
                {
                    sumSecondStar += value;
                }
            }
            Console.WriteLine($"First star: {sumFirstStar}");
            Console.WriteLine($"Second star: {sumSecondStar}");
        }

        static bool ShouldDo(Dictionary<int, bool> dosAndDonts, int index)
        {
            int[] orderedIndexes = dosAndDonts.Keys.OrderBy(i => i).ToArray();
            if (orderedIndexes.Length == 0)
                return true;
            if (orderedIndexes.Length == 1)
                return index < orderedIndexes[0] || dosAndDonts[orderedIndexes[0]];
            for (int i = 1; i < orderedIndexes.Length; i++)
            {
                int beforeIndex = orderedIndexes[i - 1];
                int afterIndex = orderedIndexes[i];
                if (beforeIndex < index && index < afterIndex)
                    return dosAndDonts[beforeIndex];
            }
            return dosAndDonts[orderedIndexes[^1]];
        }

        static int[] IndexesOf(string input, string substring)
        {
            List<int> indexes = new();
            int startIndex = 0;
            while ((startIndex = input.IndexOf(substring, startIndex)) != -1)
            {
                indexes.Add(startIndex);
                startIndex += substring.Length;
            }
            return indexes.ToArray();
        }
    }
}
