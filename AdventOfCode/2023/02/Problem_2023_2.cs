using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2023_2 : Problem
    {
        public override int Year => 2023;
        public override int Number => 2;

        class Color
        {
            public const string R = "red";
            public const string G = "green";
            public const string B = "blue";
        }

        class Game
        {
            public int N { get; }
            public int MaxRed { get; private set; } = 0;
            public int MaxGreen { get; private set; } = 0;
            public int MaxBlue { get; private set; } = 0;

            public List<Set> Sets { get; } = new();
            public void AddSet(Set s)
            {
                Sets.Add(s);
                MaxRed = Math.Max(MaxRed, s.Red);
                MaxGreen = Math.Max(MaxGreen, s.Green);
                MaxBlue = Math.Max(MaxBlue, s.Blue);
            }

            public Game(int n) => N = n;
        }

        class Set
        {
            public int Power => Red * Green * Blue;

            public int Red { get; private set; } = 0;
            public int Green { get; private set; } = 0;
            public int Blue { get; private set; } = 0;

            public Set() { }

            public Set(Cube[] cubes)
            {
                foreach (Cube c in cubes)
                {
                    AddCube(c);
                }
            }

            public void AddCube(Cube c)
            {
                if (c.Color == Color.R) Red = c.N;
                if (c.Color == Color.G) Green = c.N;
                if (c.Color == Color.B) Blue = c.N;
            }
        }

        record Cube(int N, string Color);

        public override void Run()
        {
            //UseTestInput();
            Game[] games = new Game[Lines.Length];
            for (int i = 0; i < Lines.Length; i++)
            {
                string[] inputs = Lines[i].Split(':');
                Game g = new(int.Parse(inputs[0].Split(' ')[1]));
                inputs = inputs[1].Split(';');
                foreach (string set in inputs)
                {
                    Set s = new();
                    string[] cubes = set.Split(',');
                    foreach (string cube in cubes)
                    {
                        string[] values = cube.Trim().Split(' ');
                        int nb = int.Parse(values[0]);
                        string color = values[1];
                        s.AddCube(new Cube(nb, color));
                    }
                    g.AddSet(s);
                }
                games[i] = g;
            }

            int firstStar = games.Where(g => g.Sets.All(
                s => s.Red <= 12 &&
                s.Green <= 13 &&
                s.Blue <= 14)).Sum(g => g.N);

            int secondStar = games.Sum(g => new Set(
                new[] {
                    new Cube(g.MaxRed, Color.R),
                    new Cube(g.MaxGreen, Color.G),
                    new Cube(g.MaxBlue, Color.B)
                }).Power);

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar}");
        }
    }
}
