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
        static void Main(string[] args) //ManualResetEventSlim 생성자 사용
        {
            Thread v1 = new Thread(() => TravelTrouhGates("Thread1", 5));
            Thread v2 = new Thread(() => TravelTrouhGates("Thread2", 6));
            Thread v3 = new Thread(() => TravelTrouhGates("Thread3", 12));
            v1.Start();
            v2.Start();
            v3.Start();

            Thread.Sleep(TimeSpan.FromSeconds(6));
            Console.WriteLine("Thre gates are now open!");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            _mainEvent.Reset();
            Console.WriteLine("The gates have been closed");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("Thre gates are now open for the second time!");
            _mainEvent.Set();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Thre gates have been closed!");
            _mainEvent.Reset();
        }

        static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);

        static void TravelTrouhGates(string threadName, int seconds)
        {
            Console.WriteLine("{0} falls to sleep", threadName);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));

            Console.WriteLine("{0} waits for the gates to Open!", threadName);
            _mainEvent.Wait();
            Console.WriteLine("{0} enters the gates!", threadName);
        }
    }
}