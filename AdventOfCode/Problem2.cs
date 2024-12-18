using System;
using System.Collections.Generic;
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

        public void InitPuzzle()
        {
            InitTextAndLines(ThisFolderPath + "puzzle.txt");
        }

        #region Tests

        public record TestReport(bool Success, string ErrorMessage);

        protected record Test(string Input, long Expected);
        protected virtual Test[] TestsFirstStar => Array.Empty<Test>();
        protected virtual Test[] TestsSecondStar => Array.Empty<Test>();

        public TestReport[] RunTestsFirstStar()
        {
            return RunTests(TestsFirstStar, GetFirstStar);
        }

        public TestReport[] RunTestsSecondStar()
        {
            return RunTests(TestsSecondStar, GetSecondStar);
        }

        private TestReport[] RunTests(Test[] tests, Func<long> func)
        {
            List<TestReport> reports = new();

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
                bool success = result == expected;
                reports.Add(new TestReport(success, success ? string.Empty : $"Failed (expected: {expected}, got: {result})"));
            }

            return reports.ToArray();
        }

        private void InitTextAndLines(string filePath)
        {
            Text = File.ReadAllText(filePath);
            Lines = File.ReadAllLines(filePath);
        }

        #endregion
    }
}
