#nullable enable
using AdventOfCode.Core;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2024_6 : Problem_2024
    {
        public override int Number => 6;

        int height, width;
        char[,] map;

        public override void Run()
        {
            //UseTestInput();
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

            RunGuard(startPos, startDir, (pos, dir) => { visited.Add(pos); return true; });

            Console.WriteLine($"First star: {visited.Count}");

            int nbObstructionsCausingLoop = 0;

            foreach (Vector2Int visitedPos in visited)
            {
                if (visitedPos == startPos)
                    continue; // we don't want to obstruct guard's starting position

                HashSet<(Vector2Int Pos, Vector2Int Dir)> visitedSecond = new()
                {
                    (startPos, startDir)
                };
                map[visitedPos.y, visitedPos.x] = '#';

                RunGuard(startPos, startDir, (pos, dir) => {
                    if (!visitedSecond.Add((pos, dir)))
                    {
                        nbObstructionsCausingLoop++;
                        return false;
                    }
                    return true;
                });

                map[visitedPos.y, visitedPos.x] = '.';
            }

            Console.WriteLine($"Second star: {nbObstructionsCausingLoop}");
        }

        /// <param name="onStep">If returns false, tells this method to return immediately.</param>
        void RunGuard(Vector2Int startPos, Vector2Int startDir, Func<Vector2Int, Vector2Int, bool> onStep = null)
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
                if (onStep is not null && !onStep.Invoke(pos, dir))
                    return;
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
