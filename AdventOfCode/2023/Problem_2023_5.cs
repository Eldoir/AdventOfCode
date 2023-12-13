using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2023_5 : Problem_2023
    {
        public override int Number => 5;

        class Line
        {
            public Line(string str)
            {
                long[] longs = str.Split(' ').Select(long.Parse).ToArray();
                _destinationRangeStart = longs[0];
                _sourceRangeStart = longs[1];
                _rangeLength = longs[2];
            }

            public long Apply(long n)
            {
                long delta = n - _sourceRangeStart;
                if (delta >= 0 && delta < _rangeLength)
                    return _destinationRangeStart + delta;

                return n;
            }

            private readonly long _destinationRangeStart;
            private readonly long _sourceRangeStart;
            private readonly long _rangeLength;
        }

        class Map
        {
            public List<Line> Lines { get; } = new();

            public void AddLine(Line line)
            {
                Lines.Add(line);
            }

            public long Apply(long n)
            {
                foreach (Line line in Lines)
                {
                    long newN = line.Apply(n);
                    if (newN != n)
                        return newN; // there is only one match for one number
                }
                return n;
            }
        }

        public override void Run()
        {
            UseTestInput();
            long[] seeds = Lines[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();
            List<Map> maps = new();
            Map currentMap = null;

            for (int i = 2; i < Lines.Length; i++)
            {
                string line = Lines[i];

                if (line.Contains("map"))
                {
                    currentMap = new();
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    currentMap.AddLine(new Line(line));
                }
                else
                {
                    maps.Add(currentMap);
                }
            }

            maps.Add(currentMap);

            long minLocation = long.MaxValue;

            foreach (long seed in seeds)
            {
                long value = seed;
                foreach (Map map in maps)
                {
                    value = map.Apply(value);
                }
                minLocation = Math.Min(value, minLocation);
            }

            Console.WriteLine($"First star: {minLocation}");

            minLocation = long.MaxValue;

            for (int i = 0; i < seeds.Length; i += 2)
            {
                for (int j = 0; j < seeds[i + 1]; j++)
                {
                    long value = seeds[i] + j;
                    foreach (Map map in maps)
                    {
                        value = map.Apply(value);
                    }
                    minLocation = Math.Min(value, minLocation);
                }
            }

            Console.WriteLine($"Second star: {minLocation}");
        }
    }
}
