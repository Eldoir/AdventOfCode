using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem_2015_13 : Problem_2015
    {
        public override int Number => 13;

        Dictionary<string, Dictionary<string, int>> dic;
        List<string> names;

        public override void Run()
        {
            dic = new Dictionary<string, Dictionary<string, int>>();
            names = new List<string>();

            foreach (string line in Lines)
            {
                string[] inputs = line.Split(' ');

                string firstName = inputs[0];
                string secondName = inputs[10].Substring(0, inputs[10].Length - 1);
                int happiness = (inputs[2] == "gain" ? 1 : -1) * int.Parse(inputs[3]);

                if (!dic.ContainsKey(firstName))
                {
                    dic.Add(firstName, new Dictionary<string, int>());
                    names.Add(firstName);
                }

                dic[firstName].Add(secondName, happiness);
            }

            string indexes = "";
            for (int i = 0; i < dic.Count; i++)
                indexes += i;

            int maxHappiness = int.MinValue;

            Permute(indexes, (combination) =>
            {
                int happiness = GetHappiness(combination);

                if (happiness > maxHappiness)
                {
                    maxHappiness = happiness;
                }
            });

            Console.WriteLine($"First star: {maxHappiness}");

            // Now we include ourselves in the list, with 0 happiness for everyone
            maxHappiness = int.MinValue;
            string myName = "Me";

            foreach (var kvp in dic)
            {
                kvp.Value.Add(myName, 0);
            }

            dic.Add(myName, new Dictionary<string, int>());

            foreach (string str in names)
            {
                dic[myName][str] = 0;
            }

            names.Add(myName);

            indexes = "";
            for (int i = 0; i < dic.Count; i++)
                indexes += i;

            Permute(indexes, (combination) =>
            {
                int happiness = GetHappiness(combination);

                if (happiness > maxHappiness)
                {
                    maxHappiness = happiness;
                }
            });

            Console.WriteLine($"Second star: {maxHappiness}");
        }

        private int GetHappiness(string combination)
        {
            int result = 0;

            for (int i = 0; i < names.Count; i++)
            {
                int leftIndex = i - 1;
                if (leftIndex < 0) leftIndex = names.Count - 1;
                int rightIndex = (i + 1) % names.Count;

                string current = names[combination[i] - '0'];
                string leftNeighbour = names[combination[leftIndex] - '0'];
                string rightNeighbour = names[combination[rightIndex] - '0'];

                result += dic[current][leftNeighbour] + dic[current][rightNeighbour];
            }

            return result;
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
