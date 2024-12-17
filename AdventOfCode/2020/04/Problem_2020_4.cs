using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2020_4 : Problem
    {
        public override int Year => 2020;
        public override int Number => 4;

        private Field[] fields;

        public override void Run()
        {
            fields = new Field[]
            {
                new Field("byr", (s) => IsBetween(s, 1920, 2002)),
                new Field("iyr", (s) => IsBetween(s, 2010, 2020)),
                new Field("eyr", (s) => IsBetween(s, 2020, 2030)),
                new Field("hgt", (s) =>
                {
                    if (s.EndsWith("cm")) return IsBetween(s.Substring(0, s.Length - 2), 150, 193);
                    if (s.EndsWith("in")) return IsBetween(s.Substring(0, s.Length - 2), 59, 76);
                    return false;
                }),
                new Field("hcl", @"#(\d|[a-f]){6}"),
                new Field("ecl", "amb|blu|brn|gry|grn|hzl|oth", (s) => s.Length == 3),
                new Field("pid", @"\d{9}")
            };

            var totalFirstStar = 0;
            var totalSecondStar = 0;
            var passport = "";

            foreach (var line in Lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (IsValidFirstStar(passport))
                    {
                        totalFirstStar++;
                    }
                    if (IsValidSecondStar(passport))
                    {
                        totalSecondStar++;
                    }

                    passport = "";
                }
                else
                {
                    passport += line + Environment.NewLine;
                }
            }

            if (IsValidFirstStar(passport))
            {
                totalFirstStar++;
            }
            if (IsValidSecondStar(passport))
            {
                totalSecondStar++;
            }

            Console.WriteLine($"First star: {totalFirstStar}");
            Console.WriteLine($"Second star: {totalSecondStar}");
        }

        #region Methods

        bool IsValidFirstStar(string str)
        {
            foreach (var field in fields)
            {
                if (!field.Exists(str))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsValidSecondStar(string str)
        {
            foreach (var field in fields)
            {
                if (!field.IsValid(str))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsBetween(string str, int min, int max)
        {
            var val = int.Parse(str);
            return val >= min && val <= max;
        }

        #endregion
    }

    class Field
    {
        // Fields can have a validity pattern or a validity function, and even both

        string name;
        string validityPattern;
        Predicate<string> validityFunction;

        public Field(string name, string validityPattern)
        {
            this.name = name;
            this.validityPattern = validityPattern;
        }

        public Field(string name, Predicate<string> validityFunction)
        {
            this.name = name;
            this.validityFunction = validityFunction;
        }

        public Field(string name, string validityPattern, Predicate<string> validityFunction)
        {
            this.name = name;
            this.validityPattern = validityPattern;
            this.validityFunction = validityFunction;
        }

        public bool IsValid(string str)
        {
            if (!Exists(str))
            {
                return false;
            }

            var value = GetValue(str);

            if (!string.IsNullOrEmpty(validityPattern))
            {
                var match = Regex.Match(value, $"^{validityPattern}$");

                if (string.IsNullOrEmpty(match.Value))
                {
                    return false; // the field hasn't a valid value according to the pattern
                }

                if (validityFunction != null)
                {
                    return validityFunction(value);
                }

                return true;
            }
            else
            {
                return validityFunction(value);
            }
        }

        public bool Exists(string str)
        {
            return GetValue(str) != null;
        }

        private string GetValue(string str)
        {
            var fieldPattern = $@"{name}:(\S+)\s";
            var match = Regex.Match(str, fieldPattern);

            if (match.Groups.Count == 2)
            {
                return match.Groups[1].Value;
            }

            return null;
        }
    }
}
