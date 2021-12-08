
namespace AdventOfCode.Extensions
{
    static class StringExtensions
    {
        public static int NbOccurrences(this string str, char c)
        {
            int total = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c) total++;
            }

            return total;
        }
    }
}
