using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_9 : Problem2
    {
        public override int Year => 2024;
        public override int Number => 9;

        public override long GetFirstStar()
        {
            int lastIdx = Text.Length - 1; // to decrease by 2 whenever lastCount is down to 0
            int lastCount = Text[lastIdx] - '0'; // to update whenever lastIdx changes
            int lastValue = lastIdx / 2; // to update whenever lastIdx changes
            long firstStar = 0;
            int currentIdxInExpandedString = 0; // if Text = "123", the expanded string is "0..111"
            bool representsFreeSpace = false; // toggle this to avoid modulo
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

        public override long GetSecondStar()
        {
            // key is idx
            List<FreeSpaceSlot> freeSpaceSlots = new();

            // Setup
            long secondStar = 0;
            int currentIdxInExpandedString = 0;
            bool representsFreeSpace = false;
            Dictionary<int, (int StartIdxInExpandedString, int Count)> files = new(); // key is idx in Text
            for(int i = 0; i < Text.Length; i++)
            {
                int count = Text[i] - '0';
                if (representsFreeSpace)
                {
                    freeSpaceSlots.Add(new FreeSpaceSlot
                    {
                        Idx = i,
                        StartIdxInExpandedString = currentIdxInExpandedString,
                        Available = count
                    });
                }
                else
                {
                    files.Add(i, (currentIdxInExpandedString, count));
                }
                currentIdxInExpandedString += count;
                representsFreeSpace = !representsFreeSpace;
            }

            // Look for moves
            for (int i = Text.Length - 1; i > 0; i -= 2)
            {
                int count = Text[i] - '0';
                int value = i / 2;
                // Find the leftmost available slot
                for (int j = 0; j < freeSpaceSlots.Count; j++)
                {
                    FreeSpaceSlot slot = freeSpaceSlots[j];
                    if (slot.Idx > i) // search only for left slots
                        break;

                    if (freeSpaceSlots[j].Available >= count)
                    {
                        files.Remove(i); // Remove this file for the end count since we're gonna count it right now
                        for (int k = 0; k < count; k++)
                        {
                            secondStar += (slot.StartIdxInExpandedString + k) * value;
                        }
                        freeSpaceSlots[j].Available -= count;
                        freeSpaceSlots[j].StartIdxInExpandedString += count;
                        break;
                    }
                }
            }

            // Count files that did not move
            foreach (var file in files)
            {
                int value = file.Key / 2;
                for (int i = 0; i < file.Value.Count; i++)
                {
                    secondStar += (file.Value.StartIdxInExpandedString + i) * value;
                }
            }

            return secondStar;
        }

        class FreeSpaceSlot
        {
            public int Idx;
            public int StartIdxInExpandedString;
            public int Available;
        }

        protected override Test[] TestsFirstStar => new[]
        {
            new Test("101", 1),
            new Test("12345", 60),
            new Test("2333133121414131402", 1928)
        };

        protected override Test[] TestsSecondStar => new[]
        {
            new Test("101", 1),
            new Test("12345", 132),
            new Test("54321", 31),
            new Test("2333133121414131402", 2858)
        };
    }
}
