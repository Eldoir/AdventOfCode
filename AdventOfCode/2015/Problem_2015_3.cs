using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2015_3 : Problem_2015
    {
        public override int Number => 3;

        public override void Run()
        {
            Position santaPos = new Position();
            Position robotPos = new Position();
            HashSet<string> visitedPos = new HashSet<string>();

            for (int i = 0; i < Text.Length; i++)
            {
                if (i % 2 == 0)
                {
                    string santaPosStr = santaPos.ToString();

                    if (!visitedPos.Contains(santaPosStr))
                    {
                        visitedPos.Add(santaPosStr);
                    }

                    santaPos.Move(Text[i]);
                }
                else
                {
                    string robotPosStr = robotPos.ToString();

                    if (!visitedPos.Contains(robotPosStr))
                    {
                        visitedPos.Add(robotPosStr);
                    }

                    robotPos.Move(Text[i]);
                }
            }

            // :TODO: First star should be 2081
            Console.WriteLine($"Second star: {visitedPos.Count}");
        }

        class Position
        {
            public int x, y;

            public void Move(char c)
            {
                if (c == '^') x--;
                if (c == 'v') x++;
                if (c == '<') y--;
                if (c == '>') y++;
            }

            public override string ToString()
            {
                return $"({x},{y})";
            }
        }
    }
}
