using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2025_6 : Problem2
    {
        public override long GetFirstStar()
        {
            ProblemData problem = Parse();
            long grandTotal = 0;
            for (int j = 0; j < problem.Width; j++)
            {
                (long total, Operation opFunc) = GetSetup(problem.Operators[j]);
                for (int i = 0; i < problem.Height; i++)
                {
                    total = opFunc(total, int.Parse(problem.NumberStrings[i, j]));
                }
                grandTotal += total;
            }
            return grandTotal;
        }

        public override long GetSecondStar()
        {
            ProblemData problem = Parse();
            long grandTotal = 0;
            for (int j = 0; j < problem.Width; j++)
            {
                Operator op = problem.Operators[j];
                (long total, Operation opFunc) = GetSetup(op);
                for (int m = op.Magnitude - 1; m >= 0; m--) // read from right to left
                {
                    string numberStr = string.Empty;
                    for (int i = 0; i < problem.Height; i++)
                    {
                        numberStr += problem.NumberStrings[i, j][m];
                    }
                    total = opFunc(total, int.Parse(numberStr));
                }
                grandTotal += total;
            }
            return grandTotal;
        }

        static (long Base, Operation Op) GetSetup(Operator op)
        {
            return op.OpChar == '+'
                ? (0, Add)
                : (1, Multiply);
        }

        ProblemData Parse()
        {
            Operator[] operators = ParseOperators(Lines[^1]);
            int height = Lines.Length - 1;
            int width = operators.Length;
            var numberStrings = new string[height, width];
            int startIndex = 0;
            for (int j = 0; j < width; j++) // for each column
            {
                int magnitude = operators[j].Magnitude;
                for (int i = 0; i < height; i++) // for each row
                {
                    numberStrings[i, j] = Lines[i].Substring(startIndex, magnitude);
                }
                startIndex += magnitude + 1; // + 1 because of separating space
            }

            return new ProblemData(width, height, numberStrings, operators);

            #region Local methods
            static Operator[] ParseOperators(string operatorLine)
            {
                List<Operator> operators = [];
                char currentOperator = operatorLine[0];
                int currentOperatorMagnitude = 1;
                int i = 1;
                while (i < operatorLine.Length)
                {
                    if (operatorLine[i] != ' ')
                    {
                        operators.Add(new Operator(currentOperator, currentOperatorMagnitude - 1)); // magnitude - 1 because of separating space
                        currentOperator = operatorLine[i];
                        currentOperatorMagnitude = 1;
                    }
                    else
                    {
                        currentOperatorMagnitude++;
                    }

                    i++;
                }
                operators.Add(new Operator(currentOperator, currentOperatorMagnitude)); // store last operator

                return operators.ToArray();
            }
            #endregion
        }

        record class ProblemData(int Width, int Height, string[,] NumberStrings, Operator[] Operators);

        record struct Operator(char OpChar, int Magnitude);
        delegate long Operation(long a, long b);
        private readonly static Operation Add = (a, b) => a + b;
        private readonly static Operation Multiply = (a, b) => a * b;

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 4277556)
        ];

        protected override Test[] TestsSecondStar =>
        [
            new Test("example", 3263827)
        ];
    }
}
