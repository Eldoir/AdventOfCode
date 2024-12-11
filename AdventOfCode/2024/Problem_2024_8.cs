using AdventOfCode.Core;
using AdventOfCode.Utils;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2024_8 : Problem_2024
    {
        public override int Number => 8;

        public override void Run()
        {
            //UseTestInput();
            Dictionary<char, List<Vector2Int>> antennas = new();
            HashSet<Vector2Int> antiNodes = new();
            HashSet<Vector2Int> antiNodesSecondStar = new();
            int height = Lines.Length;
            int width = Lines[0].Length;
            Vector2Int size = new(width, height);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    char c = Lines[i][j];
                    Vector2Int pos = new(j, i);
                    if (c != '.')
                    {
                        if (!antennas.TryGetValue(c, out List<Vector2Int> cAntennas))
                        {
                            cAntennas = new List<Vector2Int>();
                            antennas[c] = cAntennas;
                        }
                        foreach (Vector2Int cAntenna in cAntennas)
                        {
                            Vector2Int delta = pos - cAntenna;

                            // First star
                            Vector2Int antiNode1 = cAntenna - delta;
                            if (antiNode1.In(size))
                            {
                                antiNodes.Add(antiNode1);
                            }
                            Vector2Int antiNode2 = pos + delta;
                            if (antiNode2.In(size))
                            {
                                antiNodes.Add(antiNode2);
                            }

                            // Second star
                            Vector2Int pos2 = new(cAntenna);
                            while (pos2.In(size))
                            {
                                antiNodesSecondStar.Add(new Vector2Int(pos2));
                                pos2 -= delta;
                            }
                            pos2 = new(pos);
                            while (pos2.In(size))
                            {
                                antiNodesSecondStar.Add(new Vector2Int(pos2));
                                pos2 += delta;
                            }
                        }
                        cAntennas.Add(pos);
                    }
                }
            }
            Console.WriteLine($"First star: {antiNodes.Count}");
            Console.WriteLine($"Second star: {antiNodesSecondStar.Count}");
        }
    }
}
