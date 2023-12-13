using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2023_4 : Problem_2023
    {
        public override int Number => 4;

        record Card(int Number, int[] WinningNumbers, int[] NumbersYouHave);

        public override void Run()
        {
            Card[] cards = new Card[Lines.Length];

            for (int i = 0; i < Lines.Length; i++)
            {
                string[] inputs = Lines[i].Split(':');
                int n = int.Parse(inputs[0].Split(' ').Last());
                string[] numbers = inputs[1].Split('|');
                int[] winningNumbers = SafeParse(numbers[0]);
                int[] numbersYouHave = SafeParse(numbers[1]);
                cards[i] = new Card(n, winningNumbers, numbersYouHave);
            }

            int firstStar = 0;
            (int N, int Matches, int Copies)[] secondStar = cards.Select(c => (c.Number, 0, 1)).ToArray();
            foreach (Card card in cards)
            {
                int matches = card.WinningNumbers.Intersect(card.NumbersYouHave).Count();
                if (matches > 0)
                {
                    firstStar += (int)Math.Pow(2, matches - 1);

                    for (int i = card.Number; i < card.Number + matches; i++)
                    {
                        secondStar[i].Copies += secondStar[card.Number - 1].Copies;
                    }
                }
            }

            Console.WriteLine($"First star: {firstStar}");
            Console.WriteLine($"Second star: {secondStar.Sum(s => s.Copies)}");
        }

        private static int[] SafeParse(string s)
        {
            string[] numbers = s.Split(' ');
            List<int> result = new();
            foreach (string n in numbers)
            {
                if (int.TryParse(n, out int i))
                {
                    result.Add(i);
                }
            }
            return result.ToArray();
        }
    }
}
