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
        {//태스크 사용 Study
            Task<int> task;

            try
            {
                task = Task.Run(() => TaskMethod("Task 1", 2));
                int result = task.Result;

                Console.WriteLine("result : {0}", task.Result);
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");

            try
            {
                task = Task.Run(() => TaskMethod("task2", 2));
                int result = task.GetAwaiter().GetResult();
                Console.WriteLine(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine();


            var t1 = new Task<int>(() => TaskMethod("Task3", 3));
            var t2 = new Task<int>(() => TaskMethod("Task4", 4));

            var complexTask = Task.WhenAll(t1, t2);
            var excptionHendler = complexTask.ContinueWith(t => Console.WriteLine("Excption caught: {0}", t.Exception), TaskContinuationOptions.OnlyOnFaulted);

            t1.Start();
            t2.Start();
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task {0} is runniung on a thread id {1}. is thread pool {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            throw new Exception("Boom!");

            return 42 * seconds;
        }
    }
}