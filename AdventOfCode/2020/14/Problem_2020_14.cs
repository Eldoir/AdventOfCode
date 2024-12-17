using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Problem_2020_14 : Problem
    {
        public override int Year => 2020;
        public override int Number => 14;

        public override void Run()
        {
            var mask = "";
            var memory = new Dictionary<long, long>();

            foreach (var line in Lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = GetMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    SetMemoryAddress(line, ref memory, mask);
                }
                else
                {
                    Console.WriteLine("Unknown opcode.");
                }
            }

            Console.WriteLine($"First star: {GetSum(memory)}");

            mask = "";
            memory.Clear();

            foreach (var line in Lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = GetMask(line);
                }
                else if (line.StartsWith("mem"))
                {
                    SetMemoryAddresses(line, ref memory, mask);
                }
                else
                {
                    Console.WriteLine("Unknown opcode.");
                }
            }

            Console.WriteLine($"Second star: {GetSum(memory)}");
        }

        #region Methods

        string GetMask(string line)
        {
            return line.Split(" = ")[1];
        }

        void GetAddressAndVal(string line, out int address, out int val)
        {
            var pattern = @"mem\[(\d+)\] = (\d+)";
            var match = Regex.Match(line, pattern);
            address = int.Parse(match.Groups[1].Value);
            val = int.Parse(match.Groups[2].Value);
        }

        void SetMemoryAddress(string line, ref Dictionary<long, long> memory, string mask)
        {
            int address, val;
            GetAddressAndVal(line, out address, out val);

            var valBin = ToBinary36(val);
            var valBinWithMask = ApplyMaskFirstStar(valBin, mask);

            if (!memory.ContainsKey(address))
            {
                memory.Add(address, 0);
            }

            memory[address] = ToDec(valBinWithMask);
        }

        void SetMemoryAddresses(string line, ref Dictionary<long, long> memory, string mask)
        {
            int address, val;
            GetAddressAndVal(line, out address, out val);

            var addressBin = ToBinary36(address);
            var addressBinWithMask = ApplyMaskSecondStar(addressBin, mask);

            var countX = addressBinWithMask.Count(c => c == 'X');
            var nbMax = (int)Math.Pow(2, countX);

            for (var i = 0; i < nbMax; i++)
            {
                var xValueStr = ToBinary(i, countX);
                var combStr = ReplaceX(addressBinWithMask, xValueStr);
                var combVal = ToDec(combStr);

                if (!memory.ContainsKey(combVal))
                {
                    memory.Add(combVal, 0);
                }

                memory[combVal] = val;
            }
        }

        string ReplaceX(string strWithX, string xValueStr)
        {
            int idxInXValueStr = 0;

            string result = "";

            for (var i = 0; i < strWithX.Length; i++)
            {
                if (strWithX[i] == 'X')
                {
                    result += xValueStr[idxInXValueStr];
                    idxInXValueStr++;
                }
                else
                {
                    result += strWithX[i];
                }
            }

            return result;
        }

        string ApplyMaskFirstStar(string str, string mask)
        {
            var result = "";

            for (var i = 0; i < str.Length; i++)
            {
                var c = str[i];

                if (mask[i] != 'X')
                {
                    c = mask[i];
                }

                result += c;
            }

            return result;
        }

        string ApplyMaskSecondStar(string str, string mask)
        {
            var result = "";

            for (var i = 0; i < str.Length; i++)
            {
                var c = str[i];

                if (mask[i] != '0')
                {
                    c = mask[i];
                }

                result += c;
            }

            return result;
        }

        long GetSum(Dictionary<long, long> memory)
        {
            return memory.Sum(kvp => kvp.Value);
        }

        string ToBinary36(long n)
        {
            return ToBinary(n, 36);
        }

        string ToBinary(long n, int totalWidth)
        {
            return ToBinary(n).PadLeft(totalWidth, '0');
        }

        string ToBinary(long n)
        {
            return Convert.ToString(n, 2);
        }

        long ToDec(string bin)
        {
            return Convert.ToInt64(bin, 2);
        }

        #endregion
    }
}
