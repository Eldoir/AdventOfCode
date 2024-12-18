using System;
using static AdventOfCode.Problem2;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Problem_2024_9 prob = new();

            TestReport[] reportsFirstStar = prob.RunTestsFirstStar();
            TestReport[] reportsSecondStar = prob.RunTestsSecondStar();

            prob.InitPuzzle();

            long firstStar = prob.GetFirstStar();
            if (firstStar != 0)
            {
                PrintTestReports(reportsFirstStar);
                Console.WriteLine($"First star: {firstStar}");
            }
            long secondStar = prob.GetSecondStar();
            if (secondStar != 0)
            {
                PrintTestReports(reportsSecondStar);
                Console.WriteLine($"Second star: {secondStar}");
            }
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
