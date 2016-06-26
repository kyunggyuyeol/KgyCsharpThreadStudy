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
            ThreadSample Foreground = new ThreadSample(10);
            ThreadSample background = new ThreadSample(20);

            Thread ThreadOne = new Thread(Foreground.CountNumbers);
            ThreadOne.Name = "Foreground";
            Thread ThreadTwo = new Thread(background.CountNumbers);
            ThreadTwo.Name = "Background";
            ThreadTwo.IsBackground = true;

            ThreadOne.Start();
            ThreadTwo.Start();
        }
    }

    class ThreadSample
    {
        private int _iterations;

        public ThreadSample(int _iterations)
        {
            this._iterations = _iterations;
        }

        public void CountNumbers()
        {
            for (int i = 0; i < _iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
            }
        }
    }
}
