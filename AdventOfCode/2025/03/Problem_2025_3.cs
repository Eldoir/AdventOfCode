using AdventOfCode.Extensions;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2025_3 : Problem2
    {
        public override long GetFirstStar()
        {
            return Lines.Sum(line => long.Parse(RunAlgo(2, line)));
        }

        public override long GetSecondStar()
        {
            return Lines.Sum(line => long.Parse(RunAlgo(12, line)));
        }

        static string RunAlgo(int digitsNeeded, string line)
        {
            int currentIndex = 0;
            string str = string.Empty;
            while (str.Length < digitsNeeded)
            {
                int remainingDigitsNeeded = digitsNeeded - str.Length;
                string window = line.SubstringUpTo(currentIndex, line.Length - remainingDigitsNeeded);
                (char value, int index) = window.GetMaxValueAndIndex();
                currentIndex += index + 1;
                str += value;
            }

            return str;
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 357),
            new Test("797", 97, Name: "Second is picked last")
        ];

        protected override Test[] TestsSecondStar =>
        [
            new Test("example", 3121910778619),
            new Test("987654321111111", 987654321111),
            new Test("811111111111119", 811111111119),
            new Test("234234234234278", 434234234278),
            new Test("818181911112111", 888911112111)
        ];
    }
}
