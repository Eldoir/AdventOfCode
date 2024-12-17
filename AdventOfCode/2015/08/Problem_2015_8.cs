using System;

namespace AdventOfCode
{
    class Problem_2015_8 : Problem
    {
        public override int Year => 2015;
        public override int Number => 8;

        public override void Run()
        {
            int nbCaracsCode = 0;
            int nbCaracsMemory = 0;
            int nbCaracsEncoded = 0;

            foreach (string line in Lines)
            {
                nbCaracsCode += line.Length;
                nbCaracsMemory += GetNbCaracsMemory(line);
                nbCaracsEncoded += GetNbCaracsEncoded(line);
            }

            Console.WriteLine($"First star: {(nbCaracsCode - nbCaracsMemory)}");
            Console.WriteLine($"Second star: {(nbCaracsEncoded - nbCaracsCode)}");
        }

        private int GetNbCaracsMemory(string line)
        {
            int nbCaracsMemory = 0;

            for (int i = 1; i < line.Length - 1; i++)
            {
                if (line[i] == '\\')
                {
                    if (line[i + 1] == '\\' || line[i + 1] == '\"') // single backslash or lone double-quote
                    {
                        nbCaracsMemory++;
                        i++; // skip this
                    }
                    else if (line[i + 1] == 'x') // single character ascii
                    {
                        nbCaracsMemory++;
                        i += 3; // skip this ascii char
                    }
                }
                else // simple char
                {
                    nbCaracsMemory++;
                }
            }

            return nbCaracsMemory;
        }

        private int GetNbCaracsEncoded(string line)
        {
            int nbCaracsEncoded = 6; // Increase of "" to "\"\"", happening in every line

            for (int i = 1; i < line.Length - 1; i++)
            {
                if (line[i] == '\\')
                {
                    if (line[i + 1] == '\\' || line[i + 1] == '\"') // single backslash or lone double-quote
                    {
                        nbCaracsEncoded += 4; // from '\\' to "\\\\" or '\"' to "\\\""
                        i++; // skip this
                    }
                    else if (line[i + 1] == 'x') // single character ascii
                    {
                        nbCaracsEncoded += 5;
                        i += 3; // skip this ascii char
                    }
                }
                else // simple char
                {
                    nbCaracsEncoded++;
                }
            }

            return nbCaracsEncoded;
        }
    }
}
