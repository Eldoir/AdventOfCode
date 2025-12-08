using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2025_4 : Problem2
    {
        public override long GetFirstStar()
        {
            bool[,] map = GetMap();
            return RunAlgo(map, steps: 1);
        }

        public override long GetSecondStar()
        {
            bool[,] map = GetMap();
            return RunAlgo(map, steps: null);
        }
        
        /// <param name="steps">If null, will loop forever until no rolls are removed.</param>
        int RunAlgo(bool[,] map, int? steps)
        {
            HashSet<(int, int)> rollsPos = [];
            bool[,] nextMap = new bool[map.GetLength(0), map.GetLength(1)];
            int step = 0;
            int rollsRemovedThisStep;
            do
            {
                rollsRemovedThisStep = 0;
                for (int i = 0; i < Lines.Length; i++)
                {
                    for (int j = 0; j < Lines.Length; j++)
                    {
                        if (map[i, j] && !rollsPos.Contains((i, j)) && Core.Utils.CountNeighbours(map, i, j, b => b) < 4)
                        {
                            rollsPos.Add((i, j));
                            nextMap[i, j] = false; // remove roll
                            rollsRemovedThisStep++;
                        }
                        else
                        {
                            nextMap[i, j] = map[i, j];
                        }
                    }
                }
                map = nextMap;
                step++;
            }
            while ((steps is null || step < steps) && rollsRemovedThisStep > 0);
            return rollsPos.Count;
        }

        bool[,] GetMap()
        {
            var map = new bool[Lines.Length, Lines[0].Length];
            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[i].Length; j++)
                {
                    map[i, j] = Lines[i][j] == '@';
                }
            }
            return map;
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 13)
        ];

        protected override Test[] TestsSecondStar =>
        [
            new Test("example", 43)
        ];
    }
}
