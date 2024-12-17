using AdventOfCode.Core;
using System;

namespace AdventOfCode
{
    class Problem_2020_11 : Problem
    {
        public override int Year => 2020;
        public override int Number => 11;

        public static class CellType
        {
            public const char None = '\0';
            public const char Floor = '.';
            public const char Empty = 'L';
            public const char Occupied = '#';
        }

        public delegate char Ruleset(char[,] map, int i, int j);

        public override void Run()
        {
            var originalMap = new char[Lines.Length, Lines[0].Length];

            for (var i = 0; i < Lines.Length; i++)
            {
                for (var j = 0; j < Lines[i].Length; j++)
                {
                    originalMap[i, j] = Lines[i][j];
                }
            }

            Console.WriteLine($"First star: {GetNbOccupiedSeatsAfterWholeProcess(originalMap, RulesetFirstStar)}");
            Console.WriteLine($"Second star: {GetNbOccupiedSeatsAfterWholeProcess(originalMap, RulesetSecondStar)}");
        }

        #region Methods

        int GetNbOccupiedSeatsAfterWholeProcess(char[,] originalMap, Ruleset ruleset)
        {
            var map = new char[originalMap.GetLength(0), originalMap.GetLength(1)];
            CopyValues(map, originalMap);

            while (true)
            {
                //Display(map);

                var newMap = DoOneStep(map, ruleset);

                if (AreEqual(map, newMap)) break;

                CopyValues(map, newMap);
            }

            return GetNbOccupiedSeats(map);
        }

        char[,] DoOneStep(char[,] map, Ruleset ruleset)
        {
            var result = new char[map.GetLength(0), map.GetLength(1)];

            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = ruleset(map, i, j);
                }
            }

            return result;
        }

        char RulesetFirstStar(char[,] map, int i, int j)
        {
            var nbOccupiedSeatsAround = GetNbOccupiedSeatsAdjacentTo(map, i, j);

            if (map[i, j] == CellType.Empty && nbOccupiedSeatsAround == 0)
            {
                return CellType.Occupied;
            }
            else if (map[i, j] == CellType.Occupied && nbOccupiedSeatsAround >= 4)
            {
                return CellType.Empty;
            }
            else
            {
                return map[i, j];
            }
        }

        char RulesetSecondStar(char[,] map, int i, int j)
        {
            var nbOccupiedSeatsInAllDirections = GetNbOccupiedSeatsInAllDirections(map, i, j);

            if (map[i, j] == CellType.Empty && nbOccupiedSeatsInAllDirections == 0)
            {
                return CellType.Occupied;
            }
            else if (map[i, j] == CellType.Occupied && nbOccupiedSeatsInAllDirections >= 5)
            {
                return CellType.Empty;
            }
            else
            {
                return map[i, j];
            }
        }

        int GetNbOccupiedSeatsInAllDirections(char[,] map, int i, int j)
        {
            var count = 0;

            var pos = new Vector2Int(i, j);

            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.UpLeftIdx)) count++;
            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.UpIdx)) count++;
            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.UpRightIdx)) count++;

            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.LeftIdx)) count++;
            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.RightIdx)) count++;

            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.DownLeftIdx)) count++;
            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.DownIdx)) count++;
            if (IsASeatOccupiedInDirection(map, pos, Vector2Int.DownRightIdx)) count++;

            return count;
        }

        bool IsASeatOccupiedInDirection(char[,] map, Vector2Int pos, Vector2Int dir)
        {
            pos += dir;

            while (TryGet(map, pos, out var val))
            {
                if (val != CellType.Floor) return val == CellType.Occupied;
                pos += dir;
            }

            return false;
        }

        int GetNbOccupiedSeatsAdjacentTo(char[,] map, int i, int j)
        {
            var count = 0;

            var pos = new Vector2Int(i, j);

            if (IsOccupied(map, pos, Vector2Int.UpLeftIdx)) count++;
            if (IsOccupied(map, pos, Vector2Int.UpIdx)) count++;
            if (IsOccupied(map, pos, Vector2Int.UpRightIdx)) count++;

            if (IsOccupied(map, pos, Vector2Int.LeftIdx)) count++;
            if (IsOccupied(map, pos, Vector2Int.RightIdx)) count++;

            if (IsOccupied(map, pos, Vector2Int.DownLeftIdx)) count++;
            if (IsOccupied(map, pos, Vector2Int.DownIdx)) count++;
            if (IsOccupied(map, pos, Vector2Int.DownRightIdx)) count++;

            return count;
        }

        bool IsOccupied(char[,] map, Vector2Int pos, Vector2Int dir) => IsOccupied(map, pos + dir);

        bool IsOccupied(char[,] map, Vector2Int pos) => IsOccupied(map, pos.x, pos.y);

        bool IsOccupied(char[,] map, int i, int j)
        {
            if (TryGet(map, i, j, out var val))
            {
                return val == CellType.Occupied;
            }

            return false;
        }

        bool TryGet(char[,] map, Vector2Int pos, out char val) => TryGet(map, pos.x, pos.y, out val);

        bool TryGet(char[,] map, int i, int j, out char val)
        {
            if (i < 0 || i >= map.GetLength(0) || j < 0 || j >= map.GetLength(1))
            {
                val = CellType.None;
                return false;
            }

            val = map[i, j];
            return true;
        }

        int GetNbOccupiedSeats(char[,] map)
        {
            var count = 0;

            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == CellType.Occupied) count++;
                }
            }

            return count;
        }

        void CopyValues(char[,] dst, char[,] src)
        {
            for (var i = 0; i < src.GetLength(0); i++)
            {
                for (var j = 0; j < src.GetLength(1); j++)
                {
                    dst[i, j] = src[i, j];
                }
            }
        }

        bool AreEqual(char[,] map1, char[,] map2)
        {
            for (var i = 0; i < map1.GetLength(0); i++)
            {
                for (var j = 0; j < map1.GetLength(1); j++)
                {
                    if (map1[i, j] != map2[i, j]) return false;
                }
            }

            return true;
        }

        void Display(char[,] map)
        {
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("------------");
        }

        #endregion
    }
}
