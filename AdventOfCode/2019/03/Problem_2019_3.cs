using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// WARNING: takes about 4mn to process.
    /// </summary>
    class Problem_2019_3 : Problem
    {
        public override int Year => 2019;
        public override int Number => 3;

        public override void Run()
        {
            Wire[] wires = new Wire[Lines.Length];
            Vector2 centralPort = new Vector2(0, 0);

            for (int i = 0; i < wires.Length; i++)
            {
                string[] inputs = Lines[i].Split(',');
                wires[i] = new Wire
                {
                    startingPos = centralPort,
                    moves = new Move[inputs.Length]
                };
                for (int j = 0; j < inputs.Length; j++)
                {
                    wires[i].moves[j] = new Move(inputs[j]);
                }
            }

            List<Vector2[]> wiresMoves = new List<Vector2[]>();

            for (int i = 0; i < wires.Length; i++)
            {
                wiresMoves.Add(wires[i].ProcessMoves());
            }

            int minDistance = int.MaxValue;
            int minSteps = int.MaxValue;

            for (int i = 0; i < wiresMoves[0].Length; i++)
            {
                for (int j = 0; j < wiresMoves[1].Length; j++)
                {
                    if (wiresMoves[0][i].Equals(wiresMoves[1][j]))
                    {
                        int steps = i + j + 2;
                        int distance = wiresMoves[0][i].DistanceFrom(centralPort);

                        if (distance < minDistance)
                            minDistance = distance;

                        if (steps < minSteps)
                            minSteps = steps;
                    }
                }

                //float currentProgress = 100 * (i / (float)wiresMoves[0].Length);
                //Console.WriteLine($"{currentProgress}%");
            }

            Console.WriteLine($"First star: {minDistance}");
            Console.WriteLine($"Second star: {minSteps}");
        }

        public class Wire
        {
            public Vector2 startingPos;
            public Move[] moves;

            public Vector2[] ProcessMoves()
            {
                List<Vector2> result = new List<Vector2>();

                result.AddRange(moves[0].GetAddedPos(startingPos));

                for (int i = 1; i < moves.Length; i++)
                {
                    result.AddRange(moves[i].GetAddedPos(result[result.Count - 1]));
                }

                return result.ToArray();
            }
        }

        public class Vector2
        {
            public int x;
            public int y;

            public Vector2(Vector2 other) : this(other.x, other.y) { }

            public Vector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int DistanceFrom(Vector2 other)
            {
                return Math.Abs(other.x - x) + Math.Abs(other.y - y);
            }

            public bool Equals(Vector2 other)
            {
                return x == other.x && y == other.y;
            }

            public override string ToString()
            {
                return $"({x},{y})";
            }
        }

        public class Move
        {
            public Direction dir;
            public int amount;

            public Move(string str)
            {
                switch (str[0])
                {
                    case 'D': dir = Direction.Down; break;
                    case 'U': dir = Direction.Up; break;
                    case 'L': dir = Direction.Left; break;
                    case 'R': dir = Direction.Right; break;
                    default: Console.WriteLine("Error"); break;
                }

                amount = int.Parse(str.Substring(1));
            }

            public Vector2[] GetAddedPos(Vector2 pos)
            {
                Vector2[] result = new Vector2[amount];

                for (int i = 0; i < amount; i++)
                {
                    if (i == 0)
                        result[i] = new Vector2(pos);
                    else
                        result[i] = new Vector2(result[i - 1]);

                    switch (dir)
                    {
                        case Direction.Down: result[i].x++; break;
                        case Direction.Up: result[i].x--; break;
                        case Direction.Left: result[i].y--; break;
                        case Direction.Right: result[i].y++; break;
                        default: Console.WriteLine("Error"); break;
                    }
                }

                return result;
            }

            public override string ToString()
            {
                return $"{dir} {amount}";
            }
        }

        public enum Direction { Up, Down, Left, Right }
    }
}
