#nullable enable
using System;
using System.Diagnostics;

namespace AdventOfCode.Utils
{
    public sealed class StopwatchScope : IDisposable
    {
        public StopwatchScope(Action<TimeSpan>? onStop = null)
        {
            _sw = Stopwatch.StartNew();
            _onStop = onStop;
        }

        public void Dispose()
        {
            _sw.Stop();
            _onStop?.Invoke(_sw.Elapsed);
        }

        private readonly Stopwatch _sw;
        private readonly Action<TimeSpan>? _onStop;
    }

    /// <summary>
    /// When disposed, prints the milliseconds.
    /// </summary>
    public sealed class StopwatchMSScope : IDisposable
    {
        private readonly StopwatchScope _scope;

        public StopwatchMSScope()
        {
            _scope = new StopwatchScope(ts => Console.WriteLine($"{ts.TotalMilliseconds:F3}ms"));
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
