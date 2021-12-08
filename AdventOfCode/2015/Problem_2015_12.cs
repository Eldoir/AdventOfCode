using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2015_12 : Problem_2015
    {
        public override int Number => 12;

        public override void Run()
        {
            HashSet<string> redPaths = new HashSet<string>();
            Dictionary<string, int> values = new Dictionary<string, int>();

            JArray arr = JArray.Parse(Text);

            Traverse(arr.Root, (leaf) =>
            {
                if (leaf.ToString() == "red" && leaf.Parent.Type != JTokenType.Array)
                {
                    if (!redPaths.Contains(leaf.Parent.Parent.Path))
                        redPaths.Add(leaf.Parent.Parent.Path);
                }
                try
                {
                    int value = int.Parse(leaf.ToString());

                    values.Add(leaf.Path, value);
                }
                catch (FormatException) { }
            });

            int totalFirstStar = 0;
            int totalSecondStar = 0;

            foreach (var kvp in values)
            {
                totalFirstStar += kvp.Value;

                bool found = false;

                foreach (string redPath in redPaths)
                {
                    if (kvp.Key.StartsWith(redPath))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    totalSecondStar += kvp.Value;
            }

            Console.WriteLine($"First star: {totalFirstStar}");
            Console.WriteLine($"Second star: {totalSecondStar}");
        }

        void Traverse(JToken root, Action<JToken> doOnLeaf = null)
        {
            if (root.HasValues)
            {
                foreach (JToken token in root)
                {
                    Traverse(token, doOnLeaf);
                }
            }
            else
            {
                if (doOnLeaf != null)
                    doOnLeaf(root);
            }
        }
    }
}
