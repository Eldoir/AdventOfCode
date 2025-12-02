#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    abstract class Problem2
    {
        protected string Text { get; private set; }
        protected string[] Lines { get; private set; }

        public virtual long GetFirstStar() => 0;
        public virtual long GetSecondStar() => 0;

        /// <summary>
        /// If true, will run 1000 times P1 and P2 and print the average.
        /// </summary>
        public virtual bool Measure => false;

        record class MetaInfo(int Year, int Number);
        private MetaInfo Meta
        {
            get
            {
                if (_meta is null)
                {
                    string className = GetType().Name;
                    const string pattern = @"^Problem_(\d+)_(\d+)";
                    Match match = Regex.Match(className, pattern);
                    if (!match.Success)
                        throw new InvalidOperationException($"Problem class name must comply with: \"{pattern}\"");
                    return new MetaInfo(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                }
                return _meta;
            }

        }
        private MetaInfo? _meta;

        private string ThisFolderPath => $"../../../{Meta.Year}/{Meta.Number.ToString().PadLeft(2, '0')}/";
        private const string PuzzleFileName = "puzzle.txt";

        public void InitPuzzle()
        {
            InitTextAndLines(Path.Join(ThisFolderPath, PuzzleFileName));
        }

        #region Tests

        public record TestReport(bool Success, string ErrorMessage);
        protected static Test TestPuzzle(long expected) => new Test(PuzzleFileName, expected);
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
