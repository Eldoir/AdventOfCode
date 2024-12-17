using System;

namespace AdventOfCode
{
    class Problem_2015_18 : Problem
    {
        public override int Year => 2015;
        public override int Number => 18;

        public override void Run()
        {
            int[,] originalGrid = new int[Lines.Length, Lines[0].Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[i].Length; j++)
                {
                    originalGrid[i, j] = Lines[i][j] == '#' ? 1 : 0;
                }
            }

            int[,] firstStarGrid = CopyArray(originalGrid);
            int[,] secondStarGrid = CopyArray(originalGrid);
            secondStarGrid = GetCornersTurnedOn(secondStarGrid);

            for (int i = 0; i < 100; i++)
            {
                firstStarGrid = Process(firstStarGrid, false);
                secondStarGrid = Process(secondStarGrid, true);
            }

            Console.WriteLine($"First star: {CountLightsOn(firstStarGrid)}");
            Console.WriteLine($"Second star: {CountLightsOn(secondStarGrid)}");
        }

        private int[,] Process(int[,] grid, bool exceptCorners)
        {
            int[,] newGrid = new int[grid.GetLength(0), grid.GetLength(1)];
            newGrid = GetCornersTurnedOn(newGrid);

            for (int i = 0; i < newGrid.GetLength(0); i++)
            {
                for (int j = 0; j < newGrid.GetLength(1); j++)
                {
                    if (exceptCorners && IsCorner(newGrid, i, j))
                        continue;

                    int neighbours = CountNeighbours(grid, i, j);

                    if (grid[i, j] == 1) // Light on
                    {
                        newGrid[i, j] = (neighbours == 2 || neighbours == 3 ? 1 : 0);
                    }
                    else if (grid[i, j] == 0) // Light off
                    {
                        newGrid[i, j] = (neighbours == 3 ? 1 : 0);
                    }
                }
            }

            return newGrid;
        }

        private int CountNeighbours(int[,] grid, int i, int j)
        {
            int result = 0;

            if (i > 0)
            {
                if (grid[i - 1, j] == 1) result++; // Top middle
                if (j > 0 && grid[i - 1, j - 1] == 1) result++; // Top left
                if (j < grid.GetLength(1) - 1 && grid[i - 1, j + 1] == 1) result++; // Top right
            }

            if (i < grid.GetLength(0) - 1)
            {
                if (grid[i + 1, j] == 1) result++; // Bottom middle
                if (j > 0 && grid[i + 1, j - 1] == 1) result++; // Bottom left
                if (j < grid.GetLength(1) - 1 && grid[i + 1, j + 1] == 1) result++; // Bottom right
            }

            if (j > 0 && grid[i, j - 1] == 1) result++; // Middle left
            if (j < grid.GetLength(1) - 1 && grid[i, j + 1] == 1) result++; // Middle right

            return result;
        }

        private int CountLightsOn(int[,] grid)
        {
            int total = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    total += grid[i, j] == 1 ? 1 : 0;
                }
            }

            return total;
        }

        private int[,] GetCornersTurnedOn(int[,] grid)
        {
            grid[0, 0] = 1; // Top left
            grid[0, grid.GetLength(1) - 1] = 1; // Top right
            grid[grid.GetLength(0) - 1, 0] = 1; // Bottom left
            grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1] = 1;

            return grid;
        }

        private bool IsCorner(int[,] grid, int i, int j)
        {
            return (i == 0 && j == 0 || // Top left
                    i == 0 && j == grid.GetLength(1) - 1 || // Top right
                    i == grid.GetLength(0) - 1 && j == 0 || // Bottom left
                    i == grid.GetLength(0) - 1 && j == grid.GetLength(1) - 1); // Bottom right

        }

        private T[,] CopyArray<T>(T[,] arr)
        {
            T[,] result = new T[arr.GetLength(0), arr.GetLength(1)];
            Buffer.BlockCopy(arr, 0, result, 0, arr.Length * sizeof(int));
            return result;
        }
    }
}
