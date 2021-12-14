using System;
using AdventOfCode.Core;

namespace AdventOfCode
{
    class Problem_2020_12 : Problem_2020
    {
        public override int Number => 12;

        public override void Run()
        {
            var pos = Vector2Int.Zero;
            var dir = Vector2Int.East;

            foreach (var line in Lines)
            {
                char action = line[0];
                int val = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N':
                        pos += Vector2Int.North * val;
                        break;
                    case 'E':
                        pos += Vector2Int.East * val;
                        break;
                    case 'S':
                        pos += Vector2Int.South * val;
                        break;
                    case 'W':
                        pos += Vector2Int.West * val;
                        break;
                    case 'L':
                        dir.Rotate(val);
                        break;
                    case 'R':
                        dir.Rotate(-val);
                        break;
                    case 'F':
                        pos += dir * val;
                        break;
                    default:
                        Console.WriteLine("Unknown action.");
                        break;
                }

                //Console.WriteLine($"[{line.PadLeft(4)}] Pos: {pos}, Dir: {dir}");
            }

            Console.WriteLine($"First star: {pos.GetManhattanDistanceFromZero()}");

            pos = Vector2Int.Zero;
            var waypoint = new Vector2Int(10, 1);

            foreach (var line in Lines)
            {
                char action = line[0];
                int val = int.Parse(line.Substring(1));

                switch (action)
                {
                    case 'N':
                        waypoint += Vector2Int.North * val;
                        break;
                    case 'E':
                        waypoint += Vector2Int.East * val;
                        break;
                    case 'S':
                        waypoint += Vector2Int.South * val;
                        break;
                    case 'W':
                        waypoint += Vector2Int.West * val;
                        break;
                    case 'L':
                        waypoint.Rotate(val);
                        break;
                    case 'R':
                        waypoint.Rotate(-val);
                        break;
                    case 'F':
                        pos += waypoint * val;
                        break;
                    default:
                        Console.WriteLine("Unknown action.");
                        break;
                }

                //Console.WriteLine($"[{line.PadLeft(4)}] Pos: {pos}, Waypoint: {waypoint}");
            }

            Console.WriteLine($"Second star: {pos.GetManhattanDistanceFromZero()}");
        }
    }
}
