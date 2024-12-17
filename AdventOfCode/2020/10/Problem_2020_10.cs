using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2020_10 : Problem
    {
        public override int Year => 2020;
        public override int Number => 10;

        public override void Run()
        {
            var linesNb = Lines.Select(l => long.Parse(l)).OrderBy(l => l).ToArray();

            var joltDelta1Count = 0;
            var joltDelta3Count = 0;

            for (var i = 1; i < linesNb.Length; i++)
            {
                var delta = linesNb[i] - linesNb[i - 1];

                if (delta == 1) joltDelta1Count++;
                else if (delta == 3) joltDelta3Count++;
                else Console.WriteLine("wtf");
            }

            if (linesNb[0] == 1) joltDelta1Count++;
            else if (linesNb[0] == 3) joltDelta3Count++;
            else Console.WriteLine("wtf");

            joltDelta3Count++; // from last adapter to device

            Console.WriteLine($"First star: {joltDelta1Count * joltDelta3Count}");

            // Now, starting from the last adapter to the first one, we will sum up the possible combinations.
            // Let's take an example with the sequence: (0), 1, 4, 5, 6, 7, 10, 11, 12, 15, 16, 19, (22).
            // 19 is the last element of the array so it leads to 22 in only one way: thus, combs[indexof(19)] = 1.
            // 16 has only one element after it which is at a distance <= 3, and it's 19, so combs[indexof(16)] = combs[indexof(19)] = 1.
            // 15 is the same as 16, and it goes the same for 12 and 11.
            // 10 can go to 11 or 12, so combs[indexof(10)] = combs[indexof(11)] + combs[indexof(12)] = 1 + 1 = 2.
            // 7 can only go to 10 so combs[indexof(7)] = combs[indexof(10)] = 2. Same goes for 6.
            // 5 can go to 6 or 7 so combs[indexof(5)] = combs[indexof(6)] + combs[indexof(7)] = 2 + 2 = 4.
            // 4 can go to 5, 6 or 7 so combs[indexof(4)] = combs[indexof(5)] + combs[indexof(6)] + combs[indexof(7)] = 4 + 2 + 2 = 8.
            // 1 can only go to 4 so combs[indexof(1)] = combs[indexof(4)] = 8.
            // 0 can only go to 1 so combs[indexof(0)] = combs[indexof(1)] = 8.
            // That's it! The entire number of combinations from 0 to 22 is 8 in this example.

            long[] combs = new long[linesNb.Length];
            combs[combs.Length - 1] = 1; // last adapter necessarily go straight to device = 1 possible combination, as we saw above

            for (var i = linesNb.Length - 2; i >= 0; i--)
            {
                long combNb = 0;
                if (linesNb[i + 1] - linesNb[i] <= 3) combNb += combs[i + 1];
                if (i < Lines.Length - 2 && linesNb[i + 2] - linesNb[i] <= 3) combNb += combs[i + 2];
                if (i < Lines.Length - 3 && linesNb[i + 3] - linesNb[i] <= 3) combNb += combs[i + 3];
                combs[i] = combNb;
            }

            long combsFromZero = combs[0];
            if (linesNb[1] <= 3) combsFromZero += combs[1];
            if (linesNb[2] <= 3) combsFromZero += combs[2];

            Console.WriteLine($"Second star: {combsFromZero}");
        }
    }
}
