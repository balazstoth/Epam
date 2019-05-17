using System;
using System.Diagnostics;

namespace Airports
{
    class Timer : IDisposable
    {
        private Stopwatch watcher;
        private string processName;

        public Timer(string processName)
        {
            this.processName = processName;
            watcher = new Stopwatch();
            watcher.Start();
        }

        public void Dispose()
        {
            watcher.Stop();
            Debug.WriteLine($"The {processName} process took {watcher.ElapsedMilliseconds} ms!");
        }
    }
}
