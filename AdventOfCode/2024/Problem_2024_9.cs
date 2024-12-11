using System;

namespace AdventOfCode
{
    class Problem_2024_9 : Problem_2024
    {
        public override int Number => 9;

        public override void Run()
        {
            UseTestInput();
            int lastIdx = Text.Length - 1; // to decrease by 2 whenever lastCount is down to 0
            int lastCount = Text[lastIdx] - '0'; // to update whenever lastIdx changes
            int lastValue = lastIdx / 2; // to update whenever lastIdx changes
            long firstStar = 0;
            int pos = 0;
            bool representsFreeSpace = false;
            for (int i = 0; i < Text.Length; i++)
            {
                int value = Text[i] - '0';
                if (representsFreeSpace)
                {
                    for (int j = 0; j < value; j++)
                    {
                        firstStar += (pos + j) * lastValue;
                        lastCount--;
                        if (lastCount == 0)
                        {
                            lastIdx -= 2;
                            lastCount = Text[lastIdx] - '0';
                            lastValue = lastIdx / 2;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < value; j++)
                    {
                        firstStar += (pos + j) * (i / 2);
                    }
                }
                pos += value;
                representsFreeSpace = !representsFreeSpace;
            }
            Console.WriteLine(firstStar);
        }
    }
}
