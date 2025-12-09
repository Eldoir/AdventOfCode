#nullable enable
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2025_7 : Problem2
    {
        public override long GetFirstStar()
        {
            ProblemData problem = Parse();
            Stack<(int X, int Y)> beams = [];
            beams.Push((problem.StartX, 0));
            int uniqueHits = 0;
            while (beams.Count > 0)
            {
                (int beamX, int beamY) = beams.Pop();
                if (!problem.Splitters.ContainsKey(beamX)) continue; // no splitter on this column
                Splitter? splitter = problem.Splitters[beamX].FirstOrDefault(splitter => splitter.Y > beamY);
                if (splitter is null || splitter.Hit) continue; // no splitter below the beam on this column, or it's already hit
                splitter.SetHit();
                uniqueHits++;
                if (beamX > 0) beams.Push((beamX - 1, splitter.Y));
                if (beamX < Lines[0].Length - 1) beams.Push((beamX + 1, splitter.Y));
            }
            return uniqueHits;
        }

        public override long GetSecondStar()
        {
            ProblemData problem = Parse();

            return base.GetSecondStar();
        }

        ProblemData Parse()
        {
            int startX = Lines[0].IndexOf('S');
            Dictionary<int, List<Splitter>> splitters = []; // indexed by X
            for (int i = 1; i < Lines.Length; i++)
            {
                for (int j = 0; j < Lines[i].Length; j++)
                {
                    if (Lines[i][j] == '^')
                    {
                        Splitter splitter = new(j, i);

                        if (!splitters.ContainsKey(splitter.X))
                            splitters.Add(splitter.X, []);

                        splitters[j].Add(splitter);
                    }
                }
            }
            return new ProblemData(startX, splitters);
        }

        record class ProblemData(int StartX, Dictionary<int, List<Splitter>> Splitters);

        class Splitter
        {
            public int X { get; }
            public int Y { get; }
            public bool Hit { get; private set; }

            public Splitter(int x, int y)
            {
                X = x;
                Y = y;
                Hit = false;
            }
            
            public void SetHit()
            {
                Hit = true;
            }
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 21)
        ];

        protected override Test[] TestsSecondStar =>
       [
           new Test("example", 40)
       ];
    }
}