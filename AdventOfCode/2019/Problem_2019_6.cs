using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2019_6 : Problem_2019
    {
        public override int Number => 6;

        Dictionary<string, List<string>> links;

        public override void Run()
        {
            links = new Dictionary<string, List<string>>();

            foreach (string line in Lines)
            {
                string[] inputs = line.Split(')');
                string parent = inputs[0];
                string child = inputs[1];

                if (!links.ContainsKey(parent))
                    links.Add(parent, new List<string>());
                links[parent].Add(child);
            }

            Console.WriteLine($"First star: {CountHeights()}");
            Console.WriteLine($"Second star: {FindOrbitalTransfersBetween("YOU", "SAN")}");
        }

        private int FindOrbitalTransfersBetween(string name1, string name2)
        {
            string parent = FindClosestParentOf(name1, name2);

            return GetHeight(name1, parent) + GetHeight(name2, parent) - 2;
        }

        private int CountHeights()
        {
            int sum = 0;

            foreach (var kvp in links)
            {
                sum += kvp.Value.Sum(l => GetHeight(l));
            }

            return sum;
        }

        private int GetHeight(string name, string stopAtName = "")
        {
            int sum = 0;

            while ((name = GetParentWithChildOfName(name)) != null)
            {
                sum++;

                if (name == stopAtName)
                    break;
            }

            return sum;
        }

        private string GetParentWithChildOfName(string name)
        {
            foreach (var kvp in links)
            {
                if (kvp.Value.Contains(name))
                    return kvp.Key;
            }

            return null;
        }

        private string FindClosestParentOf(string child1, string child2)
        {
            string[] parents = FindParentsOf(child1, child2);

            int maxHeight = int.MinValue;
            int maxIdx = -1;

            for (int i = 0; i < parents.Length; i++)
            {
                int height = GetHeight(parents[i]);

                if (height > maxHeight)
                {
                    maxHeight = height;
                    maxIdx = i;
                }
            }

            return parents[maxIdx];
        }

        private string[] FindParentsOf(string child1, string child2)
        {
            return links.Where(kvp => IsParentOf(kvp.Key, child1, child2)).Select(kvp => kvp.Key).ToArray();
        }

        private bool IsParentOf(string parent, string child1, string child2)
        {
            return IsParentOf(parent, child1) && IsParentOf(parent, child2);
        }

        private bool IsParentOf(string parent, string child)
        {
            if (!links.ContainsKey(parent) || links[parent].Count == 0)
                return false;

            if (links[parent].Contains(child))
                return true;

            return links[parent].FirstOrDefault(l => IsParentOf(l, child)) != null;
        }
    }
}
