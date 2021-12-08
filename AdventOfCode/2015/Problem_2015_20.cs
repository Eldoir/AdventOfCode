using System;
using System.Linq;
using AdventOfCode.Extensions;

namespace AdventOfCode
{
    class Problem_2015_20 : Problem_2015
    {
        public override int Number => 20;

        public override void Run()
        {
            int n = int.Parse(Text);

            int house = 1;

            while (GetPresentsFirstStar(house) < n)
            {
                house++;
            }

            Console.WriteLine($"First star: {house}");

            house = 1;

            while (GetPresentsSecondStar(house) < n)
            {
                house++;
            }

            Console.WriteLine($"Second star: {house}");
        }

        private int GetPresentsFirstStar(int n)
        {
            return n.SelectDivisors().Sum() * 10;
        }

        private int GetPresentsSecondStar(int n)
        {
            return n.SelectDivisors().Where(d => d * 50 >= n).Sum() * 11;
        }
    }
}
