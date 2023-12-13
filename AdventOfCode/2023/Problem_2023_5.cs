using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2023_5 : Problem_2023
    {
        public override int Number => 5;

        public override void Run()
        {
            UseTestInput();
            long[] seeds = Lines[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();
            Console.WriteLine(string.Join(",", seeds));
        }
    }
}
