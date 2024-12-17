using System.IO;

namespace AdventOfCode
{
    abstract class Problem
    {
        public abstract int Year { get; }
        public abstract int Number { get; }

        public string Text { get; private set; }
        public string[] Lines { get; private set; }

        public abstract void Run();

        public void Init()
        {
            InitTextAndLines();
            InitInternal();
        }

        protected virtual void InitInternal() { }

        protected void UseTestInput(int testFileNumber = 1)
        {
            _useTestInput = true;
            _testFileNumber = testFileNumber;
            InitTextAndLines();
        }

        private void InitTextAndLines()
        {
            string inputFilePath = GetInputFilePath();
            Text = File.ReadAllText(inputFilePath);
            Lines = File.ReadAllLines(inputFilePath);
        }

        private string GetInputFilePath()
        {
            return $"../../../{Year}/{Number.ToString().PadLeft(2, '0')}/{(_useTestInput ? $"test_{_testFileNumber}" : "puzzle")}.txt";
        }

        private bool _useTestInput = false;
        private int _testFileNumber;
    }
}
