using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2023_3 : Problem
    {
        public override int Year => 2023;
        public override int Number => 3;

        class Symbol
        {
            public char C { get; }
            private List<int> AdjacentNumbers = new();

            public Symbol(char c)
            {
                C = c;
            }

            public void AddAdjacentNumber(int n)
            {
                AdjacentNumbers.Add(n);
            }

            public int GetGearRatio()
            {
                if (AdjacentNumbers.Count != 2)
                    return 0;

                return AdjacentNumbers.Aggregate(1, (total, next) => total * next);
            }
        }

        public override void Run()
        {
            Dictionary<Vector2Int, Symbol> symbols = new();
            Dictionary<Vector2Int, int> numbers = new();

            for (int i = 0; i < Lines.Length; i++)
            {
                int numIdx = -1;
                string line = Lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];
                    int d = c - '0';
                    if (c == 13)
                        continue; // carriage return
                    if (d >= 0 && d <= 9)
                        if (numIdx == -1) // new number
                            numIdx = j;
                        else
                            continue; // middle of number
                    else
                    {
                        if (c != '.')
                            symbols.Add(new Vector2Int(j, i), new Symbol(c));
                        
                        if (numIdx != -1) // end of number
                            numbers.Add(new Vector2Int(numIdx, i), int.Parse(line.Substring(numIdx, j - numIdx)));

                        numIdx = -1;
                    }
                }

                if (numIdx != -1) // end of number
                    numbers.Add(new Vector2Int(numIdx, i), int.Parse(line.Substring(numIdx)));
            }

            int firstStar = 0;
            foreach (var kvp in numbers)
            {
                int length = (int)Math.Log10(kvp.Value) + 1;
                int minX = kvp.Key.x - 1;
                int maxX = kvp.Key.x + length;
                foreach (var s in symbols)
                {
                    if (s.Key.x >= minX && s.Key.x <= maxX && Math.Abs(kvp.Key.y - s.Key.y) <= 1)
                    {
                        firstStar += kvp.Value;
                        if (s.Value.C == '*')
                        {
                            s.Value.AddAdjacentNumber(kvp.Value);
                        }
                        break;
                    }
                }
            }

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {symbols.Values.Sum(s => s.GetGearRatio())}");
        }
    }
}
