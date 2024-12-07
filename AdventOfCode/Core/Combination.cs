using System.Collections.Generic;

namespace AdventOfCode.Core
{
    public static class Combination
    {
        /// <summary>
        /// <para>
        /// Example with (2, 2):
        /// [[0, 0], [0, 1], [1, 0], [1, 1]]
        /// </para>
        /// <para>
        /// With (2, 3):
        /// [[0, 0], [0, 1], [0, 2], [1, 0], [1, 1], [1, 2], [2, 0], [2, 1], [2, 2]]
        /// </para>
        /// <para>
        /// With (3, 2):
        /// [[0, 0, 0], [0, 0, 1], [0, 1, 0], [0, 1, 1], [1, 0, 0], [1, 0, 1], [1, 1, 0], [1, 1, 1]]
        /// </para>
        /// </summary>
        public static List<int[]> Generate(int arrayLength, int valueCount)
        {
            List<int[]> combinations = new();
            Generate(arrayLength, valueCount, new int[arrayLength], 0, combinations);
            return combinations;
        }

        private static void Generate(int arrayLength, int valueCount, int[] current, int position, List<int[]> combinations)
        {
            if (position == arrayLength)
            {
                // Base case: Add a copy of the current array to the combinations
                combinations.Add((int[])current.Clone());
                return;
            }

            // Recursive case: Set each position to all possible values and recurse
            for (int i = 0; i < valueCount; i++)
            {
                current[position] = i;
                Generate(arrayLength, valueCount, current, position + 1, combinations);
            }
        }
    }
}
