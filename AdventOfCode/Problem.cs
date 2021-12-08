using System.IO;

namespace AdventOfCode
{
    abstract class Problem
    {
        public abstract int Year { get; }
        public abstract int Number { get; }

        public string Text { get; private set; }
        public string[] Lines { get; private set; }

        public void Init()
        {
            string filepath = $"input/{Year}/{Number}.txt";
            Text = File.ReadAllText(filepath);
            Lines = File.ReadAllLines(filepath);

            InitInternal();
        }

        protected virtual void InitInternal() { }

        public abstract void ExecuteFirstStar();
        public abstract void ExecuteSecondStar();
    }
}
