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

                Vector2Int leftNeighbor = v + Vector2Int.Left;
                if (v.x > 0 && map[leftNeighbor.y, leftNeighbor.x] == value + 1)
                {
                    if (!visited.Contains(leftNeighbor))
                    {
                        queue.Enqueue(leftNeighbor);
                        visited.Add(leftNeighbor);
                    }
                }
                Vector2Int rightNeighbor = v + Vector2Int.Right;
                if (v.x < width - 1 && map[rightNeighbor.y, rightNeighbor.x] == value + 1)
                {
                    if (!visited.Contains(rightNeighbor))
                    {
                        queue.Enqueue(rightNeighbor);
                        visited.Add(rightNeighbor);
                    }
                }
                Vector2Int topNeighbor = v + Vector2Int.Down; // Y is 0 to N from top to bottom
                if (v.y > 0 && map[topNeighbor.y, topNeighbor.x] == value + 1)
                {
                    if (!visited.Contains(topNeighbor))
                    {
                        queue.Enqueue(topNeighbor);
                        visited.Add(topNeighbor);
                    }
                }
                Vector2Int bottomNeighbor = v + Vector2Int.Up;
                if (v.y < height - 1 && map[bottomNeighbor.y, bottomNeighbor.x] == value + 1)
                {
                    if (!visited.Contains(bottomNeighbor))
                    {
                        queue.Enqueue(bottomNeighbor);
                        visited.Add(bottomNeighbor);
                    }
                }
            }

            return score;
        }

        protected override Test[] TestsFirstStar =>
        [
            new("test_1.txt", 2),
            new("test_2.txt", 4),
            new("test_3.txt", 3),
            new("test_4.txt", 36)
        ];

        protected override Test[] TestsSecondStar => base.TestsSecondStar;
    }
}
