using System;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Extensions;

namespace AdventOfCode
{
    class Problem_2021_4 : Problem
    {
        public override int Year => 2021;
        public override int Number => 4;

        private const int BoardSize = 5;

        private int[] drawnNbs;
        private List<Board> boards;

        protected override void InitInternal()
        {
            drawnNbs = Lines[0].Split(',').Select(n => int.Parse(n)).ToArray();
            boards = new List<Board>();

            for (int i = 2; i < Lines.Length; i += BoardSize + 1)
            {
                if (string.IsNullOrEmpty(Lines[i]))
                    continue;

                boards.Add(new Board(Lines.Subset(i, BoardSize).ToArray()));
            }

            base.InitInternal();
        }

        public override void Run()
        {
            for (int i = 0; i < drawnNbs.Length; i++)
            {
                foreach (Board board in boards)
                {
                    board.MarkNumber(drawnNbs[i], i);
                }
            }

            var sortedBoards = boards.OrderBy(b => b.WonWithNbIdx);
            Console.WriteLine($"First star: {sortedBoards.First().GetFinalScore()}");
            Console.WriteLine($"Second star: {sortedBoards.Last().GetFinalScore()}");
        }

        class Board
        {
            int[,] numbers;
            bool[,] marked;

            public int WonWithNbIdx { get; private set; }
            private bool hasWon = false;
            private int wonWithNb;
            private int sumUnmarkedNumbersWhenWon;

            public Board(string[] lines)
            {
                numbers = new int[BoardSize, BoardSize];
                marked = new bool[BoardSize, BoardSize];

                for (int i = 0; i < BoardSize; i++)
                {
                    int[] nbs = lines[i].ToIntArray();

                    for (int j = 0; j < BoardSize; j++)
                    {
                        numbers[i, j] = nbs[j];
                    }
                }
            }

            public void MarkNumber(int nb, int nbIdx)
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (numbers[i, j] == nb)
                        {
                            marked[i, j] = true;

                            if (!hasWon && IsWinner())
                            {
                                WonWithNbIdx = nbIdx;
                                hasWon = true;
                                wonWithNb = nb;
                                sumUnmarkedNumbersWhenWon = GetSumAllUnmarkedNumbers();
                            }

                            return;
                        }
                    }
                }
            }

            public bool HasWonWith(int nb)
            {
                return wonWithNb == nb;
            }

            public int GetFinalScore()
            {
                return sumUnmarkedNumbersWhenWon * wonWithNb;
            }

            public bool IsWinner()
            {
                return HasWinningRow() || HasWinningColumn();
            }

            private int GetSumAllUnmarkedNumbers()
            {
                int sum = 0;

                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        if (!marked[i, j])
                            sum += numbers[i, j];
                    }
                }

                return sum;
            }

            private bool HasWinningRow()
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    if (IsWinningRow(i))
                        return true;
                }

                return false;
            }

            private bool HasWinningColumn()
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (IsWinningColumn(j))
                        return true;
                }

                return false;
            }

            private bool IsWinningRow(int rowIndex)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (!marked[rowIndex, j])
                        return false;
                }

                return true;
            }

            private bool IsWinningColumn(int columnIndex)
            {
                for (int i = 0; i < BoardSize; i++)
                {
                    if (!marked[i, columnIndex])
                        return false;
                }

                return true;
            }
        }
    }
}
