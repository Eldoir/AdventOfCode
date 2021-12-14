using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2021_1 : Problem_2021
    {
        public override int Number => 1;

        private List<int> numbers;

        protected override void InitInternal()
        {
            numbers = new List<int>();

            foreach (string line in Lines)
            {
                numbers.Add(int.Parse(line));
            }

            base.InitInternal();
        }

        public override void Run()
        {
            int increased = 0;

            for (int i = 1; i < numbers.Count; i++)
            {
                if (numbers[i] > numbers[i - 1])
                    increased++;
            }

            Console.WriteLine($"First star: {increased}");

            List<int> windows = new List<int>();

            for (int i = 2; i < numbers.Count; i++)
            {
                windows.Add(numbers[i] + numbers[i - 1] + numbers[i - 2]);
            }

            increased = 0;

            for (int i = 1; i < windows.Count; i++)
            {
                if (windows[i] > windows[i - 1])
                    increased++;
            }

            Console.WriteLine($"Second star: {increased}");
        }
    }
}
