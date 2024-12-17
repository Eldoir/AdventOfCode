using System;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2020_9 : Problem
    {
        public override int Year => 2020;
        public override int Number => 9;

        public override void Run()
        {
            var linesNb = Lines.Select(l => long.Parse(l)).ToArray();

            long firstInvalidNumber = 0;
            //var nbPreviousSteps = 5; // for the test set
            var nbPreviousSteps = 25;

            for (var i = nbPreviousSteps; i < linesNb.Length; i++)
            {
                if (!IsValid(i, linesNb, nbPreviousSteps))
                {
                    firstInvalidNumber = linesNb[i];
                    break;
                }
            }

            int contiguousSetSize = 2;
            var setStartidx = 0;

            do
            {
                setStartidx++;
                if (setStartidx + contiguousSetSize == linesNb.Length)
                {
                    setStartidx = 0;
                    contiguousSetSize++;
                }
            }
            while (Sum(setStartidx, contiguousSetSize, linesNb) != firstInvalidNumber);

            var sumSmallestLargest = GetSmallest(setStartidx, contiguousSetSize, linesNb) + GetLargest(setStartidx, contiguousSetSize, linesNb);

            Console.WriteLine($"First star: {firstInvalidNumber}");
            Console.WriteLine($"Second star: {sumSmallestLargest}");
        }

        #region Methods

        long Sum(int startIdx, int length, long[] arr)
        {
            long sum = 0;
            for (var i = startIdx; i < startIdx + length; i++)
            {
                sum += arr[i];
            }
            return sum;
        }

        long GetSmallest(int startIdx, int length, long[] arr)
        {
            var min = long.MaxValue;

            for (var i = startIdx; i < startIdx + length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }

            return min;
        }

        long GetLargest(int startIdx, int length, long[] arr)
        {
            var max = long.MinValue;

            for (var i = startIdx; i < startIdx + length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }

            return max;
        }

        bool IsValid(int idx, long[] arr, int pastNbs)
        {
            var nb = arr[idx];

            for (var i = idx - pastNbs; i < idx; i++)
            {
                for (var j = i + 1; j < idx; j++)
                {
                    if (arr[i] + arr[j] == arr[idx])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
