using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using static System.Threading.Thread;
namespace Sec4._2
{
    internal class Program
    {
        public static void TaskMethod(string name)
        {
            Console.WriteLine($" task {name} is running on {CurrentThread.ManagedThreadId}, is in pool ${CurrentThread.IsThreadPoolThread}");
        }
        static void Main(string[] args)
        {
            var t1 = new Task(() => { TaskMethod("task1"); });
            var t2 = new Task(() => { TaskMethod("task2"); });
            t1.Start();
            t2.Start();
            Task.Run(() => { TaskMethod("task3"); });
            Task.Factory.StartNew(() => { TaskMethod("task4"); });
            Task.Factory.StartNew(()=>TaskMethod("task5"),TaskCreationOptions.LongRunning);
            Sleep(1000);
        }
    }
}