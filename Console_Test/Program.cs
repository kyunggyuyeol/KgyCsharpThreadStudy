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
            ThreadSample Foreground = new ThreadSample(10);//객체 생성시 값 받아 쓰려면 이렇게 사용해야한다.
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
        private readonly int _iterations;//읽기전용

        public ThreadSample(int _iterations)// 클래스와 같은 이름으로 만듬
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
