using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2020_2 : Problem
    {
        public override int Year => 2020;
        public override int Number => 2;

        public override void Run()
        {
            var pattern = @"(\d+)-(\d+) ([a-z]): ([a-z]+)";

            int totalValidPasswordsFirstStar = 0;
            int totalValidPasswordsSecondStar = 0;

            for (var i = 0; i < Lines.Length; i++)
            {
                var match = Regex.Match(Lines[i], pattern);
                var min = int.Parse(match.Groups[1].Value);
                var max = int.Parse(match.Groups[2].Value);
                var letter = match.Groups[3].Value[0];
                var password = match.Groups[4].Value;

                if (IsValidPasswordFirstStar(password, min, max, letter))
                {
                    totalValidPasswordsFirstStar++;
                }

                if (IsValidPasswordSecondStar(password, min, max, letter))
                {
                    totalValidPasswordsSecondStar++;
                }
            }

            Console.WriteLine($"First star: {totalValidPasswordsFirstStar}");
            Console.WriteLine($"Second star: {totalValidPasswordsSecondStar}");
        }

        #region Methods

        bool IsValidPasswordFirstStar(string password, int min, int max, char letter)
        {
            var count = 0;

            for (var i = 0; i < password.Length; i++)
            {
                if (password[i] == letter)
                {
                    count++;
                }
            }

            return count >= min && count <= max;
        }

        bool IsValidPasswordSecondStar(string password, int min, int max, char letter)
        {
            return password[min - 1] == letter ^ password[max - 1] == letter;
        }

        #endregion
    }
}
