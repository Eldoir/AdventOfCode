using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2021_6 : Problem_2021
    {
        public override int Number => 6;

        private List<int> lanterns;

        protected override void InitInternal()
        {
            lanterns = Lines[0].Split(',').Select(n => int.Parse(n)).ToList();

            base.InitInternal();
        }

        public override void Run()
        {
            for (int i = 0; i < 80; i++)
            {
                SimulateOneDay();
            }

            Console.WriteLine($"First star: {lanterns.Count}");
        }

        #region Methods

        private void SimulateOneDay()
        {
            int nbLanternsToSpawn = 0;

            for (int i = 0; i < lanterns.Count; i++)
            {
                lanterns[i]--;

                if (lanterns[i] < 0)
                {
                    lanterns[i] = 6;
                    nbLanternsToSpawn++;
                }
            }

            for (int i = 0; i < nbLanternsToSpawn; i++)
            {
                lanterns.Add(8);
            }
        }

        #endregion
    }
}
