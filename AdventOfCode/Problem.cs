using System.IO;

namespace AdventOfCode
{
    abstract class Problem
    {
        public abstract int Year { get; }
        public abstract int Number { get; }

        public string Text { get; private set; }
        public string[] Lines { get; private set; }

        private bool useTestInput = false;
        private int testFileNumber;

        public void Init()
        {
            InitTextAndLines();
            InitInternal();
        }

        private void InitTextAndLines()
        {
            string inputFilePath = GetInputFilePath();
            Text = File.ReadAllText(inputFilePath);
            Lines = File.ReadAllLines(inputFilePath);
        }

        private string GetInputFilePath()
        {
            return $"../../../input/{Year}/{Number}" +
                   (useTestInput ? "_test" : "") +
                   (useTestInput && testFileNumber > 1 ? $"_{testFileNumber}" : "") +
                   ".txt";
        }

        protected void UseTestInput(int testFileNumber = 1)
        {
            useTestInput = true;
            this.testFileNumber = testFileNumber;
            InitTextAndLines();
        }

        protected virtual void InitInternal() { }

        public abstract void Run();
    }
}
