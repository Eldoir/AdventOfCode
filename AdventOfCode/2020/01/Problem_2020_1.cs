using System;

namespace AdventOfCode
{
    class Problem_2020_1 : Problem
    {
        public override int Year => 2020;
        public override int Number => 1;

        private int[] entries;

        protected override void InitInternal()
        {
            entries = new int[Lines.Length];

            for (var i = 0; i < Lines.Length; i++)
            {
                entries[i] = int.Parse(Lines[i]);
            }

            base.InitInternal();
        }

        public override void Run()
        {
            for (var i = 0; i < entries.Length - 1; i++)
            {
                for (var j = i + 1; j < entries.Length; j++)
                {
                    if (entries[i] + entries[j] == 2020)
                    {
                        Console.WriteLine($"First star: {entries[i] * entries[j]}");
                        break;
                    }
                }
            }

            for (var i = 0; i < entries.Length - 2; i++)
            {
                for (var j = i + 1; j < entries.Length - 1; j++)
                {
                    for (var k = j + 1; k < entries.Length; k++)
                    {
                        if (entries[i] + entries[j] + entries[k] == 2020)
                        {
                            Console.WriteLine($"Second star: {entries[i] * entries[j] * entries[k]}");
                            break;
                        }
                    }
                }
            }
        }
    }
}
