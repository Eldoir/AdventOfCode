using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Problem_2020_8 : Problem
    {
        public override int Year => 2020;
        public override int Number => 8;

        public override void Run()
        {
            Console.WriteLine($"First star: {GetAccBeforeLoop(Lines).acc}");

            ExecResult result = new ExecResult();
            var jmpsTurnedToNop = new HashSet<int>();
            var foundNewJmp = false;

            do
            {
                var linesCloned = CopyLines(Lines);
                foundNewJmp = false;

                for (var i = 0; i < linesCloned.Length; i++)
                {
                    if (linesCloned[i].StartsWith("jmp") && !jmpsTurnedToNop.Contains(i))
                    {
                        linesCloned[i] = linesCloned[i].Replace("jmp", "nop");
                        jmpsTurnedToNop.Add(i);
                        foundNewJmp = true;
                        break;
                    }
                }

                if (!foundNewJmp)
                {
                    break;
                }

                result = GetAccBeforeLoop(linesCloned);
            }
            while (result.looped);

            if (!foundNewJmp)
            {
                Console.WriteLine("Can't turn any jmp to nop, will try the opposite");
                // :TODO: Should write the rest of this algorithm, but the program already gives the correct answer, so...
            }

            Console.WriteLine($"Second star: {result.acc}");
        }

        #region Methods

        string[] CopyLines(string[] lines)
        {
            var result = new string[lines.Length];

            for (var i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i];
            }

            return result;
        }

        ExecResult GetAccBeforeLoop(string[] lines)
        {
            var lineIdx = 0;
            var visitedLines = new HashSet<int>();

            var result = new ExecResult();

            while (!visitedLines.Contains(lineIdx) && lineIdx < lines.Length)
            {
                //Console.WriteLine($"LineIdx: {lineIdx}, acc: {acc}");
                visitedLines.Add(lineIdx);

                var args = lines[lineIdx].Split(' ');
                var cmd = args[0];
                var val = int.Parse(args[1]);

                if (cmd == "nop")
                {
                    lineIdx++;
                }
                else if (cmd == "acc")
                {
                    result.acc += val;
                    lineIdx++;
                }
                else if (cmd == "jmp")
                {
                    lineIdx += val;
                }
            }

            result.looped = lineIdx < lines.Length;

            return result;
        }

        #endregion

        class ExecResult
        {
            public int acc = 0;

            /// <summary>
            /// If not looped, it means the program executed correctly.
            /// </summary>
            public bool looped = false;
        }
    }
}
