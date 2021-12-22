using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode
{
    class Problem_2021_5 : Problem_2021
    {
        public override int Number => 5;

        private List<Line> lines;

        protected override void InitInternal()
        {
            var regex = new Regex(@"(\d+,\d+) -> (\d+,\d+)");
            lines = new List<Line>();

            foreach (string line in Lines)
            {
                GroupCollection groups = regex.Match(line).Groups;
                string[] from = groups[1].ToString().Split(',');
                var fromPos = new Vector2Int(int.Parse(from[0]), int.Parse(from[1]));
                string[] to = groups[2].ToString().Split(',');
                var toPos = new Vector2Int(int.Parse(to[0]), int.Parse(to[1]));

                lines.Add(new Line(fromPos, toPos));
            }

            base.InitInternal();
        }

        public override void Run()
        {
            Console.WriteLine($"First star: {CountOverlaps(countDiagonals: false)}");
            Console.WriteLine($"First star: {CountOverlaps(countDiagonals: true)}");
        }

        #region Methods

        private int CountOverlaps(bool countDiagonals)
        {
            Dictionary<Vector2Int, int> overlaps = new Dictionary<Vector2Int, int>();

            foreach (Line line in lines)
            {
                var fromPos = line.From;
                var toPos = line.To;

                int xInc = Utils.GetIncrement(fromPos.x, toPos.x);
                int yInc = Utils.GetIncrement(fromPos.y, toPos.y);

                if (xInc == 0) // vertical line
                {
                    int i = fromPos.y;
                    while (i != toPos.y)
                    {
                        AddPosition(new Vector2Int(fromPos.x, i));
                        i += yInc;
                    }
                    AddPosition(new Vector2Int(fromPos.x, i));
                }
                else if (yInc == 0) // horizontal line
                {
                    int j = fromPos.x;
                    while (j != toPos.x)
                    {
                        AddPosition(new Vector2Int(j, fromPos.y));
                        j += xInc;
                    }
                    AddPosition(new Vector2Int(j, fromPos.y));
                }
                else if (countDiagonals) // diagonal line
                {
                    int i = fromPos.y;
                    int j = fromPos.x;
                    while (i != toPos.y)
                    {
                        AddPosition(new Vector2Int(j, i));
                        i += yInc;
                        j += xInc;
                    }
                    AddPosition(new Vector2Int(j, i));
                }
            }

            return overlaps.Where(kvp => kvp.Value > 1).Count();

            #region Local methods

            void AddPosition(Vector2Int pos)
            {
                if (!overlaps.ContainsKey(pos))
                    overlaps.Add(pos, 0);

                overlaps[pos]++;
            }

            #endregion
        }

        #endregion

        class Line
        {
            public Vector2Int From { get; }
            public Vector2Int To { get; }

            public Line(Vector2Int from, Vector2Int to)
            {
                From = from;
                To = to;
            }
        }
    }
}
