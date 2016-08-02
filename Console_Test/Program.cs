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
            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOperation1(token));
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }

            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOperation2(token));
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }

            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOperation3(token));
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }

            Thread.Sleep(TimeSpan.FromSeconds(2));

        }

        static void AsyncOperation1(CancellationToken token)
        {
            Console.WriteLine("Start thr first task");
            for (int i = 0; i < 5; i++)
            {
                if(token.IsCancellationRequested)
                {
                    Console.WriteLine("The first task gas been canceled.");
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("The first task has completed succesfully");
        }

        static void AsyncOperation2(CancellationToken token)
        {
            try
            {
                Console.WriteLine("Starting the second task");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                }
                Console.WriteLine("The second task has completed successfully");
            }
            catch
            {
                Console.WriteLine("The second task has been calceled.");
            }
        }

        private static void AsyncOperation3(CancellationToken token)
        {
            bool cancellationFlag = false;

            token.Register(() => cancellationFlag = true);
            Console.WriteLine("Starting thre third task");
            for (int i = 0; i < 5; i++)
            {
                if(cancellationFlag)
                {
                    Console.WriteLine("The third task has been canceled.");
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("The third task has completed succesfully");
        }

    }
}