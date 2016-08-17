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
            Task t1 = new Task(() => TaskMethod("TASK 1"));
            Task t2 = new Task(() => TaskMethod("TASK 2"));
            t2.Start();
            t1.Start();

            Task.Run(() => TaskMethod("TASK 3"));
            Task.Factory.StartNew(() => TaskMethod("TASK 4"));
            Task.Factory.StartNew(() => TaskMethod("TASK 5"), TaskCreationOptions.LongRunning);
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        static void TaskMethod(string Name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread :{2}", Name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);

        }
    }
}