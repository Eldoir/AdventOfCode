using System;

namespace AdventOfCode.Core
{
    public static class Utils
    {
        /// <summary>
        /// If <paramref name="n1"/> == <paramref name="n2"/>, returns 0.
        /// If <paramref name="n1"/> is greater than <paramref name="n2"/>, returns 1.
        /// If <paramref name="n1"/> is lower than <paramref name="n2"/>, returns -1.
        /// </summary>
        public static int GetIncrement(int n1, int n2)
        {
            if (n1 == n2)
                return 0;

            if (n1 < n2)
                return 1;

            return -1;
        }

        public static int CountNeighbours<T>(T[,] grid, int i, int j, Predicate<T> pred)
        {
            int result = 0;

            if (i > 0)
            {
                if (pred(grid[i - 1, j])) result++; // Top middle
                if (j > 0 && pred(grid[i - 1, j - 1])) result++; // Top left
                if (j < grid.GetLength(1) - 1 && pred(grid[i - 1, j + 1])) result++; // Top right
            }

            if (i < grid.GetLength(0) - 1)
            {
                if (pred(grid[i + 1, j])) result++; // Bottom middle
                if (j > 0 && pred(grid[i + 1, j - 1])) result++; // Bottom left
                if (j < grid.GetLength(1) - 1 && pred(grid[i + 1, j + 1])) result++; // Bottom right
            }

            if (j > 0 && pred(grid[i, j - 1])) result++; // Middle left
            if (j < grid.GetLength(1) - 1 && pred(grid[i, j + 1])) result++; // Middle right

            return result;
        }
    }
}
