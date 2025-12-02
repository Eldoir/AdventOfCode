namespace AdventOfCode
{
    class Problem_2025_1 : Problem2
    {
        public override long GetFirstStar()
        {
            int current = 50;
            int sum = 0;
            for (int i = 0; i < Lines.Length; i++)
            {
                string line = Lines[i];
                current += (line[0] == 'L' ? -1 : 1) * int.Parse(line.Substring(1));
                if (current % 100 == 0) sum++;
            }
            return sum;
        }

        public override long GetSecondStar()
        {
            return 0;
        }

        protected override Test[] TestsFirstStar => [
            new Test("example", 3)    
        ];
    }
}
