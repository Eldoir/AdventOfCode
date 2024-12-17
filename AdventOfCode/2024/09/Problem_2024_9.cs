using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_9 : Problem2
    {
        public override int Year => 2024;
        public override int Number => 9;

        protected override Test[] TestsFirstStar => new[]
        {
            new Test("12345", 60),
            new Test("2333133121414131402", 1928)
        };

        public override long GetFirstStar()
        {
            int lastIdx = Text.Length - 1; // to decrease by 2 whenever lastCount is down to 0
            int lastCount = Text[lastIdx] - '0'; // to update whenever lastIdx changes
            int lastValue = lastIdx / 2; // to update whenever lastIdx changes
            int lastSpaces = 0; // total spaces from the right to the left
            long firstStar = 0;
            int currentIdx = 0;
            bool representsFreeSpace = false;
            int total = Text.Sum(c => c - '0');
            for (int i = 0; i < Text.Length; i++)
            {
                int value = Text[i] - '0';
                if (representsFreeSpace)
                {
                    for (int j = 0; j < value; j++, currentIdx++)
                    {
                        if (currentIdx >= total - lastSpaces - lastIdx)
                            return firstStar;

                        firstStar += currentIdx * lastValue;
                        lastCount--;
                        if (lastCount == 0)
                        {
                            lastIdx--;
                            lastSpaces += Text[lastIdx] - '0';
                            lastIdx--;
                            lastCount = Text[lastIdx] - '0';
                            lastValue = lastIdx / 2;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < value; j++, currentIdx++)
                    {
                        if (currentIdx >= total - lastSpaces - lastIdx)
                            return firstStar;

                        firstStar += currentIdx * (i / 2);
                    }
                }
                representsFreeSpace = !representsFreeSpace;
            }

            return firstStar;
        }
    }
}
