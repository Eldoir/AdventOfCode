namespace AdventOfCode
{
    class Problem_2025_2 : Problem2
    {
        public override long GetFirstStar()
        {
            return RunAlgo(Text, 2);
        }

        public override long GetSecondStar()
        {
            return RunAlgo(Text, 10);
        }

        private static long RunAlgo(string text, int maxRepetitions)
        {
            Range[] ranges = GetRanges(text);
            long sum = 0;

            foreach (Range range in ranges)
            {
                for (long i = range.Start; i <= range.End; i++)
                {
                    if (IsInvalid(i, maxRepetitions)) sum += i;
                }
            }

            return sum;
        }

        private static Range[] GetRanges(string text)
        {
            string[] split = text.Split(',');
            Range[] result = new Range[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                result[i] = GetRange(split[i]);
            }
            return result;

            static Range GetRange(string str)
            {
                string[] split = str.Split('-');
                return new Range(long.Parse(split[0]), long.Parse(split[1]));
            }
        }

        private static bool IsInvalid(long n, int maxRepetitions)
        {
            string s = n.ToString();
            for (int i = 1; i <= s.Length / 2; i++) // i = chunk size
            {
                if (s.Length % i != 0) continue; // string can't be split in chunks with that size
                
                string baseChunk = s.Substring(0, i);
                int j = i;
                int repetitions = 1;
                while (j < s.Length && repetitions <= maxRepetitions)
                {
                    string chunk = s.Substring(j, i);
                    if (chunk != baseChunk) break;
                    j += i;
                    repetitions++;
                }
                if (j == s.Length && repetitions <= maxRepetitions) return true;
            }
            return false;
        }

        private record struct Range(long Start, long End);

        protected override Test[] TestsFirstStar =>
        [
            new Test("example", 1227775554),
            new Test("11-22,95-115", 132, Name: "11,22,99"),
            new Test("998-1012", 1010, Name: "1010")
        ];

        protected override Test[] TestsSecondStar =>
        [
            new Test("example", 4174379265),
            new Test("11-22,95-115", 243, Name: "11,22,99,111"),
            new Test("998-1012", 2009, Name: "999,1010")
        ];
    }
}
