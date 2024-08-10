using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Threading.Thread;
namespace Sec4._9
{
    internal class Program
    {
        public static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine($"task {name},running on {CurrentThread.ManagedThreadId}, is pool {CurrentThread.IsThreadPoolThread}");
            Sleep(1000);
            return seconds;
        }
        static void Main(string[] args)
        {
            var t1 = new Task<int>(() => TaskMethod("task1", 3));
            var t2 = new Task<int>(() => TaskMethod("task2", 2));
            var whenAllTask = Task.WhenAll(t1, t2);

            whenAllTask.ContinueWith(t => Console.WriteLine($"{t1.Result},{t2.Result}"));

            t1.Start();
            t2.Start();

            Sleep(2000);
        }
    }
}