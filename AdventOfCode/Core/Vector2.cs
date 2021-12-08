
namespace AdventOfCode.Core
{
    class Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Zero => new Vector2(0f, 0f);
        public static Vector2 Up => new Vector2(0f, 1f);
        public static Vector2 Down => new Vector2(0f, -1f);
        public static Vector2 Left => new Vector2(-1f, 0f);
        public static Vector2 Right => new Vector2(1f, 0f);

        public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.x + v2.x, v1.y + v2.y);
        public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.x - v2.x, v1.y - v2.y);
        public static Vector2 operator *(Vector2 v1, float k) => new Vector2(v1.x * k, v1.y * k);
        public static Vector2 operator /(Vector2 v1, float k) => new Vector2(v1.x / k, v1.y / k);
    }
}
