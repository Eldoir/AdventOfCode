using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2015_16 : Problem
    {
        public override int Year => 2015;
        public override int Number => 16;

        public override void Run()
        {
            Sue[] sues = new Sue[Lines.Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                string[] inputs = Lines[i].Split(' ');

                int nb = int.Parse(inputs[1].Split(':')[0]);

                sues[i] = new Sue { nb = nb };

                for (int j = 2; j < inputs.Length; j += 2)
                {
                    string character = inputs[j].Split(':')[0];
                    int n = int.Parse(inputs[j + 1].Split(',')[0]);

                    switch (character)
                    {
                        case "children": sues[i].children = n; break;
                        case "cats": sues[i].cats = n; break;
                        case "samoyeds": sues[i].samoyeds = n; break;
                        case "pomeranians": sues[i].pomeranians = n; break;
                        case "akitas": sues[i].akitas = n; break;
                        case "vizslas": sues[i].vizslas = n; break;
                        case "goldfish": sues[i].goldfish = n; break;
                        case "trees": sues[i].trees = n; break;
                        case "cars": sues[i].cars = n; break;
                        case "perfumes": sues[i].perfumes = n; break;
                        default: Console.WriteLine("ERROR"); break;
                    }
                }
            }

            Sue sueToFind = new Sue
            {
                children = 3,
                cats = 7,
                samoyeds = 2,
                pomeranians = 3,
                akitas = 0,
                vizslas = 0,
                goldfish = 5,
                trees = 3,
                cars = 2,
                perfumes = 1
            };

            Console.WriteLine($"First star: {sues.Where(s => s.CouldFitFirstStar(sueToFind)).ToArray()[0].nb}");
            Console.WriteLine($"Second star: {sues.Where(s => s.CouldFitSecondStar(sueToFind)).ToArray()[0].nb}");
        }

        class Sue
        {
            public int nb = -1;
            public int children = -1;
            public int cats = -1;
            public int samoyeds = -1;
            public int pomeranians = -1;
            public int akitas = -1;
            public int vizslas = -1;
            public int goldfish = -1;
            public int trees = -1;
            public int cars = -1;
            public int perfumes = -1;


            public bool CouldFitFirstStar(Sue other)
            {
                if (children != -1 && children != other.children) return false;
                if (cats != -1 && cats != other.cats) return false;
                if (samoyeds != -1 && samoyeds != other.samoyeds) return false;
                if (pomeranians != -1 && pomeranians != other.pomeranians) return false;
                if (akitas != -1 && akitas != other.akitas) return false;
                if (vizslas != -1 && vizslas != other.vizslas) return false;
                if (goldfish != -1 && goldfish != other.goldfish) return false;
                if (trees != -1 && trees != other.trees) return false;
                if (cars != -1 && cars != other.cars) return false;
                if (perfumes != -1 && perfumes != other.perfumes) return false;

                return true;
            }

            public bool CouldFitSecondStar(Sue other)
            {
                if (children != -1 && children != other.children) return false;
                if (cats != -1 && cats <= other.cats) return false;
                if (samoyeds != -1 && samoyeds != other.samoyeds) return false;
                if (pomeranians != -1 && pomeranians >= other.pomeranians) return false;
                if (akitas != -1 && akitas != other.akitas) return false;
                if (vizslas != -1 && vizslas != other.vizslas) return false;
                if (goldfish != -1 && goldfish >= other.goldfish) return false;
                if (trees != -1 && trees <= other.trees) return false;
                if (cars != -1 && cars != other.cars) return false;
                if (perfumes != -1 && perfumes != other.perfumes) return false;

                return true;
            }
        }
    }
}
