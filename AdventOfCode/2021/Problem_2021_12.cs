using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2021_12 : Problem_2021
    {
        public override int Number => 12;

        Dictionary<string, Cave> caves;
        Cave Start => caves["start"];
        Cave End => caves["end"];

        protected override void InitInternal()
        {
            UseTestInput();

            caves = new Dictionary<string, Cave>();
            foreach (string line in Lines)
            {
                string[] inputs = line.Split('-');
                string from = inputs[0];
                string to = inputs[1];

                if (!caves.ContainsKey(from))
                    caves[from] = new Cave(from);
                if (!caves.ContainsKey(to))
                    caves[to] = new Cave(to);

                caves[from].Link(caves[to]);
                caves[to].Link(caves[from]);
            }

            base.InitInternal();
        }

        public override void Run()
        {
            Console.WriteLine($"First star: 0");
        }

        class Cave
        {
            public string Name { get; }
            public List<Cave> Links { get; }
            public bool IsSmall => Name.ToLower() == Name;
            public bool IsBig => !IsSmall;

            public Cave(string name)
            {
                Name = name;
                Links = new List<Cave>();
            }

            public void Link(Cave other)
            {
                Links.Add(other);
            }

            public override string ToString() => Name;
        }
    }
}
