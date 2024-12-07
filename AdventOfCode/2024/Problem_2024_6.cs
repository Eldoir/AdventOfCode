#nullable enable
using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode
{
    class Problem_2024_6 : Problem_2024
    {
        public override int Number => 6;

        int height, width;
        char[,] map;

        public override void Run()
        {
            UseTestInput();
            height = Lines.Length;
            width = Lines[0].Length;
            map = new char[height, width];
            Vector2Int startPos = new();
            Vector2Int startDir = new();
            HashSet<Vector2Int> visited = new();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = Lines[i][j];
                    if (map[i, j] != '.' && map[i, j] != '#')
                    {
                        startPos = new(j, i);
                        visited.Add(startPos);
                        startDir = map[i, j] switch
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

            RunGuard(startPos, startDir, (pos, dir) => visited.Add(pos));

            Console.WriteLine($"First star: {visited.Count}");

            foreach (Vector2Int visitedPos in visited)
            {
                map[visitedPos.y, visitedPos.x] = '#';

                // TODO

                map[visitedPos.y, visitedPos.x] = '.';
            }
        }

        bool IsLoop(Vector2Int pos, Vector2Int dir)
        {
            return false;
        }

        void RunGuard(Vector2Int startPos, Vector2Int startDir, Action<Vector2Int, Vector2Int> onStep = null)
        {
            Vector2Int pos = new(startPos);
            Vector2Int dir = new(startDir);
            while (true)
            {
                Vector2Int? newDir = GetNewDir(pos, dir);
                if (newDir is null)
                    break;

                pos += newDir;
                dir = newDir;
                onStep?.Invoke(pos, dir);
            }
        }

        /// <returns>
        /// null if out of bounds
        /// </returns>
        Vector2Int? GetNewDir(Vector2Int pos, Vector2Int dir)
        {
            Vector2Int nextDir = new(dir);
            Vector2Int nextPos = pos + dir;
            if (nextPos.x < 0 || nextPos.x >= width || nextPos.y < 0 || nextPos.y >= height)
                return null;

            if (map[nextPos.y, nextPos.x] == '#')
            {
                nextDir.Rotate(90);
            }
            return nextDir;
        }
    }
}
