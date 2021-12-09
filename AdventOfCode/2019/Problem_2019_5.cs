using System;

namespace AdventOfCode
{
    class Problem_2019_5 : Problem_2019
    {
        public override int Number => 5;

        string[] inputs;
        int[] ints;
        int currentIndex = 0;

        public override void Run()
        {
            inputs = Text.Split(',');
            ResetMemory();

            Console.WriteLine("Input 1 for 1st star, or 5 for 2nd star");
            RunProgram();
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

        private bool RunProgram(int expectedOutput = -1)
        {
            while (!ShouldHalt())
            {
                string opcode = ints[currentIndex].ToString().PadLeft(4, '0');
                int val1, val2; // Used for storing parameters

                switch (opcode[opcode.Length - 1] - '0')
                {
                    case 1: // Add
                        ints[ints[currentIndex + 3]] = GetParamValue(opcode, 0) + GetParamValue(opcode, 1);
                        currentIndex += 4;
                        break;
                    case 2: // Multiply
                        ints[ints[currentIndex + 3]] = GetParamValue(opcode, 0) * GetParamValue(opcode, 1);
                        currentIndex += 4;
                        break;
                    case 3: // Input
                        Console.WriteLine("Input: ");
                        string input = Console.ReadLine();
                        ints[ints[currentIndex + 1]] = int.Parse(input);
                        currentIndex += 2;
                        break;
                    case 4: // Output
                        Console.Write(GetParamValue(opcode, 0));
                        currentIndex += 2;
                        break;
                    case 5: // Jump if true
                        val1 = GetParamValue(opcode, 0);
                        val2 = GetParamValue(opcode, 1);
                        if (val1 != 0)
                            currentIndex = val2;
                        else
                            currentIndex += 3;
                        break;
                    case 6: // Jump if false
                        val1 = GetParamValue(opcode, 0);
                        val2 = GetParamValue(opcode, 1);
                        if (val1 == 0)
                            currentIndex = val2;
                        else
                            currentIndex += 3;
                        break;
                    case 7: // Less than
                        val1 = GetParamValue(opcode, 0);
                        val2 = GetParamValue(opcode, 1);
                        ints[ints[currentIndex + 3]] = (val1 < val2 ? 1 : 0);
                        currentIndex += 4;
                        break;
                    case 8: // Equals
                        val1 = GetParamValue(opcode, 0);
                        val2 = GetParamValue(opcode, 1);
                        ints[ints[currentIndex + 3]] = (val1 == val2 ? 1 : 0);
                        currentIndex += 4;
                        break;
                    default:
                        Console.WriteLine("Unknown opcode");
                        break;
                }
            }

            Console.WriteLine();

            return ints[0] == expectedOutput;
        }

        private int GetParamValue(string opcode, int idx)
        {
            if (opcode[1 - idx] == '1') return ints[currentIndex + idx + 1];
            else return ints[ints[currentIndex + idx + 1]];
        }
    }
}
