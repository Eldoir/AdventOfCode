using AdventOfCode.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_1 : Problem_2024
    {
        public override int Number => 1;

        public override void Run()
        {
            int[] list1 = new int[Lines.Length];
            int[] list2 = new int[Lines.Length];
            Dictionary<int, int> dico2 = new();
            for (int i = 0; i < Lines.Length; i++)
            {
                string[] line = Lines[i].Split(' ');
                list1[i] = int.Parse(line.First());
                int value2 = int.Parse(line.Last());
                list2[i] = value2;
                if (!dico2.ContainsKey(value2))
                    dico2.Add(value2, 0);
                dico2[value2]++;
            }
            list1 = list1.Sort();
            list2 = list2.Sort();
            Console.WriteLine($"First star: " + list1.Select((a, i) => Math.Abs(a - list2[i])).Sum());
            Console.WriteLine($"Second star: " + list1.Select((a, i) => a * (dico2.ContainsKey(a) ? dico2[a] : 0)).Sum());
        }
    }
}
