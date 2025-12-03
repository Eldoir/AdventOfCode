using System;

namespace AdventOfCode
{
    class Problem_2025_1 : Problem2
    {
        const int Start = 50;

        public override long GetFirstStar()
        {
            int current = Start;
            int sum = 0;
            for (int i = 0; i < Lines.Length; i++)
            {
                (int direction, int amount) = GetDirectionAndAmount(Lines[i]);
                current += direction * amount;
                if (current % 100 == 0) sum++;
            }
            return sum;
        }

        public override long GetSecondStar()
        {
            int current = Start;
            int sum = 0;
            for (int i = 0; i < Lines.Length; i++)
            {
                (int direction, int amount) = GetDirectionAndAmount(Lines[i]);
                int hundreds = amount / 100;
                sum += hundreds;
                amount %= 100;
                int old = current;
                current += direction * amount;
                if (current == 0)
                {
                    sum++;
                }
                else if (current >= 100)
                {
                    sum++;
                    current -= 100;
                }
                else if (current < 0)
                {
                    if (old != 0) sum++;
                    current += 100; // get back to equivalent positive value
                }
            }
            return sum;
        }

        private static (int, int) GetDirectionAndAmount(string line)
        {
            return (line[0] == 'L' ? -1 : 1, int.Parse(line[1..]));
        }

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 3),
        ];

        protected override Test[] TestsSecondStar =>
        [
            new Test("example", 6),
            new Test("R1000", 10, Name: "R1000"),
            new Test("L75\nR20", 1, Name: "Basic pass to the left"),
            new Test("R75\nL20", 1, "Basic pass to the right"),
            new Test("L50\nR50", 1, "Left ending on zero (1)"),
            new Test("L50\nL50", 1, "Left ending on zero (2)"),
            new Test("R50\nR50", 1, "Right ending on zero (1)"),
            new Test("R50\nL50", 1, "Right ending on zero (2)"),
            new Test("L200", 2, Name: "Big pass to the left"),
            new Test("R200", 2, Name: "Big pass to the right"),
            new Test("L150\nL50", 2, Name: "Extra pass left landing on zero (1)"),
            new Test("L150\nR50", 2, Name: "Extra pass left landing on zero (2)"),
            new Test("R150\nL50", 2, Name: "Extra pass right landing on zero (1)"),
            new Test("R150\nR50", 2, Name: "Extra pass right landing on zero (2)"),
        ];
    }
}
