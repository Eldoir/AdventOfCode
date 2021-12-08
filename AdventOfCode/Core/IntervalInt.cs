
namespace AdventOfCode.Core
{
    class IntervalInt : Vector2Int
    {
        public IntervalInt() : base(0, 0) { }

        public IntervalInt(int min, int max) : base(min, max) { }

        /// <summary>
        /// Alias for <see cref="x"/>.
        /// </summary>
        public int min
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Alias for <see cref="y"/>.
        /// </summary>
        public int max
        {
            get { return y; }
            set { y = value; }
        }

        public override string ToString()
        {
            return $"[{min}-{max}]";
        }
    }
}
