using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// WARNING: takes a few minutes to run (~10-15).
    /// </summary>
    class Problem_2015_7 : Problem_2015
    {
        public override int Number => 7;

        Dictionary<string, ushort> wires = new Dictionary<string, ushort>();
        List<string> lines;
        List<int> doneIdx;
        bool secondStar = false;

        public override void Run()
        {
            doneIdx = new List<int>();

            lines = Lines.OrderBy(l => GetNbWires(l)).ThenBy(l => l).ToList();

            Process();

            //DisplayWires();

            if (wires.ContainsKey("a"))
            {
                ushort wireA = wires["a"];
                Console.WriteLine($"First star: {wireA}");
                wires.Clear();
                wires.Add("b", wireA);
                doneIdx.Clear();
                secondStar = true;
                Process();
                Console.WriteLine($"Second star: {wires["a"]}");
            }
            else
                Console.WriteLine("No wire 'a'");
        }

        private int GetNbWires(string str)
        {
            string[] inputs = str.Split(' ');

            if (inputs.Length == 3)
            {
                return IsWire(inputs[0]) ? 1 : 0;
            }
            else if (inputs.Length == 4)
            {
                return IsWire(inputs[1]) ? 1 : 0;
            }
            else // inputs.Length == 5
            {
                int nb = 0;
                nb += IsWire(inputs[1]) ? 1 : 0;
                nb += IsWire(inputs[3]) ? 1 : 0;
                return nb;
            }
        }

        private bool IsWire(string str)
        {
            try
            {
                ushort.Parse(str);
                return false;
            }
            catch
            {
                return true;
            }
        }

        private void Process()
        {
            while (doneIdx.Count < lines.Count)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (!doneIdx.Contains(i))
                    {
                        if (ProcessLine(lines[i]))
                        {
                            doneIdx.Add(i);
                        }
                    }
                }
            }
        }

        private bool ProcessLine(string line)
        {
            string[] inputs = line.Split(' ');

            try
            {
                if (inputs.Length == 3) // simple assignment
                {
                    AssignOrCreate(inputs[2], GetValue(inputs[0]));
                }
                else if (inputs.Length == 4) // NOT
                {
                    AssignOrCreate(inputs[3], NOT(GetValue(inputs[1])));
                }
                else if (inputs.Length == 5) // AND, OR, LSHIFT, RSHIFT
                {
                    AssignOrCreate(inputs[4], DoOp(inputs[1], GetValue(inputs[0]), GetValue(inputs[2])));
                }

                return true;
            }
            catch (UnknownWireException)
            {
                // A gate provides no signal until all of its inputs have a signal.
                // One if its inputs don't have a signal: we must ignore this gate for now.
                return false;
            }
        }

        private ushort GetValue(string str)
        {
            ushort value = 0;

            try
            {
                value = ushort.Parse(str);
            }
            catch (FormatException) // it's not an ushort, it's a wire
            {
                if (wires.ContainsKey(str))
                {
                    value = wires[str];
                }
                else // We don't know this wire yet, we create it
                {
                    throw new UnknownWireException(str);
                    //AssignOrCreate(str);
                }
            }

            return value;
        }

        private void AssignOrCreate(string wire, ushort value = 0)
        {
            if (!wires.ContainsKey(wire))
            {
                wires.Add(wire, 0);
            }

            if (!secondStar || wire != "b")
                wires[wire] = value;
        }

        private ushort DoOp(string op, ushort value1, ushort value2)
        {
            switch (op)
            {
                case "AND": return AND(value1, value2);
                case "OR": return OR(value1, value2);
                case "LSHIFT": return LSHIFT(value1, value2);
                case "RSHIFT": return RSHIFT(value1, value2);
                default: return 0;
            }
        }

        private ushort NOT(ushort value)
        {
            return (ushort)(~(int)value);
        }

        private ushort AND(ushort value1, ushort value2)
        {
            return (ushort)((int)value1 & (int)value2);
        }

        private ushort OR(ushort value1, ushort value2)
        {
            return (ushort)((int)value1 | (int)value2);
        }

        private ushort LSHIFT(ushort value1, ushort value2)
        {
            return (ushort)((int)value1 << (int)value2);
        }

        private ushort RSHIFT(ushort value1, ushort value2)
        {
            return (ushort)((int)value1 >> (int)value2);
        }

        private void DisplayWires()
        {
            var keys = wires.Keys.OrderBy(k => k);

            foreach (var key in keys)
            {
                Console.WriteLine($"{key}: {wires[key]}");
            }
        }

        [Serializable]
        public class UnknownWireException : Exception
        {
            public UnknownWireException() { }

            public UnknownWireException(string wire)
            : base($"Unknown wire: {wire}") { }
        }
    }
}
