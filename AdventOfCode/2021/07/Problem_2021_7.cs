using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2021_7 : Problem
    {
        public override int Year => 2021;
        public override int Number => 7;

        public delegate int CalcMethod(int baseNb, int nb);

        public override void Run()
        {
            int[] nbs = Text.Split(',').Select(l => int.Parse(l)).ToArray();

            Console.WriteLine($"First star: {Process(nbs, CalcMethodFirstStar)}");
            Console.WriteLine($"Second star: {Process(nbs, CalcMethodSecondStar)}");
        }

        #region Methods

        static int Process(int[] nbs, CalcMethod methodCalc)
        {
            int min = nbs.Min();
            int max = nbs.Max();
            int minDistance = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                int distance = 0;

                foreach (int nb in nbs)
                {
                    distance += methodCalc(nb, i);
                }

                if (distance < minDistance)
                {
                    minDistance = distance;
                }
            }

            return minDistance;
        }

        int CalcMethodFirstStar(int baseNb, int nb)
        {
            return Math.Abs(baseNb - nb);
        }

        int CalcMethodSecondStar(int baseNb, int nb)
        {
            int diff = Math.Abs(baseNb - nb);
            return (diff * diff + diff) / 2;
        }

        #endregion
    }
}
