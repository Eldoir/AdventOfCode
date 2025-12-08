namespace AdventOfCode
{
    class Problem_2025_4 : Problem2
    {
        public override long GetFirstStar()
        {
            var map = new bool[Lines.Length, Lines[0].Length];
            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[i].Length; j++)
                {
                    map[i, j] = Lines[i][j] == '@';
                }
            }

            int sum = 0;
            for (int i = 0; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines.Length; j++)
                {
                    if (map[i, j] && Core.Utils.CountNeighbours(map, i, j, b => b) < 4) sum++;
                }
            }
            return sum;
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 13)
        ];
    }
}
