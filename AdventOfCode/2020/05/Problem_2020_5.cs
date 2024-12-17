using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2020_5 : Problem
    {
        public override int Year => 2020;
        public override int Number => 5;

        public override void Run()
        {
            var ids = new List<int>(Lines.Length);

            foreach (var line in Lines)
            {
                ids.Add(GetID(line));
            }

            ids.Sort();

            var mySeatId = 0;

            for (var i = 0; i < ids.Count - 1; i++)
            {
                if (ids[i + 1] - ids[i] == 2)
                {
                    mySeatId = ids[i] + 1;
                    break;
                }
            }

            Console.WriteLine($"First star: {ids[ids.Count - 1]}");
            Console.WriteLine($"Second star: {mySeatId}");
        }

        #region Methods

        int GetID(string str)
        {
            var minRow = 0;
            var maxRow = 127;

            //Console.WriteLine($"Row is between {minRow} and {maxRow}");

            for (var i = 0; i < 7; i++)
            {
                if (str[i] == 'F')
                {
                    maxRow = (maxRow + minRow) / 2;
                }
                else if (str[i] == 'B')
                {
                    var odd = (maxRow + minRow) % 2 == 1;
                    minRow = (maxRow + minRow) / 2;
                    if (odd) minRow++;
                }
                else
                {
                    Console.WriteLine("Error: unknown character.");
                }

                //Console.WriteLine($"Found {str[i]}: Row is between {minRow} and {maxRow}");
            }

            var minColumn = 0;
            var maxColumn = 7;

            //Console.WriteLine($"Column is between {minColumn} and {maxColumn}");

            for (var i = 0; i < 3; i++)
            {
                if (str[i + 7] == 'L')
                {
                    maxColumn = (maxColumn + minColumn) / 2;
                }
                else if (str[i + 7] == 'R')
                {
                    var odd = (maxColumn + minColumn) % 2 == 1;
                    minColumn = (maxColumn + minColumn) / 2;
                    if (odd) minColumn++;
                }
                else
                {
                    Console.WriteLine("Error: unknown character.");
                }

                //Console.WriteLine($"Found {str[i + 7]}: Column is between {minColumn} and {maxColumn}");
            }

            //Console.WriteLine($"Row: {minRow}, Column: {minColumn}");

            return minRow * 8 + minColumn;
        }

        #endregion
    }
}
