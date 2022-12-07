using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2022_5 : Problem_2022
    {
        public override int Number => 5;
        string[] cranes;
        int startLine = -1;

        protected override void InitInternal()
        {
            int nbCranes = (Lines[0].Length - 3) / 4 + 1;
            cranes = new string[nbCranes];
            int i = 0;
            while (!Lines[i].StartsWith(" 1"))
            {
                string line = Lines[i];
                int craneNb = 0;
                for (int j = 1; j < line.Length; j += 4)
                {
                    if (line[j] != ' ')
                    {
                        cranes[craneNb] = line[j] + cranes[craneNb];
                    }
                    craneNb++;
                }
                i++;
            }
            startLine = i + 2; // skip line starting with " 1" and empty line

            base.InitInternal();
        }

        public override void Run()
        {
            string[] cranesSecondStar = Copy(cranes); 
            var regex = new Regex(@"move (\d+) from (\d+) to (\d+)");
            for (int i = startLine; i < Lines.Length; i++)
            {
                GroupCollection groups = regex.Match(Lines[i]).Groups;
                int move = int.Parse(groups[1].ToString());
                int from = int.Parse(groups[2].ToString());
                int to = int.Parse(groups[3].ToString());

                var moveCommand = new MoveCommand(move, from, to);
                moveCommand.Execute(cranes, reverse: true);
                moveCommand.Execute(cranesSecondStar, reverse: false);
            }

            Console.WriteLine($"First star: {GetStacksTops(cranes)}");
            Console.WriteLine($"Second star: {GetStacksTops(cranesSecondStar)}");
        }

        string[] Copy(string[] arr)
        {
            return arr.Select(s => new string(s)).ToArray();
        }

        string GetStacksTops(string[] arr)
        {
            return new string(arr.Select(c => c.Last()).ToArray());
        }

        class MoveCommand
        {
            private readonly int move;
            private readonly int from;
            private readonly int to;

            public MoveCommand(int move, int from, int to)
            {
                this.move = move;
                this.from = from;
                this.to = to;
            }

            public void Execute(string[] arr, bool reverse)
            {
                string fromCrane = arr[from - 1];
                string packet = fromCrane.Substring(fromCrane.Length - move, move);
                if (reverse)
                    packet = new string(packet.Reverse().ToArray());
                arr[to - 1] += packet;
                arr[from - 1] = fromCrane.Substring(0, fromCrane.Length - move);
            }
        }
    }
}
