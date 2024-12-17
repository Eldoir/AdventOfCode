using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode
{
    class Problem_2021_3 : Problem
    {
        public override int Year => 2021;
        public override int Number => 3;

        private delegate char BitCriteria(int column, string[] lines, char defaultValueIfEqual);

        private int GammaRate => GetRate(MostCommonValue);
        private int EpsilonRate => GetRate(LeastCommonValue);
        private int PowerConsumption => GammaRate * EpsilonRate;

        private int OxygenGeneratorRating => GetRatingWithBitCriteria(MostCommonValue, '1');
        private int CO2ScrubberRating => GetRatingWithBitCriteria(LeastCommonValue, '0');
        private int LifeSupportRating => OxygenGeneratorRating * CO2ScrubberRating;

        public override void Run()
        {
            Console.WriteLine($"First star: {PowerConsumption}");
            Console.WriteLine($"Second star: {LifeSupportRating}");
        }

        #region Methods

        private int GetRate(BitCriteria criteria)
        {
            string rate = "";

            for (int i = 0; i < Lines[0].Length; i++)
            {
                rate += criteria(i, Lines, '0');
            }

            return Mathf.BinaryToInt(rate);
        }

        private int GetRatingWithBitCriteria(BitCriteria criteria, char defaultValueIfEqual)
        {
            var lines = new List<string>(Lines);
            int i = 0;

            while (lines.Count != 1)
            {
                char mostCommon = criteria(i, lines.ToArray(), defaultValueIfEqual);
                lines = lines.Where(l => l[i] == mostCommon).ToList();
                i++;
            }

            return Mathf.BinaryToInt(lines[0]);
        }

        private char MostCommonValue(int column, string[] lines, char defaultValueIfEqual)
        {
            (int , int) countBits = CountBits(column, lines);

            if (countBits.Item1 == countBits.Item2)
                return defaultValueIfEqual;

            return countBits.Item1 > countBits.Item2 ? '0' : '1';
        }

        private char LeastCommonValue(int column, string[] lines, char defaultValueIfEqual)
        {
            (int, int) countBits = CountBits(column, lines);

            if (countBits.Item1 == countBits.Item2)
                return defaultValueIfEqual;

            return countBits.Item1 < countBits.Item2 ? '0' : '1';
        }

        private (int, int) CountBits(int column, string[] lines)
        {
            (int, int) count = (0, 0);

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i][column] == '0')
                    count.Item1++;
                else
                    count.Item2++;
            }

            return count;
        }

        #endregion
    }
}
