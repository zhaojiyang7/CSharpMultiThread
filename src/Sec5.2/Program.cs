using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Threading.Thread;
namespace Sec5._2
{
    internal class Program
    {
        static Task AsynchronyWithTPL()
        {
            Task<string> t = GetInfoAsync("Task1");
            Task t2 = t.ContinueWith(task => Console.WriteLine(t.Result), TaskContinuationOptions.NotOnFaulted);
            Task t3 = t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);
            return Task.WhenAny(t2, t3);
        }
        static async Task AsynchronoyWithAwait()
        {
            try
            {
                string result = await GetInfoAsync("task2");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        static async Task<string> GetInfoAsync(string name)
        {
            await Task.Delay(2000);
            throw new Exception("boom");
            return $"task {name} is {CurrentThread.ManagedThreadId}";
        }
        static void Main(string[] args)
        {
            Task t1=AsynchronyWithTPL();
            t1.Wait();
            Task t2 = AsynchronoyWithAwait();
            t2.Wait();
        }
    }
}