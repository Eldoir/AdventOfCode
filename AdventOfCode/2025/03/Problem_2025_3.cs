using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2025_3 : Problem2
    {
        public override long GetFirstStar()
        {
            return Lines.Sum(GetMaxJoltage);
        }

        static int GetMaxJoltage(string line)
        {
            for (int i = 99; i >= 11; i--)
            {
                if (Includes(line, i)) return i;
            }

            throw new InvalidOperationException();

            static bool Includes(string line, int i)
            {
                string iStr = i.ToString();
                int first = line.IndexOf(iStr[0]);
                int second = line.LastIndexOf(iStr[1]);
                return first != -1 && first < second;
            }
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 357),
            new Test("797", 97, Name: "Second is picked last")
        ];
    }
}
