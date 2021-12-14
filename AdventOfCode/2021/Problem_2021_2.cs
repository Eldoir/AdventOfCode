using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2021_2 : Problem_2021
    {
        enum Direction
        {
            None = 0,
            Forward,
            Up,
            Down
        }

        public override int Number => 2;

        private List<KeyValuePair<Direction, int>> directions;

        private int horizontal;
        private int depth;

        protected override void InitInternal()
        {
            directions = new List<KeyValuePair<Direction, int>>();

            foreach (string line in Lines)
            {
                string[] split = line.Split(' ');
                directions.Add(new KeyValuePair<Direction, int>(ToDirection(split[0]), int.Parse(split[1])));
            }

            base.InitInternal();
        }

        public override void Run()
        {
            foreach (var kvp in directions)
            {
                switch (kvp.Key)
                {
                    case Direction.Forward:
                        horizontal += kvp.Value;
                        break;
                    case Direction.Up:
                        depth -= kvp.Value;
                        break;
                    case Direction.Down:
                        depth += kvp.Value;
                        break;
                }
            }

            Console.WriteLine($"First star: {horizontal * depth}");

            horizontal = 0;
            depth = 0;

            int aim = 0;

            foreach (var kvp in directions)
            {
                switch (kvp.Key)
                {
                    case Direction.Forward:
                        horizontal += kvp.Value;
                        depth += aim * kvp.Value;
                        break;
                    case Direction.Up:
                        aim -= kvp.Value;
                        break;
                    case Direction.Down:
                        aim += kvp.Value;
                        break;
                }
            }

            Console.WriteLine($"Second star: {horizontal * depth}");
        }

        #region Methods

        private Direction ToDirection(string s)
        {
            switch (s)
            {
                case "forward": return Direction.Forward;
                case "up": return Direction.Up;
                case "down": return Direction.Down;
                default: return Direction.None;
            }
        }

        #endregion
    }
}
