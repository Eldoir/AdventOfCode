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

        protected virtual int? TestFileNumber => null;

        /// <summary>
        /// Implement this if you have some operations to do before running all the tests.
        /// </summary>
        protected virtual void Parse() { }

        public virtual long GetFirstStar() => 0;
        public virtual long GetSecondStar() => 0;

        protected Problem2()
        {
            Parse();

            RunTests(TestsFirstStar, GetFirstStar);
            RunTests(TestsSecondStar, GetSecondStar);

            string inputFilePath = $"../../../{Year}/{Number.ToString().PadLeft(2, '0')}/" +
                $"{(TestFileNumber is null ? "puzzle" : $"test_{TestFileNumber}")}.txt";

            Text = File.ReadAllText(inputFilePath);
            Lines = File.ReadAllLines(inputFilePath);
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
                Text = tests[i].Input;
                Lines = Text.Split('\n');
                long result = func();
                long expected = tests[i].Expected;
                string message = result == expected
                    ? "OK"
                    : $"Failed (expected: {expected}, got: {result})";
                Console.WriteLine($"TEST {i + 1} {message}");
            }
            Console.WriteLine("----------");
        }

        #endregion
    }
}
