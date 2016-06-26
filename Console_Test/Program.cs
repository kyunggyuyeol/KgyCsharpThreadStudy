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
            ThreadSample sample = new ThreadSample(10);

            Thread ThreadOne = new Thread(sample.CountNumbers);
            ThreadOne.Name = "threadOne";
            ThreadOne.Start();
            ThreadOne.Join();
            Console.WriteLine("------------------------------------------");

            Thread ThreadTwo = new Thread(Count);
            ThreadTwo.Name = "ThreadTwo";
            ThreadTwo.Start(8);
            ThreadTwo.Join();
            Console.WriteLine("------------------------------------------");

            Thread ThreadThree = new Thread(() => CountNumbers(12));
            ThreadThree.Name = "ThreadThree";
            ThreadThree.Start();
            ThreadThree.Join();
            Console.WriteLine("------------------------------------------");

            int i = 10;
            Thread ThreadFour = new Thread(() => PrintNumber(i));
            i = 20;
            Thread ThreadFive = new Thread(() => PrintNumber(i));
            ThreadFour.Start();
            ThreadFive.Start();

        }

        static void Count(object value)
        {
            CountNumbers((int)value);
        }

        static void CountNumbers(int value)
        {
            for (int i = 0; i < value; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} Prints {1}", Thread.CurrentThread.Name, i);
            }
        }

        static void PrintNumber(int number)
        {
            Console.WriteLine(number);
        }

        class ThreadSample
        {
            private readonly int _value;

            public ThreadSample(int value)
            {
                this._value = value;
            }


            public void CountNumbers()
            {
                for (int i = 0; i < _value; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                    Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
                }
            }
        }
    }
}
