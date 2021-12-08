using System;

namespace AdventOfCode
{
    class Problem_2015_6 : Problem_2015
    {
        public override int Number => 6;

        public delegate void DoOnLight(int x, int y);

        public override void Run()
        {
            int gridSize = 1000;
            bool[,] lights1 = new bool[gridSize, gridSize];
            int[,] lights2 = new int[gridSize, gridSize];

            for (int i = 0; i < Lines.Length; i++)
            {
                Process(lights1, Lines[i]);
                Process(lights2, Lines[i]);
            }

            Console.WriteLine($"First star: {CountLightsLit(lights1)}");
            Console.WriteLine($"Second star: {CountBrightness(lights2)}");
        }

        private void Process(bool[,] lights, string str)
        {
            string[] inputs = str.Split(' ');

            if (inputs.Length == 4) // toggle
            {
                Position start = Position.FromString(inputs[1]);
                Position end = Position.FromString(inputs[3]);

                DoOnRange(start, end, (x, y) => lights[x, y] = !lights[x, y]);
            }
            else if (inputs.Length == 5) // turn on/off
            {
                bool on = inputs[1] == "on";
                Position start = Position.FromString(inputs[2]);
                Position end = Position.FromString(inputs[4]);

                DoOnRange(start, end, (x, y) => lights[x, y] = on);
            }
            else
            {
                Console.WriteLine("Problem");
            }
        }

        private void Process(int[,] lights, string str)
        {
            string[] inputs = str.Split(' ');

            if (inputs.Length == 4) // toggle
            {
                Position start = Position.FromString(inputs[1]);
                Position end = Position.FromString(inputs[3]);

                DoOnRange(start, end, (x, y) => lights[x, y] += 2);
            }
            else if (inputs.Length == 5) // turn on/off
            {
                bool on = inputs[1] == "on";
                Position start = Position.FromString(inputs[2]);
                Position end = Position.FromString(inputs[4]);

                DoOnRange(start, end, (x, y) =>
                {
                    lights[x, y] = Clamp(lights[x, y] + (on ? 1 : -1), 0, int.MaxValue);
                });
            }
            else
            {
                Console.WriteLine("Problem");
            }
        }

        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            else if (value > max) return max;
            else return value;
        }

        private void DoOnRange(Position start, Position end, DoOnLight doCallback)
        {
            for (int i = start.x; i <= end.x; i++)
            {
                for (int j = start.y; j <= end.y; j++)
                {
                    doCallback(i, j);
                }
            }
        }

        private int CountLightsLit(bool[,] lights)
        {
            int count = 0;

            for (int i = 0; i < lights.GetLength(0); i++)
            {
                for (int j = 0; j < lights.GetLength(1); j++)
                {
                    count += lights[i, j] ? 1 : 0;
                }
            }

            return count;
        }

        private int CountBrightness(int[,] lights)
        {
            int count = 0;

            for (int i = 0; i < lights.GetLength(0); i++)
            {
                for (int j = 0; j < lights.GetLength(1); j++)
                {
                    count += lights[i, j];
                }
            }

            return count;
        }

        class Position
        {
            public int x, y;

            public static Position FromString(string str)
            {
                string[] inputs = str.Split(',');

                return new Position
                {
                    x = int.Parse(inputs[0]),
                    y = int.Parse(inputs[1])
                };
            }

            public override string ToString()
            {
                return $"{x},{y}";
            }
        }
    }
}
