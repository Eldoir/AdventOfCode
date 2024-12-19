using AdventOfCode.Core;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    internal class Problem_2024_10 : Problem2
    {
        public override int Year => 2024;
        public override int Number => 10;

        int width;
        int height;
        int[,] map;

        public override long GetFirstStar()
        {
            height = Lines.Length;
            width = Lines[0].Length;
            map = new int[height, width];
            List<Vector2Int> trailheads = [];
            for (int i = 0; i < height; i++)
            {
                for (int  j = 0; j < width; j++)
                {
                    map[i, j] = Lines[i][j] - '0'; // '.' (impassible tiles) will have a value of -2
                    if (map[i, j] == 0)
                    {
                        trailheads.Add(new Vector2Int(j, i));
                    }
                }
            }

            return trailheads.Sum(GetScore);
        }

        private int GetScore(Vector2Int trailhead)
        {
            Queue<Vector2Int> queue = new();
            HashSet<Vector2Int> visited = [];
            int score = 0;

            queue.Enqueue(trailhead);
            visited.Add(trailhead);

            while (queue.Count > 0)
            {
                Vector2Int v = queue.Dequeue();
                int value = map[v.y, v.x];

                if (value == 9)
                {
                    score++;
                    continue;
                }

                if (v.x > 0) TryVisiting(v + Vector2Int.Left, value);
                if (v.x < width - 1) TryVisiting(v + Vector2Int.Right, value);
                if (v.y > 0) TryVisiting(v + Vector2Int.Down, value);
                if (v.y < height - 1) TryVisiting(v + Vector2Int.Up, value);
            }

            return score;

            #region Local methods

            void TryVisiting(Vector2Int pos, int value)
            {
                if (map[pos.y, pos.x] == value + 1)
                {
                    if (!visited.Contains(pos))
                    {
                        queue.Enqueue(pos);
                        visited.Add(pos);
                    }
                }
            }

            #endregion
        }

        public override long GetSecondStar()
        {
            return 1;
        }

        protected override Test[] TestsFirstStar =>
        [
            new("test_1.txt", 2),
            new("test_2.txt", 4),
            new("test_3.txt", 3),
            new("test_4.txt", 36),
            TestPuzzle(822)
        ];

        protected override Test[] TestsSecondStar =>
        [
            new("test_p2_1.txt", 3),
            new("test_p2_2.txt", 13),
            new("test_p2_3.txt", 227),
            new("test_4.txt", 81)
        ];
    }
}
