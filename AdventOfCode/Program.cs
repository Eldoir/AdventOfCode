using System;
using System.Diagnostics;
using static AdventOfCode.Problem2;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Problem_2024_9 prob = new();

            // Tests
            TestReport[] reportsFirstStar = prob.RunTestsFirstStar();
            TestReport[] reportsSecondStar = prob.RunTestsSecondStar();

            prob.InitPuzzle();

            // Measure
            if (prob.Measure)
            {
                const int nbRuns = 1000;
                PrintAverageMS("P1", nbRuns, () => prob.GetFirstStar());
                PrintAverageMS("P2", nbRuns, () => prob.GetSecondStar());
            }

            // First star
            long firstStar = prob.GetFirstStar();
            if (firstStar != 0)
            {
                PrintTestReports(reportsFirstStar);
                Console.WriteLine($"First star: {firstStar}");
            }

            // Second star
            long secondStar = prob.GetSecondStar();
            if (secondStar != 0)
            {
                PrintTestReports(reportsSecondStar);
                Console.WriteLine($"Second star: {secondStar}");
            }
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
                        : reports[i].ErrorMessage;
                    Console.WriteLine(message);
                }
                Console.WriteLine("----------");
            }
        }
    }
}
