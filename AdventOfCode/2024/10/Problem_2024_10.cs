namespace AdventOfCode
{
    internal class Problem_2024_10 : Problem2
    {
        public override int Year => 2024;
        public override int Number => 10;

        public override long GetFirstStar()
        {
            return base.GetFirstStar();
        }

        protected override Test[] TestsFirstStar => new Test[]
        {
            new("test_1.txt", 36)
        };

        protected override Test[] TestsSecondStar => base.TestsSecondStar;
    }
}
