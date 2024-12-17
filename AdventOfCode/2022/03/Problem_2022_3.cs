using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2022_3 : Problem
    {
        public override int Year => 2022;
        public override int Number => 3;

        public override void Run()
        {
            int firstStar = Lines.Sum(l => CharToInt(GetCommonChar(SplitInTwo(l))));

            int secondStar = 0;
            for (int i = 0; i < Lines.Length; i += 3)
            {
                secondStar += CharToInt(GetCommonChar(Lines[i], Lines[i + 1], Lines[i + 2]));
            }

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar}");
        }

        private (string, string) SplitInTwo(string str)
        {
            int half = str.Length / 2;
            return (str.Substring(0, half), str.Substring(half, half));
        }

        private char GetCommonChar((string str1, string str2) tuple)
            => GetCommonChar(tuple.str1, tuple.str2);

        private char GetCommonChar(string str1, string str2)
        {
            foreach(char c in str1)
            {
                if (str2.Contains(c))
                    return c;
            }

            throw new Exception();
        }

        private char GetCommonChar(string str1, string str2, string str3)
        {
            foreach (char c in str1)
            {
                if (str2.Contains(c) && str3.Contains(c))
                    return c;
            }

            throw new Exception();
        }

        private int CharToInt(char c)
        {
            if (c >= 'a')
                return c - 'a' + 1;

            return c - 'A' + 27;
        }
    }
}
