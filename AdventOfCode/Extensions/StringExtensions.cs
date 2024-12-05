using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Extensions
{
    static class StringExtensions
    {
        public static int[] IndexesOf(this string str, string needle)
        {
            List<int> indexes = new();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i..].StartsWith(needle))
                    indexes.Add(i);
            }
            return indexes.ToArray();
        }

        public static int NbOccurrences(this string str, char c)
        {
            int total = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c) total++;
            }

            return total;
        }

        public static int NbOccurrences(this string str, string needle)
        {
            int total = 0;

            for (int i = 0; i < str.Length - needle.Length + 1; i++)
            {
                if (str.Substring(i, needle.Length) == needle)
                    total++;
            }

            return total;
        }

        public static string Reverse(this string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string RemoveWhitespace(this string str)
        {
            return new string(str.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Numbers in <paramref name="str"/> must be separated by one or more whitespaces.
        /// </summary>
        public static int[] ToIntArray(this string str)
        {
            var regex = new Regex(@"\s*(\d+)");
            var matches = regex.Matches(str);

            var result = new List<int>();

            foreach (Match match in matches)
            {
                result.Add(int.Parse(match.Groups[1].ToString()));
            }

            return result.ToArray();
        }

        public static string[] Split(this string str, string needle)
        {
            return str.Split(new string[] { needle }, StringSplitOptions.None);
        }

        public static string Replace(this string str, string needle, int idx, int length)
        {
            string newStr = "";

            for (int i = 0; i < idx; i++)
            {
                newStr += str[i];
            }

            newStr += needle;

            for (int i = idx + length; i < str.Length; i++)
            {
                newStr += str[i];
            }

            return newStr;
        }

        public static string Minus(this string str, string other)
        {
            string result = "";

            for (int i = 0; i < str.Length; i++)
            {
                int idx = other.IndexOf(str[i]);

                if (idx == -1)
                    result += str[i];
                else
                    other = other.Remove(idx, 1);
            }

            return result;
        }

        public static bool ContainsAllLettersOf(this string str, string other)
        {
            return other.Minus(str).Length == 0;
        }

        public static bool IsEqualToShuffled(this string str, string other)
        {
            return str.ContainsAllLettersOf(other) && other.ContainsAllLettersOf(str);
        }
    }
}
