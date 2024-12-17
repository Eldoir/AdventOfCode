using AdventOfCode.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2020_16 : Problem
    {
        public override int Year => 2020;
        public override int Number => 16;

        public override void Run()
        {
            int lineIndex = 0;

            var fields = new List<Field>();

            while (!string.IsNullOrEmpty(Lines[lineIndex]))
            {
                var line = Lines[lineIndex];

                if (!string.IsNullOrEmpty(line))
                {
                    var pattern = @"(.*): (\d+)-(\d+) or (\d+)-(\d+)";
                    var groups = Regex.Match(line, pattern).Groups;
                    var fieldName = groups[1].Value;
                    var interval1 = new IntervalInt(int.Parse(groups[2].Value), int.Parse(groups[3].Value));
                    var interval2 = new IntervalInt(int.Parse(groups[4].Value), int.Parse(groups[5].Value));

                    fields.Add(new Field(fieldName, interval1, interval2));
                }

                lineIndex++;
            }

            lineIndex += 2; // skip empty line and "your ticket:" line
            var myTicketNbs = Lines[lineIndex].Split(',').Select(l => int.Parse(l)).ToArray();

            lineIndex += 3; // skip empty line and "nearby tickets:" line
            var nearbyTicketsNbs = new List<int[]>();

            for (var i = lineIndex; i < Lines.Length; i++)
            {
                nearbyTicketsNbs.Add(Lines[i].Split(',').Select(l => int.Parse(l)).ToArray());
            }

            var sumInvalidValues = 0;
            var invalidTicketNbsIdxs = new List<int>();

            for (var i = 0; i < nearbyTicketsNbs.Count; i++)
            {
                foreach (var nb in nearbyTicketsNbs[i])
                {
                    var validForAnyField = false;

                    foreach (var field in fields)
                    {
                        if (field.IsValidTicketNb(nb))
                        {
                            validForAnyField = true;
                            break;
                        }
                    }

                    if (!validForAnyField)
                    {
                        sumInvalidValues += nb;
                        invalidTicketNbsIdxs.Add(i);
                        break;
                    }
                }
            }

            Console.WriteLine($"First star: {sumInvalidValues}");

            // Remove all invalid tickets from list
            for (var i = 0; i < invalidTicketNbsIdxs.Count; i++)
            {
                nearbyTicketsNbs.RemoveAt(invalidTicketNbsIdxs[i] - i);
            }

            var fieldsValidIdxs = new List<List<int>>();

            foreach (var field in fields)
            {
                var validIdxs = new List<int>();

                for (var idx = 0; idx < fields.Count; idx++)
                {
                    var validForAllTicketsAtIdx = true;

                    foreach (var nearbyTicketNbs in nearbyTicketsNbs)
                    {
                        if (!field.IsValidTicketNb(nearbyTicketNbs[idx]))
                        {
                            validForAllTicketsAtIdx = false;
                            break;
                        }
                    }

                    if (validForAllTicketsAtIdx)
                    {
                        validIdxs.Add(idx);
                    }
                }

                if (validIdxs.Count > 0)
                {
                    fieldsValidIdxs.Add(validIdxs);
                }
                else
                {
                    Console.WriteLine($"Error: couldn't find matching values for \"{field.name}\".");
                }
            }

            var fieldsProcessed = 0;

            while (fieldsProcessed < fields.Count)
            {
                var fieldValidIdx = -1;

                for (var i = 0; i < fieldsValidIdxs.Count; i++)
                {
                    if (fieldsValidIdxs[i].Count == 1) // we are the only field with only one valid idx
                    {
                        fieldValidIdx = fieldsValidIdxs[i][0];
                        fields[i].SetIdx(fieldsValidIdxs[i][0]);
                        break;
                    }
                }

                // Now, remove that idx from all the lists
                foreach (var fieldValidIdxs in fieldsValidIdxs)
                {
                    fieldValidIdxs.Remove(fieldValidIdx);
                }

                fieldsProcessed++;
            }

            long mul = 1;

            foreach (var field in fields)
            {
                if (field.name.StartsWith("departure"))
                {
                    mul *= myTicketNbs[field.idx];
                }
            }

            Console.WriteLine($"Second star: {mul}");
        }

        class Field
        {
            public string name { get; }
            public int[] allowedNumbers { get; }
            public int idx { get; private set; }

            public Field(string name, IntervalInt interval1, IntervalInt interval2)
            {
                this.name = name;

                allowedNumbers = new int[interval1.max - interval1.min + 1 + interval2.max - interval2.min + 1];
                var idx = 0;

                for (var i = interval1.min; i <= interval1.max; i++)
                {
                    allowedNumbers[idx] = i;
                    idx++;
                }

                for (var i = interval2.min; i <= interval2.max; i++)
                {
                    allowedNumbers[idx] = i;
                    idx++;
                }

                this.idx = -1;
            }

            public bool IsValidTicketNb(int ticketNb)
            {
                return allowedNumbers.Contains(ticketNb);
            }

            public void SetIdx(int idx)
            {
                this.idx = idx;
            }
        }
    }
}
