using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2015_14 : Problem_2015
    {
        public override int Number => 14;

        public override void Run()
        {
            List<Reindeer> reindeers = new List<Reindeer>();

            foreach (string line in Lines)
            {
                string[] inputs = line.Split(' ');

                string name = inputs[0];
                int speed = int.Parse(inputs[3]);
                int duration = int.Parse(inputs[6]);
                int rest = int.Parse(inputs[13]);

                reindeers.Add(new Reindeer
                {
                    name = name,
                    speed = speed,
                    duration = duration,
                    rest = rest
                });
            }

            for (int i = 1; i <= 2503; i++)
            {
                int maxDistance = GetMaxDistance(reindeers, i);

                foreach (Reindeer reindeer in reindeers)
                {
                    if (reindeer.Evaluate(i) == maxDistance)
                    {
                        reindeer.points++;
                    }
                }
            }

            reindeers = reindeers.OrderByDescending(r => r.points).ToList();

            Console.WriteLine($"First star: {GetMaxDistance(reindeers, 2503)}");
            Console.WriteLine($"Second star: {reindeers[0].points}");
        }

        int GetMaxDistance(List<Reindeer> reindeers, int seconds)
        {
            int maxDistance = int.MinValue;

            foreach (Reindeer reindeer in reindeers)
            {
                int distance = reindeer.Evaluate(seconds);

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                }
            }

            return maxDistance;
        }

        class Reindeer
        {
            public string name;
            public int speed;
            public int duration;
            public int rest;
            public int points = 0;

            public int Evaluate(int seconds)
            {
                bool flying = true; // if false, he's resting
                int i = 0;
                int flyFrame = 0;
                int restFrame = 0;
                int total = 0;

                while (i < seconds)
                {
                    if (flying)
                    {
                        flyFrame++;
                        total += speed;

                        if (flyFrame == duration)
                        {
                            flying = false;
                            flyFrame = 0;
                        }
                    }
                    else // resting
                    {
                        restFrame++;

                        if (restFrame == rest)
                        {
                            flying = true;
                            restFrame = 0;
                        }
                    }

                    i++;
                }

                return total;
            }
        }
    }
}
