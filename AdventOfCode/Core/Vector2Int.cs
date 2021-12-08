using System;

namespace AdventOfCode.Core
{
    class Vector2Int
    {
        public int x;
        public int y;

        public Vector2Int() : this(0, 0) { }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2Int Zero => new Vector2Int(0, 0);

        public static Vector2Int Up => new Vector2Int(0, 1);
        public static Vector2Int Down => new Vector2Int(0, -1);
        public static Vector2Int Left => new Vector2Int(-1, 0);
        public static Vector2Int Right => new Vector2Int(1, 0);

        /// <summary>
        /// Alias for <see cref="Up"/>.
        /// </summary>
        public static Vector2Int North => Up;
        /// <summary>
        /// Alias for <see cref="Down"/>.
        /// </summary>
        public static Vector2Int South => Down;
        /// <summary>
        /// Alias for <see cref="Left"/>.
        /// </summary>
        public static Vector2Int West => Left;
        /// <summary>
        /// Alias for <see cref="Right"/>.
        /// </summary>
        public static Vector2Int East => Right;

        // For indexes, the reference is different: (0, 0) is at the top left and it goes to the bottom right
        public static Vector2Int UpIdx => new Vector2Int(-1, 0);
        public static Vector2Int DownIdx => new Vector2Int(1, 0);
        public static Vector2Int LeftIdx => new Vector2Int(0, -1);
        public static Vector2Int RightIdx => new Vector2Int(0, 1);
        public static Vector2Int UpLeftIdx => new Vector2Int(-1, -1);
        public static Vector2Int UpRightIdx => new Vector2Int(-1, 1);
        public static Vector2Int DownLeftIdx => new Vector2Int(1, -1);
        public static Vector2Int DownRightIdx => new Vector2Int(1, 1);

        public static Vector2Int operator +(Vector2Int v1, Vector2Int v2) => new Vector2Int(v1.x + v2.x, v1.y + v2.y);
        public static Vector2Int operator -(Vector2Int v1, Vector2Int v2) => new Vector2Int(v1.x - v2.x, v1.y - v2.y);
        public static Vector2Int operator *(Vector2Int v1, int k) => new Vector2Int(v1.x * k, v1.y * k);
        public static Vector2Int operator /(Vector2Int v1, int k) => new Vector2Int(v1.x / k, v1.y / k);

        public int GetManhattanDistanceFromZero() => GetManhattanDistanceFrom(Zero);

        public int GetManhattanDistanceFrom(Vector2Int other)
        {
            var delta = other - this;
            return Math.Abs(delta.x) + Math.Abs(delta.y);
        }

        public void Rotate(int degrees)
        {
            var rad = degrees * Mathf.Deg2Rad;
            var cos = (int)Math.Cos(rad);
            var sin = (int)Math.Sin(rad);
            var newX = cos * x - sin * y;
            var newY = sin * x + cos * y;
            x = newX; y = newY;
        }

        public void RotateAround(Vector2Int pivot, int degrees)
        {
            var pos = this - pivot;
            pos.Rotate(degrees);
            pos += pivot;
            x = pos.x; y = pos.y;
        }

        public override string ToString()
        {
            return $"({x},{y})";
        }
    }
}
