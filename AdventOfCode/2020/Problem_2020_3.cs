using System;

namespace AdventOfCode
{
    class Problem_2020_3 : Problem_2020
    {
        public override int Number => 3;

        public override void Run()
        {
            Console.WriteLine($"First star: {GetTreesEncountered(1, 3)}");

            var slopes = new (int, int)[]
            {
                (1, 1),
                (1, 3),
                (1, 5),
                (1, 7),
                (2, 1),
            };

            long totalSecondStar = 1;

            foreach (var slope in slopes)
            {
                totalSecondStar *= GetTreesEncountered(slope.Item1, slope.Item2);
            }

            Console.WriteLine($"Second star: {totalSecondStar}");
        }

        #region Methods

        int GetTreesEncountered(int xInc, int yInc)
        {
            var count = 0;

            var x = 0;
            var y = 0;

            var lineLength = Lines[0].Length;

            while (x < Lines.Length)
            {
                if (Lines[x][y] == '#')
                {
                    count++;
                }

                y = (y + yInc) % lineLength;
                x += xInc;
            }

            return count;
        }

        #endregion
    }
}
