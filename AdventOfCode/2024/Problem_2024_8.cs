using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_8 : Problem_2024
    {
        public override int Number => 8;

        public override void Run()
        {
            //UseTestInput();
            Dictionary<char, List<Vector2Int>> antennas = new();
            Dictionary<char, List<Vector2Int>> antiNodes = new();
            Dictionary<char, List<Vector2Int>> antiNodesSecondStar = new();
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
                        if (!antiNodes.TryGetValue(c, out List<Vector2Int> cAntiNodes))
                        {
                            cAntiNodes = new List<Vector2Int>();
                            antiNodes[c] = cAntiNodes;
                            antiNodesSecondStar.Add(c, new List<Vector2Int>());
                        }
                        foreach (Vector2Int cAntenna in cAntennas)
                        {
                            Vector2Int delta = pos - cAntenna;
                            cAntiNodes.Add(cAntenna - delta);
                            cAntiNodes.Add(pos + delta);

                            Vector2Int pos2 = new(cAntenna);
                            while (pos2.In(size))
                            {
                                antiNodesSecondStar[c].Add(new Vector2Int(pos2));
                                pos2 -= delta;
                            }
                            pos2 = new(pos);
                            while (pos2.In(size))
                            {
                                antiNodesSecondStar[c].Add(new Vector2Int(pos2));
                                pos2 += delta;
                            }
                        }
                        cAntennas.Add(pos);
                    }
                }
            }
            Console.WriteLine($"First star: {antiNodes.Values.SelectMany(p => p).Where(p => p.In(size)).Distinct().Count()}");
            Console.WriteLine($"Second star: {antiNodesSecondStar.Values.SelectMany(p => p).Distinct().Count()}");
        }
    }
}
