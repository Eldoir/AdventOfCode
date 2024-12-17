using AdventOfCode.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2015_19 : Problem
    {
        public override int Year => 2015;
        public override int Number => 19;

        Dictionary<string, string> reverseReplacements;

        public override void Run()
        {
            string molecule = Lines[Lines.Length - 1];

            Dictionary<string, List<string>> replacements = new Dictionary<string, List<string>>();
            reverseReplacements = new Dictionary<string, string>();

            for (int i = 0; i < Lines.Length - 2; i++)
            {
                string[] inputs = Lines[i].RemoveWhitespace().Split("=>");

                if (!replacements.ContainsKey(inputs[0]))
                    replacements.Add(inputs[0], new List<string>());

                replacements[inputs[0]].Add(inputs[1]);

                if (!reverseReplacements.ContainsKey(inputs[1]))
                    reverseReplacements.Add(inputs[1], inputs[0]);
                else
                    Console.WriteLine("Error");
            }

            HashSet<string> molecules = new HashSet<string>();

            foreach (var kvp in replacements)
            {
                int[] idxs = FindIndexesOf(molecule, kvp.Key);

                foreach (string replacement in kvp.Value)
                {
                    foreach (int idx in idxs)
                    {
                        string newMolecule = molecule.Replace(replacement, idx, kvp.Key.Length);

                        if (!molecules.Contains(newMolecule))
                        {
                            //Console.WriteLine(newMolecule);
                            molecules.Add(newMolecule);
                        }
                    }
                }
            }

            // Sort keys by length, descending
            string[] keys = reverseReplacements.Keys.ToArray();
            keys = keys.OrderByDescending(k => k.Length).ToArray();

            string originalMolecule = molecule;

            int count = 0;
            while (molecule != "e")
            {
                string newMolecule = Replace(molecule, keys);

                if (molecule == newMolecule) // On est bloqué, on doit choisir un nouveau mélange de clés
                {
                    keys = keys.OrderBy(k => Guid.NewGuid()).ToArray();
                    molecule = originalMolecule;
                    count = 0;
                }
                else
                {
                    molecule = newMolecule;
                    count++;
                }
            }

            Console.WriteLine($"First star: {molecules.Count}");
            Console.WriteLine($"Second star: {count}");
        }

        private string Replace(string molecule, string[] keys)
        {
            foreach (string key in keys)
            {
                int idx = molecule.IndexOf(key);
                if (idx != -1)
                {
                    string newMolecule = molecule.Replace(reverseReplacements[key], idx, key.Length);
                    return newMolecule;
                }
            }

            //Console.WriteLine("No replacement found");
            return molecule;
        }

        private int[] FindIndexesOf(string src, string search)
        {
            List<int> indexes = new List<int>();

            int idx = -1;
            int nbRemoved = 0;

            do
            {
                idx = src.IndexOf(search);
                if (idx != -1)
                {
                    indexes.Add(nbRemoved + idx);
                    src = src.Remove(idx, search.Length);
                    nbRemoved += search.Length;
                }
            }
            while (idx != -1);

            return indexes.ToArray();
        }
    }
}
