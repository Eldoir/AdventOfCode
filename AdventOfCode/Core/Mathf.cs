using System;

namespace AdventOfCode.Core
{
    static class Mathf
    {
        public const double Deg2Rad = (Math.PI * 2) / 360;

        public static int BinaryToInt(string binary)
        {
            return Convert.ToInt32(binary, 2);
        }
    }
}
