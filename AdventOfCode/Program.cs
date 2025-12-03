using System;
using System.Diagnostics;
using static AdventOfCode.Problem2;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Problem_2025_2 prob = new();

            #region Config
            /// <summary>
            /// If true, will run {nbRuns} times P1 and P2 and print the average.
            /// </summary>
            bool measure = false;
            const int nbRuns = 1000;

            bool runTests = true;
            #endregion

            TestReport[] reportsFirstStar = [];
            TestReport[] reportsSecondStar = [];

            if (runTests)
            {
                reportsFirstStar = prob.RunTestsFirstStar();
                reportsSecondStar = prob.RunTestsSecondStar();
            }

            prob.InitPuzzle();

            #region First star
            WriteHeader("FIRST STAR");
            Stopwatch sw1 = Stopwatch.StartNew();
            long firstStarResult = prob.GetFirstStar();
            sw1.Stop();
            if (runTests)
            {
                PrintTestReports(reportsFirstStar);
            }
            PrintStarResult(firstStarResult, sw1.Elapsed);
            if (measure)
            {
                PrintAverageMS(nbRuns, () => prob.GetFirstStar());
            }
            #endregion

            #region Second star
            WriteHeader("SECOND STAR");
            Stopwatch sw2 = Stopwatch.StartNew();
            long secondStarResult = prob.GetSecondStar();
            sw2.Stop();
            if (runTests)
            {
                PrintTestReports(reportsSecondStar);
            }
            PrintStarResult(secondStarResult, sw2.Elapsed);
            if (measure)
            {
                PrintAverageMS(nbRuns, () => prob.GetSecondStar());
            }
            #endregion
        }

        static void WriteHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine($"║ {title.PadRight(36)} ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
        }

        static void PrintStarResult(long value, TimeSpan elapsed)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("* ");
            Console.Write($"Result: {value}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" ({elapsed.TotalMilliseconds:0.000} ms)");
            Console.ResetColor();
        }

        static void PrintTestReports(TestReport[] reports)
        {
            if (reports is null || reports.Length == 0) return;

            Console.WriteLine("Tests:");

            int nbOk = 0, nbFail = 0;
            foreach (TestReport report in reports)
            {
                if (report.Success)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  [V] {report.TestName}");
                    nbOk++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  [X] {report.TestName}: {report.ErrorMessage}");
                    nbFail++;
                }
            }

            Console.ResetColor();
            Console.WriteLine("  ─────────────");
            Console.ForegroundColor = nbFail == 0 ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"  {nbOk}/{reports.Length} passed");
            Console.ResetColor();
        }

        static void PrintAverageMS(int nbRuns, Action run)
        {
            double totalMilliseconds = 0;
            for (int i = 0; i < nbRuns; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                run();
                sw.Stop();
                totalMilliseconds += sw.Elapsed.TotalMilliseconds;
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"  Average: {(totalMilliseconds/nbRuns):0.000} ms ({nbRuns} runs)");
            Console.ResetColor();
        }

    }
}
