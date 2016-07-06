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
        static void Main(string[] args) //CountdownEvent 사용
        {
            Console.WriteLine("Starting two operations");
            Thread t1 = new Thread(() => PerformOperation("operation 1 is Completed", 4));
            Thread t2 = new Thread(() => PerformOperation("operation 2 is Completed", 8));

            t1.Start();
            t2.Start();
            _countdown.Wait();
            Console.WriteLine("Both operaions have been completed.");
            _countdown.Dispose();
        }

        static CountdownEvent _countdown = new CountdownEvent(2);

        static void PerformOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countdown.Signal();
        }
    }
}
