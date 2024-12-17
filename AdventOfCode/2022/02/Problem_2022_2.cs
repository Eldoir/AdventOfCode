using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2022_2 : Problem
    {
        public override int Year => 2022;
        public override int Number => 2;

        private readonly Dictionary<string, int> firstStarTable = new()
        {
            { "A X", 4 },
            { "A Y", 8 },
            { "A Z", 3 },
            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },
            { "C X", 7 },
            { "C Y", 2 },
            { "C Z", 6 },
        };

        private readonly Dictionary<string, int> secondStarTable = new()
        {
            { "A X", 3 },
            { "A Y", 4 },
            { "A Z", 8 },
            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },
            { "C X", 2 },
            { "C Y", 6 },
            { "C Z", 7 },
        };

        public override void Run()
        {
            Console.WriteLine($"First star: {Lines.Sum(l => firstStarTable[l])}");
            Console.WriteLine($"Second star: {Lines.Sum(l => secondStarTable[l])}");
        }
    }
}
