using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_9 : Problem2
    {
        public override int Year => 2024;
        public override int Number => 9;

        protected override Test[] TestsFirstStar => new[]
        {
            new Test("101", 1),
            new Test("12345", 60),
            new Test("2333133121414131402", 1928)
        };

        public override long GetFirstStar()
        {
            int lastIdx = Text.Length - 1; // to decrease by 2 whenever lastCount is down to 0
            int lastCount = Text[lastIdx] - '0'; // to update whenever lastIdx changes
            int lastValue = lastIdx / 2; // to update whenever lastIdx changes
            long firstStar = 0;
            int currentIdxInExpandedString = 0; // if Text = "123", the expanded string is "0..111"
            bool representsFreeSpace = false;
            int lastIdxInExpandedString = Text.Sum(c => c - '0') - 1;
            for (int i = 0; i < Text.Length; i++)
            {
                int value = Text[i] - '0';
                if (representsFreeSpace)
                {
                    for (int j = 0; j < value; j++, currentIdxInExpandedString++)
                    {
                        firstStar += currentIdxInExpandedString * lastValue;
                        lastCount--;
                        lastIdxInExpandedString--;
                        if (lastCount == 0)
                        {
                            lastIdx--;
                            lastIdxInExpandedString -= Text[lastIdx] - '0';
                            lastIdx--;
                            lastCount = Text[lastIdx] - '0';
                            lastValue = lastIdx / 2;
                        }

                        if (currentIdxInExpandedString >= lastIdxInExpandedString)
                            return firstStar;
                    }
                }
                else
                {
                    for (int j = 0; j < value; j++, currentIdxInExpandedString++)
                    {
                        firstStar += currentIdxInExpandedString * (i / 2);

                        if (currentIdxInExpandedString >= lastIdxInExpandedString)
                            return firstStar;
                    }
                }
                representsFreeSpace = !representsFreeSpace;
            }

            return firstStar;
        }
    }
}
