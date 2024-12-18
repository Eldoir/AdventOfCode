using System;
using System.IO;

namespace AdventOfCode
{
    abstract class Problem2
    {
        public abstract int Year { get; }
        public abstract int Number { get; }

        protected string Text { get; private set; }
        protected string[] Lines { get; private set; }

        public virtual long GetFirstStar() => 0;
        public virtual long GetSecondStar() => 0;

        private string ThisFolderPath => $"../../../{Year}/{Number.ToString().PadLeft(2, '0')}/";

        protected Problem2()
        {
            RunTests(TestsFirstStar, GetFirstStar);
            RunTests(TestsSecondStar, GetSecondStar);

            InitTextAndLines(ThisFolderPath + "puzzle.txt");
        }

        #region Tests

        protected record Test(string Input, long Expected);
        protected virtual Test[] TestsFirstStar => Array.Empty<Test>();
        protected virtual Test[] TestsSecondStar => Array.Empty<Test>();

        private void RunTests(Test[] tests, Func<long> func)
        {
            if (tests.Length == 0)
                return;

            Console.WriteLine("----------");
            for (int i = 0; i < tests.Length; i++)
            {
                string input = tests[i].Input;
                string filePath = ThisFolderPath + input;
                if (File.Exists(filePath))
                {
                    InitTextAndLines(filePath);
                }
                else
                {
                    Text = input;
                    Lines = Text.Split('\n');
                }
                
                long result = func();
                long expected = tests[i].Expected;
                string message = result == expected
                    ? "OK"
                    : $"Failed (expected: {expected}, got: {result})";
                Console.WriteLine($"TEST {i + 1} {message}");
            }
            Console.WriteLine("----------");
        }

        private void InitTextAndLines(string filePath)
        {
            Text = File.ReadAllText(filePath);
            Lines = File.ReadAllLines(filePath);
        }

        #endregion
    }
}
