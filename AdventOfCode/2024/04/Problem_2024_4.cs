using AdventOfCode.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Problem_2024_4 : Problem
    {
        public override int Year => 2024;
        public override int Number => 4;

        public override void Run()
        {
            //UseTestInput();
            int height = Lines.Length;
            int width = Lines[0].Length;
            List<string> stringsToConsider = new();

            // Count horizontally
            for (int i = 0; i < height; i++)
            {
                stringsToConsider.Add(Lines[i]);
            }
            // Count vertically
            for (int j = 0; j < width; j++)
            {
                string col = string.Empty;
                for (int i = 0; i < height; i++)
                {
                    col += Lines[i][j];
                }
                stringsToConsider.Add(col);
            }
            // Diagonals top-left to bottom-right
            // First the ones starting from the top row
            for (int startCol = 0; startCol < width; startCol++)
            {
                string diagonal = string.Empty;
                for (int row = 0, col = startCol; row < height && col < width; row++, col++)
                {
                    diagonal += Lines[row][col];
                }
                stringsToConsider.Add(diagonal);
            }
            // Then the ones starting from the left column
            for (int startRow = 1; startRow < height; startRow++)
            {
                string diagonal = string.Empty;
                for (int row = startRow, col = 0; row < height && col < width; row++, col++)
                {
                    diagonal += Lines[row][col];
                }
                stringsToConsider.Add(diagonal);
            }
            // Diagonals top-right to bottom-left
            // First the ones starting from the top row
            for (int startCol = width - 1; startCol >= 0; startCol--)
            {
                string diagonal = string.Empty;
                for (int row = 0, col = startCol; row < height && col >= 0; row++, col--)
                {
                    diagonal += Lines[row][col];
                }
                stringsToConsider.Add(diagonal);
            }
            // Then the ones starting from the right column
            for (int startRow = 1; startRow < height; startRow++)
            {
                string diagonal = string.Empty;
                for (int row = startRow, col = width - 1; row < height && col >= 0; row++, col--)
                {
                    diagonal += Lines[row][col];
                }
                stringsToConsider.Add(diagonal);
            }

            int firstStar = stringsToConsider.Select(s => s.NbOccurrences("XMAS") + s.NbOccurrences("SAMX")).Sum();
            Console.WriteLine($"First star: {firstStar}");

            int secondStar = 0;
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (Lines[i][j] == 'A')
                    {
                        char topLeft = Lines[i - 1][j - 1];
                        char topRight = Lines[i - 1][j + 1];
                        char bottomLeft = Lines[i + 1][j - 1];
                        char bottomRight = Lines[i + 1][j + 1];
                        int diagonal = 'M' + 'S';
                        if (topLeft + bottomRight == diagonal && topRight + bottomLeft == diagonal)
                        {
                            secondStar++;
                        }
                    }
                }
            }

            Console.WriteLine($"Second star: {secondStar}");
        }
    }
}
