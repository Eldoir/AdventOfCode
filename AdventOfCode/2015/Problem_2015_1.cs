using System;

namespace AdventOfCode
{
    class Problem_2015_1 : Problem_2015
    {
        public override int Number => 1;

        int firstStar = 0;
        int secondStar = 0;
        bool secondStarFound = false;

        public override void Run()
        {
            for (int i = 0; i < Text.Length; i++)
            {
                firstStar += (Text[i] == '(' ? 1 : -1);

                if (!secondStarFound && firstStar == -1)
                {
                    secondStar = i + 1;
                    secondStarFound = true;
                }
            }

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar}");
        }
    }
}
