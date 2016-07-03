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
            Console.WriteLine("Incorrect Counter");

            Counter c = new Counter();

            Thread t1 = new Thread(() => TestCounter(c));
            Thread t2 = new Thread(() => TestCounter(c));
            Thread t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("TotalCount :{0}", c.count);
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("DCorrect counter");

            counterNoLock c1 = new counterNoLock();

            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine("TotalCount :{0}", c1.Count);
        }

        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }

        class Counter : CounterBase
        {
            private int _count;

            public int count
            {
                get { return _count; }
            }

            public override void Increment()
            {
                _count++;
            }
            public override void Decrement()
            {
                _count--;
            }
        }

        class counterNoLock : CounterBase
        {
            private int _count;

            public int Count { get { return _count; } }

            public override void Increment()
            {
                Interlocked.Increment(ref _count);
            }

            public override void Decrement()
            { 
                Interlocked.Decrement(ref _count);
            }
        }

        abstract class CounterBase
        {
            public abstract void Increment();
            public abstract void Decrement();
        }
    }
}