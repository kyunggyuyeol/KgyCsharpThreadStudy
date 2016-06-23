using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread th = new Thread(PrintNumbersWithDelay);
            th.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            th.Abort();
            Console.WriteLine("A thread has been aborted");
            Thread t = new Thread(printNumber);
            t.Start();
            printNumber();
        }

        static void printNumber()
        {
            Console.WriteLine("Starting....");
            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        static void PrintNumbersWithDelay()
        {
            Console.WriteLine("Starting...");
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
        }
    }
}
