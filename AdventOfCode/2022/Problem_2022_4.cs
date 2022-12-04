using AdventOfCode.Core;
using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2022_4 : Problem_2022
    {
        public override int Number => 4;
        Pair[] pairs;

        protected override void InitInternal()
        {
            pairs = new Pair[Lines.Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                pairs[i] = GetPair(Lines[i]);
            }

            base.InitInternal();
        }

        public override void Run()
        {
            Console.WriteLine($"First star: {pairs.Count(p => p.DoesOneContainTheOther())}");
            Console.WriteLine($"Second star: {pairs.Count(p => p .DoesOverlap())}");
        }

        private Pair GetPair(string str)
        {
            int[][] sp = str.Split(',').Select(s => s.Split('-').Select(ss => int.Parse(ss)).ToArray()).ToArray();
            return new Pair(sp[0][0], sp[0][1], sp[1][0], sp[1][1]);
        }

        class Pair
        {
            private IntervalInt elve1;
            private IntervalInt elve2;

            public Pair(int elve1Min, int elve1Max, int elve2Min, int elve2Max)
            {
                elve1 = new IntervalInt(elve1Min, elve1Max);
                elve2 = new IntervalInt(elve2Min, elve2Max);
            }

            public bool DoesOneContainTheOther()
            {
                return elve1.min <= elve2.min && elve1.max >= elve2.max
                    || elve2.min <= elve1.min && elve2.max >= elve1.max;
            }

            public bool DoesOverlap()
            {
                if (elve1.min <= elve2.min)
                    return elve1.max >= elve2.min;
                else
                    return elve2.max >= elve1.min;
            }
        }
    }
}
