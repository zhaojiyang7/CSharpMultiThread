using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Threading.Thread;
namespace Sec4._7TaskCancel
{
    internal class Program
    {
        public static int TaskMethod(string name,int seconds, CancellationToken token)
        {
            Console.WriteLine($"task {name},running on {CurrentThread.ManagedThreadId}, is pool {CurrentThread.IsThreadPoolThread}");
            for(int i = 0; i < seconds; i++)
            {
                Sleep(1000);
                if (token.IsCancellationRequested) return -1;
            }
            return seconds;
        }
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var longTask = new Task<int>(()=>TaskMethod("t1",10,cts.Token), cts.Token);
            Console.WriteLine(longTask.Status);
            cts.Cancel();
            Console.WriteLine(longTask.Status);
            Console.WriteLine("t1 is canceled");

            cts = new CancellationTokenSource();
            longTask = new Task<int>(() => TaskMethod("t2", 10, cts.Token), cts.Token);
            longTask.Start();
            for(int i=0;i<5;i++)
            {
                Console.WriteLine(longTask.Status);
                Sleep(1000);
            }
            cts.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(longTask.Status);
                Sleep(1000);
            }
            Console.WriteLine(longTask.Result);
        }
    }
}