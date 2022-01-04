using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2021_6 : Problem_2021
    {
        public override int Number => 6;

        private const int MaxLanternTimer = 8;

        private long[] lanterns;

        protected override void InitInternal()
        {
            lanterns = new long[MaxLanternTimer + 1];
            var nbs = Lines[0].Split(',').Select(n => int.Parse(n));

            foreach (int nb in nbs)
            {
                AddLantern(nb);
            }

            base.InitInternal();
        }

        public override void Run()
        {
            Console.WriteLine($"First star: {CountLanternsAfterDays(80)}");
            Console.WriteLine($"Second star: {CountLanternsAfterDays(176)}"); // simulate another 176 days, so 256 in total
        }

        #region Methods

        private void SpawnLanterns(long count)
        {
            AddLanterns(MaxLanternTimer, count);
        }

        private void AddLantern(int nb)
        {
            AddLanterns(nb, 1);
        }

        private void AddLanterns(int nb, long count)
        {
            lanterns[nb] += count;
        }

        private long CountLanternsAfterDays(int nb)
        {
            for (int i = 0; i < nb; i++)
            {
                SimulateOneDay();
            }

            return lanterns.Sum();
        }

        private void SimulateOneDay()
        {
            long nbLanternsToSpawn = lanterns[0];

            for (int i = 0; i < lanterns.Length - 1; i++)
            {
                lanterns[i] = lanterns[i + 1];
            }

            lanterns[^1] = 0;

            AddLanterns(6, nbLanternsToSpawn);
            SpawnLanterns(nbLanternsToSpawn);
        }

        #endregion
    }
}
