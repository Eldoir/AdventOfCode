using AdventOfCode.Core;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2023_3 : Problem_2023
    {
        public override int Number => 3;

        public override void Run()
        {
            //UseTestInput();
            Dictionary<Vector2Int, char> symbols = new();
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
                            symbols.Add(new Vector2Int(j, i), c);
                        
                        if (numIdx != -1) // end of number
                            numbers.Add(new Vector2Int(numIdx, i), int.Parse(line.Substring(numIdx, j - numIdx)));

                        numIdx = -1;
                    }
                }

                if (numIdx != -1) // end of number
                    numbers.Add(new Vector2Int(numIdx, i), int.Parse(line.Substring(numIdx)));
            }

            int sum = 0;
            foreach (var kvp in numbers)
            {
                int length = (int)Math.Log10(kvp.Value) + 1;
                int minX = kvp.Key.x - 1;
                int maxX = kvp.Key.x + length;
                foreach (var s in symbols)
                {
                    if (s.Key.x >= minX && s.Key.x <= maxX && Math.Abs(kvp.Key.y - s.Key.y) <= 1)
                    {
                        sum += kvp.Value;
                        break;
                    }
                }
            }

            Console.WriteLine($"First star: {sum}");
            Console.WriteLine($"Second star: {0}");
        }
    }
}
