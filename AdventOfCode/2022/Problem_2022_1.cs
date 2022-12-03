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

            for (int i = 0; i < Lines.Length; i++)
            {
                if (string.IsNullOrEmpty(Lines[i]))
                {
                    elves.Add(elve);
                    elve = new Elve();
                }
                else
                {
                    elve.AddSnack(int.Parse(Lines[i]));
                }
            }

            elves.Add(elve);

            IOrderedEnumerable<Elve> orderedElves = elves.OrderByDescending(e => e.SnacksTotal);

            Console.WriteLine($"First star: {orderedElves.First().SnacksTotal}");
            Console.WriteLine($"Second star: {orderedElves.Take(3).Sum(e => e.SnacksTotal)}");
        }

        class Elve
        {
            public int Id { get; }
            public List<int> Snacks { get; }
            public int SnacksTotal => Snacks.Sum();

            private static int NbElves = 0;

            public Elve()
            {
                Id = NbElves++; ;
                Snacks = new List<int>();
            }

            public void AddSnack(int snack)
            {
                Snacks.Add(snack);
            }
        }
    }
}
