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
            Console.WriteLine("current thread priority: {0}", Thread.CurrentThread.Priority);
            Console.WriteLine("Running on all Cores available");

            RunThreads();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Running on a Single core");
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1); //단일코어 실행

            RunThreads();
        }

        public class ThreadSample
        {
            private bool _isStopped = false;

            public void Stop()
            {
                _isStopped = true;
            }

            public void CountNumbers()
            {
                long counter = 0;
                while (!_isStopped)
                {
                    counter++;
                }

                Console.WriteLine("{0} with {1,11} priority" + "has a count = {2,13}", Thread.CurrentThread.Name, Thread.CurrentThread.Priority, counter.ToString("N0"));
            }
        }

        static void RunThreads()
        {
            ThreadSample sample = new ThreadSample();

            Thread threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";

            Thread threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";

            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;

            threadOne.Start();
            threadTwo.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }

    }
        
}
