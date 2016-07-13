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
            //스레드풀로 대리자를 호출하는 방법을 이해하기 위한 연습 코딩
            int threadid = 0;

            RunOnThreadPool poolDelegate = Test;

            Thread t1 = new Thread(() => Test(out threadid));
            t1.Start();
            t1.Join();

            Console.WriteLine("Thread id:{0}", threadid);
            IAsyncResult r = poolDelegate.BeginInvoke(out threadid, Callback, "a delegate asynchronus call");
            r.AsyncWaitHandle.WaitOne();

            string result = poolDelegate.EndInvoke(out threadid, r);
            Console.WriteLine("Thread pool worker thread id:{0}", threadid);
            Console.WriteLine(result);
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        private delegate string RunOnThreadPool(out int threadId);

        private static void Callback(IAsyncResult ar)
        {
            Console.WriteLine("Starting a CallBack...");
            Console.WriteLine("State passed to a callback: {0}", ar.AsyncState);
            Console.WriteLine("Is Thread Pool thread:{0}", Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Thread pool worker thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static string Test(out int threadId)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("Is thread pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            threadId = Thread.CurrentThread.ManagedThreadId;
            return string.Format("Thread pool worker thread id was:{0}", threadId);
        }


    }
}