using System;
using System.Diagnostics;
using static AdventOfCode.Problem2;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            // Change class name to change current problem here
            Problem_2025_1 prob = new();

            // Tests
            bool runTests = true;
            TestReport[] reportsFirstStar = [];
            TestReport[] reportsSecondStar = [];
            if (runTests)
            {
                reportsFirstStar = prob.RunTestsFirstStar();
                reportsSecondStar = prob.RunTestsSecondStar();
            }

            prob.InitPuzzle();

            /// <summary>
            /// If true, will run {nbRuns} times P1 and P2 and print the average.
            /// </summary>
            bool measure = false;
            const int nbRuns = 1000;
            if (measure)
            {
                PrintAverageMS("P1", nbRuns, () => prob.GetFirstStar());
                PrintAverageMS("P2", nbRuns, () => prob.GetSecondStar());
            }

            // First star
            long firstStar = prob.GetFirstStar();
            if (runTests)
            {
                PrintTestReports(reportsFirstStar);
            }
            Console.WriteLine($"First star: {firstStar}");

            // Second star
            long secondStar = prob.GetSecondStar();
            if (runTests)
            {
                PrintTestReports(reportsSecondStar);
            }
            Console.WriteLine($"Second star: {secondStar}");
        }

        static void PrintAverageMS(string msg, int nbRuns, Action run)
        {
            double totalMilliseconds = 0;
            for (int i = 0; i < nbRuns; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                run();
                sw.Stop();
                totalMilliseconds += sw.Elapsed.TotalMilliseconds;
            }
            Console.WriteLine($"{msg} Avg (/{nbRuns}): {totalMilliseconds / nbRuns}ms");
        }

        static void PrintTestReports(TestReport[] reports)
        {
            if (reports.Length > 0)
            {
                Console.WriteLine("----------");
                for (int i = 0; i < reports.Length; i++)
                {
                    string message = reports[i].Success
                        ? "OK"
                        : $"FAILED {reports[i].ErrorMessage}";
                    Console.WriteLine(message);
                }
                Console.WriteLine("----------");
            }
        }
    }
}
