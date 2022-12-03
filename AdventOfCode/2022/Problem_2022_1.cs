using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2022_1 : Problem_2022
    {
        public override int Number => 1;

        public override void Run()
        {
            var elves = new List<Elve>();
            var elve = new Elve();

            foreach (string line in Lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    elves.Add(elve);
                    elve = new Elve();
                }
                else
                {
                    elve.AddSnack(int.Parse(line));
                }
            }

            elves.Add(elve);

            IOrderedEnumerable<Elve> orderedElves = elves.OrderByDescending(e => e.SnacksTotal);

            Console.WriteLine($"First star: {orderedElves.First().SnacksTotal}");
            Console.WriteLine($"Second star: {orderedElves.Take(3).Sum(e => e.SnacksTotal)}");
        }

        class Elve
        {
            public List<int> Snacks { get; } = new List<int>();
            public int SnacksTotal => Snacks.Sum();

            public void AddSnack(int snack)
            {
                Snacks.Add(snack);
            }
        }
    }
}
