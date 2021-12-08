using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Problem_2015_11 : Problem_2015
    {
        public override int Number => 11;

        public override void Run()
        {
            string _text = Text;

            while (!IsValid(_text))
            {
                _text = Increment(_text);
            }

            Console.WriteLine($"First star: {_text}");

            _text = Increment(_text);

            while (!IsValid(_text))
            {
                _text = Increment(_text);
            }

            Console.WriteLine($"Second star: {_text}");
        }

        private string Increment(string str)
        {
            int digit = str.Length - 1;
            bool retenue = false;
            StringBuilder strB = new StringBuilder(str);

            do
            {
                if (strB[digit] == 'z')
                {
                    strB[digit] = 'a';
                    retenue = true;
                    digit--;

                    if (digit == -1)
                    {
                        return null;
                    }
                }
                else
                {
                    strB[digit]++;
                    retenue = false;
                }
            }
            while (retenue);

            return strB.ToString();
        }

        private bool IsValid(string str)
        {
            return ContainsIncreasingPair(str, 3) &&
                   !ContainsIOL(str) &&
                   ContainsPairs(str, 2);
        }

        private bool ContainsIncreasingPair(string str, int count)
        {
            for (int i = 0; i < str.Length - (count - 1); i++)
            {
                bool found = true;

                for (int j = 1; j < count; j++)
                {
                    if (str[i + j] != (char)(str[i] + j))
                        found = false;
                }

                if (found)
                    return true;
            }

            return false;
        }

        private bool ContainsIOL(string str)
        {
            return str.Contains("i") || str.Contains("o") || str.Contains("l");
        }

        private bool ContainsPairs(string str, int count)
        {
            List<char> pairs = new List<char>();

            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1] && !pairs.Contains(str[i]))
                {
                    pairs.Add(str[i]);
                    i++;

                    if (pairs.Count == count)
                        return true;
                }
            }

            return false;
        }
    }
}
