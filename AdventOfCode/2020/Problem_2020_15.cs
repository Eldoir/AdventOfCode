using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2020_15 : Problem_2020
    {
        public override int Number => 15;

        public override void Run()
        {
            var nbs = Lines[0].Split(',').Select(l => (int.Parse(l))).ToArray();

            Console.WriteLine($"First star: {Process(nbs, 2020)}");
            Console.WriteLine($"Second star: {Process(nbs, 30000000)}");
        }

        int Process(int[] nbs, int limit)
        {
            var dic = new Dictionary<int, Tuple<int, int>>(); // idx : number, tuple : (last seen, current). last seen = -1 means it is new and hasn't been seen yet.

            for (var i = 0; i < nbs.Length; i++)
            {
                dic.Add(nbs[i], new Tuple<int, int>(i < nbs.Length - 1 ? i + 1 : -1, i + 1));
                //Console.WriteLine($"{nbs[i]} {dic[nbs[i]]}");
            }

            var lastNbSpoken = nbs[nbs.Length - 1];

            for (var i = nbs.Length + 1; i <= limit; i++)
            {
                var nbSpoken = -1;

                if (dic[lastNbSpoken].Item1 == -1) // number was just added and hasn't been seen before
                {
                    nbSpoken = 0;
                }
                else
                {
                    nbSpoken = dic[lastNbSpoken].Item2 - dic[lastNbSpoken].Item1;
                }

                if (dic.ContainsKey(nbSpoken))
                {
                    dic[nbSpoken] = new Tuple<int, int>(dic[nbSpoken].Item2, i);
                }
                else
                {
                    dic.Add(nbSpoken, new Tuple<int, int>(-1, i));
                }

                lastNbSpoken = nbSpoken;

                //Console.WriteLine($"{lastNbSpoken} {dic[lastNbSpoken]}");
            }

            return lastNbSpoken;
        }
    }
}
