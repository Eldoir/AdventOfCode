using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode
{
    class Problem_2021_9 : Problem
    {
        public override int Year => 2021;
        public override int Number => 9;

        int[][] map;
        int width;
        int height;

        protected override void InitInternal()
        {
            width = Lines[0].Length;
            height = Lines.Length;

            map = new int[height][];

            for (int i = 0; i < height; i++)
            {
                map[i] = new int[width];
                for (int j = 0; j < width; j++)
                {
                    map[i][j] = Lines[i][j] - '0';
                }
            }

            base.InitInternal();
        }

        public override void Run()
        {
            Vector2Int[] lowPoints = GetLowPoints();
            Console.WriteLine($"First star: {lowPoints.Sum(p => map[p.x][p.y]) + lowPoints.Length}");
            Vector2Int[][] basins = lowPoints.Select(p => GetBasin(p)).ToArray();
            var threeLargestBasins = basins.OrderByDescending(b => b.Length).Take(3);
            int mul = 1;
            foreach (var basin in threeLargestBasins)
            {
                mul *= basin.Length;
            }
            Console.WriteLine($"Second star: {mul}");
        }

        #region Methods

        private Vector2Int[] GetLowPoints()
        {
            var lowPoints = new List<Vector2Int>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var pos = new Vector2Int(i, j);
                    if (IsLowPoint(pos))
                    {
                        lowPoints.Add(pos);
                    }
                }
            }

            return lowPoints.ToArray();
        }

        private bool IsLowPoint(Vector2Int pos)
        {
            return map[pos.x][pos.y] < GetNeighbours(pos).Min();
        }

        private int[] GetNeighbours(Vector2Int pos)
        {
            var neighbours = new List<int>();

            if (pos.x > 0)
            {
                if (pos.y > 0) neighbours.Add(map[pos.x - 1][pos.y - 1]); // top left
                if (pos.y < width - 1) neighbours.Add(map[pos.x - 1][pos.y + 1]); // top right
                neighbours.Add(map[pos.x - 1][pos.y]); // top middle
            }

            if (pos.y > 0) neighbours.Add(map[pos.x][pos.y - 1]); // middle left
            if (pos.y < width - 1) neighbours.Add(map[pos.x][pos.y + 1]); // middle right;

            if (pos.x < height - 1)
            {
                if (pos.y > 0) neighbours.Add(map[pos.x + 1][pos.y - 1]); // bottom left
                if (pos.y < width - 1) neighbours.Add(map[pos.x + 1][pos.y + 1]); // bottom right
                neighbours.Add(map[pos.x + 1][pos.y]); // bottom middle
            }

            return neighbours.ToArray();
        }

        /// <summary>
        /// Flood fill algorithm
        /// </summary>
        private Vector2Int[] GetBasin(Vector2Int pos)
        {
            var positions = new List<Vector2Int>();

            var considered = new List<Vector2Int>();

            var considering = new Stack<Vector2Int>();
            considering.Push(pos);

            while (considering.Count > 0)
            {
                Vector2Int p = considering.Pop();

                if (considered.Contains(p))
                    continue;

                if (p.x >= 0 && p.x < height && p.y >= 0 && p.y < width)
                {
                    if (map[p.x][p.y] != 9)
                    {
                        positions.Add(p);
                        considering.Push(p + Vector2Int.UpIdx);
                        considering.Push(p + Vector2Int.RightIdx);
                        considering.Push(p + Vector2Int.DownIdx);
                        considering.Push(p + Vector2Int.LeftIdx);
                    }
                }

                considered.Add(p);
            }

            return positions.ToArray();
        }

        #endregion
    }
}
