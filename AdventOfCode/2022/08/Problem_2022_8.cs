using System;

namespace AdventOfCode
{
    class Problem_2022_8 : Problem
    {
        public override int Year => 2022;
        public override int Number => 8;

        int[,] map;

        protected override void InitInternal()
        {
            map = new int[Lines.Length, Lines[0].Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[0].Length; j++)
                {
                    map[i, j] = Lines[i][j] - '0';
                }
            }

            base.InitInternal();
        }

        public override void Run()
        {
            int visibleTrees = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (IsTreeVisible(i, j))
                        visibleTrees++;
                }
            }

            Console.WriteLine($"First star: {visibleTrees}");

            int max = int.MinValue;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    int score = GetScenicScore(i, j);
                    if (score > max)
                        max = score;
                }
            }

            Console.WriteLine($"Second star: {max}");
        }

        private bool IsTreeVisible(int x, int y)
        {
            return IsEdge(x, y)
                || CheckTop(x, y) || CheckBottom(x, y)
                || CheckLeft(x, y) || CheckRight(x, y);
        }

        private bool IsEdge(int x, int y)
        {
            return x == 0 || x == map.GetLength(0) - 1
                || y == 0 || y == map.GetLength(1) - 1;
        }

        private bool CheckTop(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                if (map[i, y] >= map[x, y])
                    return false;
            }

            return true;
        }

        private bool CheckBottom(int x, int y)
        {
            for (int i = x + 1; i < map.GetLength(0); i++)
            {
                if (map[i, y] >= map[x, y])
                    return false;
            }

            return true;
        }

        private bool CheckLeft(int x, int y)
        {
            for (int j = 0; j < y; j++)
            {
                if (map[x, j] >= map[x, y])
                    return false;
            }

            return true;
        }

        private bool CheckRight(int x, int y)
        {
            for (int j = y + 1; j < map.GetLength(1); j++)
            {
                if (map[x, j] >= map[x, y])
                    return false;
            }

            return true;
        }

        private int GetScenicScore(int x, int y)
        {
            return GetScenicScoreTop(x, y) * GetScenicScoreBottom(x, y)
                * GetScenicScoreLeft(x, y) * GetScenicScoreRight(x, y);
        }

        private int GetScenicScoreTop(int x, int y)
        {
            int score = 0;
            int i = x - 1;
            while (true)
            {
                if (i < 0)
                    break;

                if (map[i, y] >= map[x, y])
                {
                    score++;
                    break;
                }

                score++;
                i--;
            }

            return score;
        }

        private int GetScenicScoreBottom(int x, int y)
        {
            int score = 0;
            int i = x + 1;
            while (true)
            {
                if (i >= map.GetLength(0))
                    break;

                if (map[i, y] >= map[x, y])
                {
                    score++;
                    break;
                }

                score++;
                i++;
            }

            return score;
        }

        private int GetScenicScoreLeft(int x, int y)
        {
            int score = 0;
            int j = y - 1;
            while (true)
            {
                if (j < 0)
                    break;

                if (map[x, j] >= map[x, y])
                {
                    score++;
                    break;
                }

                score++;
                j--;
            }

            return score;
        }

        private int GetScenicScoreRight(int x, int y)
        {
            int score = 0;
            int j = y + 1;
            while (true)
            {
                if (j >= map.GetLength(1))
                    break;

                if (map[x, j] >= map[x, y])
                {
                    score++;
                    break;
                }

                score++;
                j++;
            }

            return score;
        }
    }
}
