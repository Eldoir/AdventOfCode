using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2021_9 : Problem_2021
    {
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
            int[] lowPoints = GetLowPoints();
            Console.WriteLine($"First star: {lowPoints.Sum() + lowPoints.Length}");
        }

        #region Methods

        private int[] GetLowPoints()
        {
            var lowPoints = new List<int>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (IsLowPoint(i, j))
                    {
                        lowPoints.Add(map[i][j]);
                    }
                }
            }

            return lowPoints.ToArray();
        }

        private bool IsLowPoint(int i, int j)
        {
            return map[i][j] < GetNeighbours(i, j).Min();
        }

        private int[] GetNeighbours(int i, int j)
        {
            var neighbours = new List<int>();

            if (i > 0)
            {
                if (j > 0) neighbours.Add(map[i - 1][j - 1]); // top left
                if (j < width - 1) neighbours.Add(map[i - 1][j + 1]); // top right
                neighbours.Add(map[i - 1][j]); // top middle
            }

            if (j > 0) neighbours.Add(map[i][j - 1]); // middle left
            if (j < width - 1) neighbours.Add(map[i][j + 1]); // middle right;

            if (i < height - 1)
            {
                if (j > 0) neighbours.Add(map[i + 1][j - 1]); // bottom left
                if (j < width - 1) neighbours.Add(map[i + 1][j + 1]); // bottom right
                neighbours.Add(map[i + 1][j]); // bottom middle
            }

            return neighbours.ToArray();
        }

        #endregion
    }
}
