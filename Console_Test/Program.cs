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
            for (int i = 1; i < 7; i++)
            {
                string threadName = "thread : " + i.ToString();
                int secondsToWait = 2 + 2 * i;

                Thread t = new Thread(() => AccessDatabase(threadName, secondsToWait));
                t.Start(); 
            }
        }

        static SemaphoreSlim _semaphore = new SemaphoreSlim(4); // 동시스레드 갯수 지정

        static void AccessDatabase(string Name, int seconds)
        {
            Console.WriteLine("{0} waits to access a database", Name);
            _semaphore.Wait();
            Console.WriteLine("{0} was granted an access to a databses", Name);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("{0} is completed", Name);
            _semaphore.Release();
        }
    }
}