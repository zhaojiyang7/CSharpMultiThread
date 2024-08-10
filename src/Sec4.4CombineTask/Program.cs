using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Threading.Thread;
namespace Sec4._4CombineTask
{
    internal class Program
    {
        public static int TaskMethod(string name,int sec)
        {
            Console.WriteLine($" task {name} is running on {CurrentThread.ManagedThreadId}, is in pool ${CurrentThread.IsThreadPoolThread}");
            Sleep(sec);
            return sec/1000;
        }
        static void Main(string[] args)
        {
            var t1 = new Task<int>(()=>TaskMethod("t1",3000));
            var t2 = new Task<int>(() => TaskMethod("t2", 2000));

            t1.ContinueWith(t => Console.WriteLine($"t1 res is {t.Result},thread id {CurrentThread.ManagedThreadId}"),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            t1.Start(); 
            t2.Start();

            Sleep(4000);

            Task continuation = t2.ContinueWith(t=> Console.WriteLine($"t2 res is {t.Result},thread id {CurrentThread.ManagedThreadId}"),
                TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
            continuation.GetAwaiter().OnCompleted(() => Console.WriteLine($"continue thread id {CurrentThread.ManagedThreadId}"));
        }
    }
}