using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2022_6 : Problem_2022
    {
        public override int Number => 6;

        public override void Run()
        {
            Console.WriteLine($"First star: {GetFirstMarkerPosition(Lines[0], 4)}");
            Console.WriteLine($"Second star: {GetFirstMarkerPosition(Lines[0], 14)}");
        }

        int GetFirstMarkerPosition(string str, int markerLength)
        {
            for (int i = 0; i < str.Length - markerLength + 1; i++)
            {
                string sub = str.Substring(i, markerLength);
                if (sub.Distinct().Count() == sub.Length)
                    return i + markerLength;
            }

            return -1;
        }
    }
}
