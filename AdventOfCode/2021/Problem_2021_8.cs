using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2021_8 : Problem_2021
    {
        public override int Number => 8;

        List<Entry> entries;

        protected override void InitInternal()
        {
            entries = new List<Entry>();

            foreach (string line in Lines)
            {
                entries.Add(new Entry(line));
            }

            base.InitInternal();
        }

        public override void Run()
        {
            int sum = 0;
            var uniqueNbs = new[] { 2, 3, 4, 7 };

            foreach (Entry entry in entries)
            {
                sum += entry.OutputValues.Where(value=> uniqueNbs.Contains(value.Length)).Count();
            }

            Console.WriteLine($"First star: {sum}");
        }

        class Entry
        {
            public string[] SignalPatterns { get; }
            public string[] OutputValues { get; }

            static Regex regex = new Regex("[a-g]{2,}");

            public Entry(string line)
            {
                SignalPatterns = new string[10];
                OutputValues = new string[4];

                MatchCollection matches = regex.Matches(line);

                for (int i = 0; i < SignalPatterns.Length; i++)
                {
                    SignalPatterns[i] = matches[i].Value;
                }

                for (int i = 0; i < OutputValues.Length; i++)
                {
                    OutputValues[i] = matches[SignalPatterns.Length + i].Value;
                }
            }
        }
    }
}
