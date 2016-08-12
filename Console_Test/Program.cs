using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Enter' to stop thre timer...");
            DateTime start = DateTime.Now;
            _timer = new Timer(_ => TimerOperation(start), null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));

            Thread.Sleep(TimeSpan.FromSeconds(6));

            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4));
            Console.ReadLine();
            _timer.Dispose();

        }

        static Timer _timer;
        static void TimerOperation(DateTime start)
        {
            TimeSpan elapsed = DateTime.Now - start;
            Console.WriteLine("{0} seconds from {1}. Timer thread pool thread id: {2}", elapsed.Seconds, start, Thread.CurrentThread.ManagedThreadId);
        }
    }
}