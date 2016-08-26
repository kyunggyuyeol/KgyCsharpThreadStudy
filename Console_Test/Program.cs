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

            var firstTask = new Task<int>(() => TaskMethod("First Task", 3));
            var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));

            //선행이 완료된 경우 실행
            firstTask.ContinueWith(t => Console.WriteLine("The First answer is {0}. Thread Id : {1} is thread pool Thread : {2}", 
                t.Result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread), TaskContinuationOptions.OnlyOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();

            //두 태스크가 끝날때까지 대기
            Thread.Sleep(TimeSpan.FromSeconds(4));
            
            //동기적 실행 설정 짧은 수명일떄 유용 스레드 풀보단 메인스레드 이용이 효율적일때 사용
            Task continuation = secondTask.ContinueWith(
                t => Console.WriteLine("The Second anser is {0}. Thread id {1}, is thread pool thread: {2}", 
                t.Result, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread), TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            continuation.GetAwaiter().OnCompleted(() => 
            Console.WriteLine("Continbuation task completed! thread id{0}, is thread pool thread :{1}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread));


            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("");

            //자식태스크 실행 부모태스크를 실행하는 동안에 자식태스크를 실행해야한다.
            firstTask = new Task<int>(() => { var innerTask = Task.Factory.StartNew(() => TaskMethod("Second Task", 5), TaskCreationOptions.AttachedToParent);
                innerTask.ContinueWith(t => TaskMethod("Third", 2), TaskContinuationOptions.AttachedToParent); return TaskMethod("First Task", 2);
            });

            firstTask.Start();

            while(!firstTask.IsCompleted)
            {
                Console.WriteLine(firstTask.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            Console.WriteLine(firstTask.Status);
            Thread.Sleep(TimeSpan.FromSeconds(10));

        }
        
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}", name,  Thread.CurrentThread.ManagedThreadId ,Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;
        }
    }
}