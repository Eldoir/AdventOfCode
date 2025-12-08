#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    /// <summary>
    /// Should be used after 2024.
    /// Classes inheriting from this must be named: Problem_{Year}_{Number}.
    /// </summary>
    abstract class Problem2
    {
        protected string? Text { get; private set; }
        protected string[]? Lines { get; private set; }

        public virtual long GetFirstStar() => 0;
        public virtual long GetSecondStar() => 0;

        #region MetaInfo
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
        #endregion

        private string ThisFolderPath => Path.Join("..", "..", "..", Meta.Year.ToString(), Meta.Number.ToString().PadLeft(2, '0'));
        private const string PuzzleFileName = "puzzle.txt";

        public void InitPuzzle()
        {
            InitTextAndLines(Path.Join(ThisFolderPath, PuzzleFileName));
        }

        #region Tests

        public record TestReport(string TestName, bool Success, string ErrorMessage);

        /// <summary>
        /// Will test against the puzzle file (usually puzzle.txt).
        /// </summary>
        protected static Test TestPuzzle(long expected) => new(PuzzleFileName, expected);

        /// <param name="Input">
        /// Can be a raw input, or a filename.
        /// If it's a filename, the file should be a .txt, located in the same directory as the problem's class file.
        /// Example: "first_test" will try to read "first_text.txt".
        /// </param>
        /// <param name="TestName">
        /// If null, then the name will be the filename if <paramref name="Input"/> is a filename,
        /// OR the test number in the list of tests if <paramref name="Input"/> is a raw input.
        /// </param>
        protected record Test(string Input, long Expected, string? Name = null);

        protected virtual Test[] TestsFirstStar => [];
        protected virtual Test[] TestsSecondStar => [];

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
            List<TestReport> reports = [];

            for (int i = 0; i < tests.Length; i++)
            {
                string input = tests[i].Input;
                string fileName = $"{input}.txt";
                string filePath = Path.Join(ThisFolderPath, fileName);
                bool fromFile = File.Exists(filePath);
                if (fromFile)
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
                string errorMessage = string.Empty;
                string testName = tests[i].Name ?? (fromFile ? fileName : $"Test {i}");
                if (!success)
                {
                    errorMessage = $"expected {expected}, got {result}";
                }

                reports.Add(new TestReport(testName, success, errorMessage));
            }

            return reports.ToArray();
        }

        #endregion

        private void InitTextAndLines(string filePath)
        {
            Text = File.ReadAllText(filePath);
            Lines = File.ReadAllLines(filePath);
        }
    }
}
