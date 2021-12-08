using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2015_5 : Problem_2015
    {
        public override int Number => 5;

        public override void Run()
        {
            int countFirstStar = 0;
            int countSecondStar = 0;

            for (int i = 0; i < Lines.Length; i++)
            {
                if (IsNiceFirstStar(Lines[i])) countFirstStar++;
                if (IsNiceSecondStar(Lines[i])) countSecondStar++;
            }

            Console.WriteLine($"First star: {countFirstStar}");
            Console.WriteLine($"Second star: {countSecondStar}");
        }

        private bool IsNiceFirstStar(string str)
        {
            return ContainsAtLeastVowels(str, 3) &&
            AtLeastOneLetterAppearingTwiceInARow(str) &&
            !ContainsSpecialStrings(str);
        }

        private bool IsNiceSecondStar(string str)
        {
            return AtLeastOneLetterAppearingTwiceWithOneLetterBetween(str) &&
            PairAppearingAtLeastTwice(str);
        }

        private bool ContainsAtLeastVowels(string str, int n)
        {
            string vowels = "aeiou";
            int count = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (vowels.Contains(str[i].ToString())) count++;

                if (count == n) return true;
            }

            return false;
        }

        private bool AtLeastOneLetterAppearingTwiceInARow(string str)
        {
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == str[i - 1]) return true;
            }

            return false;
        }

        private bool PairAppearingAtLeastTwice(string str)
        {
            HashSet<int> usedIndexes = new HashSet<int>();

            for (int i = 0; i < str.Length - 1; i++)
            {
                int count = 0, limit = 2;
                usedIndexes.Clear();

                for (int j = 1; j < str.Length; j++)
                {
                    if (str[j] == str[i + 1] && str[j - 1] == str[i] && !
                    usedIndexes.Contains(j - 1) && !usedIndexes.Contains(j))
                    {
                        count++;
                        usedIndexes.Add(j);
                        usedIndexes.Add(j - 1);
                    }

                    if (count == limit)
                        return true;
                }
            }

            return false;
        }

        private bool AtLeastOneLetterAppearingTwiceWithOneLetterBetween(string str)
        {
            for (int i = 2; i < str.Length; i++)
            {
                if (str[i] == str[i - 2]) return true;
            }

            return false;
        }

        private bool ContainsSpecialStrings(string str)
        {
            string[] specialStrings = new string[] { "ab", "cd", "pq", "xy" };

            for (int i = 0; i < specialStrings.Length; i++)
            {
                if (str.Contains(specialStrings[i])) return true;
            }

            return false;
        }
    }
}
