using System;
using System.Diagnostics;

namespace Airports
{
    class Timer : IDisposable
    {
        private Stopwatch sw;
        private string processName;

        public Timer(string processname)
        {
            processName = processname;
            sw = new Stopwatch();
            sw.Start();
        }

        public void Dispose()
        {
            sw.Stop();
            Debug.WriteLine($"The {processName} process took {sw.ElapsedMilliseconds} ms!");
        }
    }
}
