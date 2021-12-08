using System;

namespace AdventOfCode
{
    class Problem_2015_2 : Problem_2015
    {
        public override int Number => 2;

        public override void Run()
        {
            int totalPaper = 0;
            int totalRibbon = 0;

            for (int i = 0; i < Lines.Length; i++)
            {
                string[] inputs = Lines[i].Split('x');
                int w = int.Parse(inputs[0]);
                int h = int.Parse(inputs[1]);
                int l = int.Parse(inputs[2]);
                totalPaper += GetWrappingPaperFeet(w, h, l);
                totalRibbon += GetRibbonFeet(w, h, l);
            }

            Console.WriteLine($"First star: {totalPaper}");
            Console.WriteLine($"Second star: {totalRibbon}");
        }

        private int GetWrappingPaperFeet(int w, int h, int l)
        {
            int min = Math.Min(Math.Min(l * w, w * h), h * l);

            return 2 * l * w + 2 * w * h + 2 * h * l + min;
        }

        private int GetRibbonFeet(int w, int h, int l)
        {
            int wrap = 2 * Math.Min(l + w, Math.Min(w + h, h + l));
            int bow = w * h * l;
            return wrap + bow;
        }
    }
}
