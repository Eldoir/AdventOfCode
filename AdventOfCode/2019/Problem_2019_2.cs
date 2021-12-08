using System;

namespace AdventOfCode
{
    class Problem_2019_2 : Problem_2019
    {
        public override int Number => 2;

        string[] inputs;
        int[] ints;
        int currentIndex = 0;

        public override void Run()
        {
            inputs = Text.Split(',');
            ResetMemory();

            // For first star
            RunProgram(12, 2);
            Console.WriteLine($"First star: {ints[0]}");

            Console.WriteLine($"Second star: {GetResult(19690720)}");
        }

        private bool ShouldHalt()
        {
            return ints[currentIndex] == 99;
        }

        private void ResetMemory()
        {
            currentIndex = 0;
            ints = new int[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                ints[i] = int.Parse(inputs[i]);
            }
        }

        private int GetResult(int expectedOutput)
        {
            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    ResetMemory();

                    if (RunProgram(i, j, expectedOutput))
                    {
                        return i * 100 + j;
                    }
                }
            }

            return -1;
        }

        private bool RunProgram(int address1Value, int address2Value, int expectedOutput = -1)
        {
            ints[1] = address1Value; ints[2] = address2Value;

            while (!ShouldHalt())
            {
                if (ints[currentIndex] == 1)
                {
                    ints[ints[currentIndex + 3]] = ints[ints[currentIndex + 1]] + ints[ints[currentIndex + 2]];
                }
                else if (ints[currentIndex] == 2)
                {
                    ints[ints[currentIndex + 3]] = ints[ints[currentIndex + 1]] * ints[ints[currentIndex + 2]];
                }

                currentIndex += 4;
            }

            return ints[0] == expectedOutput;
        }
    }
}
