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
            //th.Join();
            Console.WriteLine("Thread Completed");
            //printNumber();
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
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                Thread.Sleep(2000);
                Console.WriteLine(i);
            }
        }
    }
}
