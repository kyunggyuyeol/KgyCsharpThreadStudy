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
            TaskMethod("Main Thread Task"); //thread pool 에서 실행 안함

            Task<int> task = CreateTask("Task 1"); //thread pool에 배치
            task.Start();
            int result = task.Result;
            Console.WriteLine("Result is: {0}", result);

            task = CreateTask("Task 2");
            task.RunSynchronously(); //task 옵션에 의해 메인스레드에서 실행
            result = task.Result;
            Console.WriteLine("Result is: {0}", result);

            task = CreateTask("Task 3");
            task.Start();

            while(!task.IsCompleted) //task1 과 동일하지만 task3가 끝날떄까지 상태를 출력한다.
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine("Result is : {0}", result);
        }

        static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }

        static int TaskMethod(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}", name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 42;
        }


    }
}
