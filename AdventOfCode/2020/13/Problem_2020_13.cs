using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2020_13 : Problem
    {
        public override int Year => 2020;
        public override int Number => 13;

        public override void Run()
        {
            var timestamp = int.Parse(Lines[0]);
            var idsStr = Lines[1].Split(',');

            var bestBusId = 0;
            var minTimeToWait = int.MaxValue;

            foreach (var idStr in idsStr)
            {
                if (int.TryParse(idStr, out var result))
                {
                    var timeToWait = result - timestamp % result;

                    if (timeToWait < minTimeToWait)
                    {
                        bestBusId = result;
                        minTimeToWait = timeToWait;
                    }
                }
            }

            Console.WriteLine($"First star: {bestBusId * minTimeToWait}");

            var busIds = new List<Tuple<int, int>>();
            var offset = 0;

            foreach (var idStr in idsStr)
            {
                if (int.TryParse(idStr, out var result))
                {
                    busIds.Add(new Tuple<int, int>(offset, result));
                }
                offset++;
            }

            // Using the Chinese Remainder Theorem, see here (fr): https://fr.wikipedia.org/wiki/Th%C3%A9or%C3%A8me_des_restes_chinois

            long n = 1;
            foreach (var id in busIds)
            {
                n *= id.Item2;
            }

            var eList = new List<long>();

            foreach (var id in busIds)
            {
                long ni = id.Item2;
                long n_i = n / ni;
                for (var i = 1; i < ni; i++)
                {
                    if ((n_i * i) % ni == 1)
                    {
                        eList.Add(n_i * i);
                        break;
                    }
                }
            }

            long sol = 0;

            for (var i = 0; i < busIds.Count; i++)
            {
                sol += busIds[i].Item1 * eList[i];
            }

            var first_mod = sol % n;

            Console.WriteLine($"Second star: {n - first_mod}");
        }
    }
}
