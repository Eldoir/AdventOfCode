using System.IO;

namespace AdventOfCode
{
    abstract class Problem
    {
        public abstract int Year { get; }
        public abstract int Number { get; }

        public string Text => useTestInput ? textTest : text;
        public string[] Lines => useTestInput ? linesTest : lines;

        private string text;
        private string[] lines;

        private string textTest;
        private string[] linesTest;

        private bool useTestInput = false;

        public void Init()
        {
            (text, lines) = InitTextAndLines(isTestInput: false);

            if (TestFileExists())
            {
                (textTest, linesTest) = InitTextAndLines(isTestInput: true);
            }

            InitInternal();
        }

        private (string, string[]) InitTextAndLines(bool isTestInput)
        {
            string inputFilePath = GetInputFilePath(isTestInput);
            return (File.ReadAllText(inputFilePath), File.ReadAllLines(inputFilePath));
        }

        private bool TestFileExists()
        {
            return File.Exists(GetInputFilePath(isTestInput: true));
        }

        private string GetInputFilePath(bool isTestInput)
        {
            return $"../../../input/{Year}/{Number}{(isTestInput ? "_test" : "")}.txt";
        }

        protected void UseTestInput()
        {
            useTestInput = true;
        }

        protected virtual void InitInternal() { }

        public abstract void Run();
    }
}
