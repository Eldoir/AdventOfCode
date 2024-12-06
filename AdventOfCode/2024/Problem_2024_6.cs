using AdventOfCode.Core;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2024_6 : Problem_2024
    {
        public override int Number => 6;

        public override void Run()
        {
            //UseTestInput();
            int height = Lines.Length;
            int width = Lines[0].Length;
            char[,] map = new char[height, width];
            Vector2Int guardPos = new();
            Vector2Int guardDir = new();
            HashSet<Vector2Int> visited = new();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = Lines[i][j];
                    if (map[i, j] != '.' && map[i, j] != '#')
                    {
                        guardPos = new(j, i);
                        visited.Add(guardPos);
                        guardDir = map[i, j] switch
                        {
                            '^' => new Vector2Int(0, -1),
                            'v' => new Vector2Int(0, 1),
                            '<' => new Vector2Int(-1, 0),
                            '>' => new Vector2Int(1, 0),
                            _ => throw new Exception("Invalid guard direction")
                        };
                    }
                }
            }
            while (true)
            {
                Vector2Int nextPos = guardPos + guardDir;
                if (nextPos.x < 0 || nextPos.x >= width || nextPos.y < 0 || nextPos.y >= height)
                    break;

                if (map[nextPos.y, nextPos.x] == '#')
                {
                    guardDir.Rotate(90);
                }
                guardPos += guardDir;
                visited.Add(guardPos);
            }
            Console.WriteLine($"First star: {visited.Count}");
        }
    }
}
