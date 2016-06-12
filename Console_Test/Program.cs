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
            Thread th = new Thread(printNumber);
            th.Start();
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
    }
}
