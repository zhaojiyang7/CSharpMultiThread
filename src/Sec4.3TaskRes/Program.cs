using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Threading.Thread;
namespace Sec4._3TaskRes
{
    internal class Program
    {
        public static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }
        public static int TaskMethod(string name)
        {
            Console.WriteLine($" task {name} is running on {CurrentThread.ManagedThreadId}, is in pool ${CurrentThread.IsThreadPoolThread}");
            return 5;
        }
        static void Main(string[] args)
        {
            Task<int> t1 = CreateTask("task1");
            t1.Start();
            int result = t1.Result;
            Console.WriteLine($"task1 {result}");

            var t2 = CreateTask("task2");
            t2.RunSynchronously();
            result = t2.Result;
            Console.WriteLine($"task2 {result}");

            var t3 = CreateTask("task3");
            Console.WriteLine(t3.Status);
            t3.Start();
        }
    }
}