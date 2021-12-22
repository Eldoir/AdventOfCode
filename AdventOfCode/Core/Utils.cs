
namespace AdventOfCode.Core
{
    public static class Utils
    {
        /// <summary>
        /// If <paramref name="n1"/> == <paramref name="n2"/>, returns 0.
        /// If <paramref name="n1"/> is greater than <paramref name="n2"/>, returns 1.
        /// If <paramref name="n1"/> is lower than <paramref name="n2"/>, returns -1.
        /// </summary>
        public static int GetIncrement(int n1, int n2)
        {
            if (n1 == n2)
                return 0;

            if (n1 < n2)
                return 1;

            return -1;
        }
    }
}
