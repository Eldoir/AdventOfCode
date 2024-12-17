using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2015_9 : Problem
    {
        public override int Year => 2015;
        public override int Number => 9;

        Dictionary<string, int> distances;
        List<string> names;

        public override void Run()
        {
            distances = new Dictionary<string, int>();
            names = new List<string>();

            foreach (string line in Lines)
            {
                string[] inputs = line.Split(' ');
                string from = inputs[0];
                string to = inputs[2];
                int distance = int.Parse(inputs[4]);

                distances.Add($"{from}_{to}", distance);
                distances.Add($"{to}_{from}", distance);

                if (!names.Contains(from))
                    names.Add(from);
                if (!names.Contains(to))
                    names.Add(to);
            }

            string indexes = "";
            for (int i = 0; i < names.Count; i++)
                indexes += i;

            int minDistance = int.MaxValue;
            int maxDistance = int.MinValue;

            Permute(indexes, (combination) =>
            {
                int distance = GetDistance(combination);

                if (distance < minDistance)
                    minDistance = distance;
                if (distance > maxDistance)
                    maxDistance = distance;
            });

            Console.WriteLine($"First star: {minDistance}");
            Console.WriteLine($"Second star: {maxDistance}");
        }

        // The string must be made of indexes, like "01234567"
        private int GetDistance(string str)
        {
            int distance = 0;

            for (int j = 0; j < str.Length - 1; j++)
            {
                string name = $"{names[str[j] - '0']}_{names[str[j + 1] - '0']}";

                distance += distances[name];
            }

            return distance;
        }

        private void Permute(string str, Action<string> callback = null)
        {
            Permute(str, 0, str.Length - 1, callback);
        }

        private void Permute(string str, int l, int r, Action<string> callback = null)
        {
            if (l == r)
            {
                //Console.WriteLine(str);
                if (callback != null)
                    callback(str);
            }
            else
            {
                for (int i = l; i <= r; i++)
                {
                    str = Swap(str, l, i);
                    Permute(str, l + 1, r, callback);
                    str = Swap(str, l, i);
                }
            }
        }

        private string Swap(string a, int i, int j)
        {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;

            return new string(charArray);
        }
    }
}
