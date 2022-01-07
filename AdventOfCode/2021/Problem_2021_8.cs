using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Extensions;

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
            var uniqueNbs = new[] { 2, 3, 4, 7 };
            int sumFirstStar = entries.Sum(entry => entry.OutputValues.Where(value => uniqueNbs.Contains(value.Length)).Count());
            int sumSecondStar = entries.Sum(entry => entry.GetOutputValue());

            Console.WriteLine($"First star: {sumFirstStar}");
            Console.WriteLine($"Second star: {sumSecondStar}");
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

            public int GetOutputValue()
            {
                string[] signals = new string[10];

                signals[1] = SignalPatterns.Single(p => p.Length == 2);
                signals[4] = SignalPatterns.Single(p => p.Length == 4);
                signals[7] = SignalPatterns.Single(p => p.Length == 3);
                signals[8] = SignalPatterns.Single(p => p.Length == 7);

                List<string> signalsWithLength6 = SignalPatterns.Where(p => p.Length == 6).ToList();
                signals[9] = signalsWithLength6.Single(s => s.ContainsAllLettersOf(signals[4]));
                signalsWithLength6.Remove(signals[9]);
                signals[0] = signalsWithLength6.Single(s => s.ContainsAllLettersOf(signals[7]));
                signalsWithLength6.Remove(signals[0]);
                signals[6] = signalsWithLength6[0]; // only one left

                List<string> signalsWithLength5 = SignalPatterns.Where(p => p.Length == 5).ToList();
                signals[2] = signalsWithLength5.Single(s => signals[4].Minus(s).Length == 2);
                signalsWithLength5.Remove(signals[2]);
                signals[5] = signalsWithLength5.Single(s => signals[6].ContainsAllLettersOf(s));
                signalsWithLength5.Remove(signals[5]);
                signals[3] = signalsWithLength5[0]; // only one left

                string result = "";

                for (int i = 0; i < OutputValues.Length; i++)
                {
                    for (int j = 0; j < signals.Length; j++)
                    {
                        if (signals[j].IsEqualToShuffled(OutputValues[i]))
                        {
                            result += j.ToString();
                            break;
                        }
                    }
                }

                return int.Parse(result);
            }
        }
    }
}
