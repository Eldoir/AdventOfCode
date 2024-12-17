using System;

namespace AdventOfCode
{
    class Problem_2019_1 : Problem
    {
        public override int Year => 2019;
        public override int Number => 1;

        public override void Run()
        {
            int firstStarTotal = 0;
            int secondStarTotal = 0;

            foreach (string line in Lines)
            {
                int mass = int.Parse(line);

                firstStarTotal += (int)(mass / 3) - 2;
                secondStarTotal += CalculateNeededFuel(mass);
            }

            Console.WriteLine($"First star: {firstStarTotal}");
            Console.WriteLine($"Second star: {secondStarTotal}");
        }

        int CalculateNeededFuel(int mass)
        {
            int result = (int)(mass / 3) - 2;

            if (result <= 0)
                return 0;

            return result + CalculateNeededFuel(result);
        }
    }
}
