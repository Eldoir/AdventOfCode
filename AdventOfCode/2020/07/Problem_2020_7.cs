using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2020_7 : Problem
    {
        public override int Year => 2020;
        public override int Number => 7;

        private Dictionary<string, List<Tuple<int, string>>> dic;

        public override void Run()
        {
            var pattern = @"(\d*) ?(\w+ \w+) bags?";

            dic = new Dictionary<string, List<Tuple<int, string>>>();

            foreach (var line in Lines)
            {
                var matches = Regex.Matches(line, pattern);

                if (matches.Count > 1)
                {
                    var mainBag = matches[0].Groups[2].Value;

                    dic.Add(mainBag, new List<Tuple<int, string>>());

                    for (var i = 1; i < matches.Count; i++)
                    {
                        if (!matches[i].Groups[2].Value.Equals("no other"))
                        {
                            var quantity = int.Parse(matches[i].Groups[1].Value);
                            var containedBag = matches[i].Groups[2].Value;
                            var tuple = new Tuple<int, string>(quantity, containedBag);
                            dic[mainBag].Add(tuple);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Error: no matches on line: {line}");
                }
            }

            var bagsContainingShinyGold = dic.Where(kvp => ContainsShinyGoldBag(kvp.Key)).Count();
            var shinyGoldTotalBags = GetTotalBags("shiny gold") - 1; // - 1 because we don't want to include the shiny gold bag itself

            Console.WriteLine($"First star: {bagsContainingShinyGold}");
            Console.WriteLine($"Second star: {shinyGoldTotalBags}");
        }

        #region Methods

        bool ContainsShinyGoldBag(string bag)
        {
            if (dic[bag].Count == 0)
                return false;

            foreach (var tuple in dic[bag])
            {
                if (tuple.Item2.Equals("shiny gold"))
                    return true;
                else if (ContainsShinyGoldBag(tuple.Item2))
                    return true;
            }

            return false;
        }

        int GetTotalBags(string bag)
        {
            if (dic[bag].Count == 0)
                return 1;

            var total = 1;

            foreach (var tuple in dic[bag])
            {
                total += tuple.Item1 * GetTotalBags(tuple.Item2);
            }

            return total;
        }

        #endregion
    }
}
