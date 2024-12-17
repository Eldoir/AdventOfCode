using System;

namespace AdventOfCode
{
    class Problem_2015_10 : Problem
    {
        public override int Year => 2015;
        public override int Number => 10;

        private string _text;

        public override void Run()
        {
            const double conwayConstant = 1.30355844; // Value adapted to the exercise.

            _text = Text;
            int firstStarIterations = 40;
            int secondStarIterations = 50;

            for (int i = 0; i < firstStarIterations; i++)
            {
                _text = Process(_text);
            }

            int secondStarResult = (int)(_text.Length * Math.Pow(conwayConstant, secondStarIterations - firstStarIterations));
            Console.WriteLine($"First star: {_text.Length}");
            Console.WriteLine($"Second star: {secondStarResult}");
        }

        string Process(string text)
        {
            string result = "";

            char c = text[0];
            int count = 1;

            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] != c)
                {
                    result += $"{count}{c}";
                    c = text[i];
                    count = 1;
                }
                else
                {
                    count++;
                }
            }

            if (count != 0)
            {
                result += $"{count}{c}";
            }

            return result;
        }
    }
}
