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
            RunOperations(TimeSpan.FromSeconds(5));
            RunOperations(TimeSpan.FromSeconds(7));
        }
        static void RunOperations(TimeSpan workerOperaionTimeout)
        {
            using (var evt = new ManualResetEvent(false))
            using (var cts = new CancellationTokenSource())
            {
                Console.WriteLine("Registering timeout operations...");
                var worker = ThreadPool.RegisterWaitForSingleObject(evt, (state, isTimedOut) => workerOperationWait(cts, isTimedOut), null, workerOperaionTimeout, true);
                Console.WriteLine("Starting long running operation....");

                ThreadPool.QueueUserWorkItem(_ => workerOperation(cts.Token, evt));

                Thread.Sleep(workerOperaionTimeout.Add(TimeSpan.FromSeconds(2)));
                worker.Unregister(evt);
            }
            
        }

        static void workerOperation(CancellationToken token, ManualResetEvent evt)
        {
            for (int i = 0; i < 6; i++)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            evt.Set();
        }

        static void workerOperationWait(CancellationTokenSource cts, bool isTimedOut)
        {
            if(isTimedOut)
            {
                cts.Cancel();
                Console.WriteLine("Worker operation timed out and was canceled.");
            }
            else
            {
                Console.WriteLine("Worker operation succeded.");
            }
        }

    }
}