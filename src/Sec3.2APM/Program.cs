using System;
using System.Collections;
using System.Threading;
namespace Sec3._2APM
{
    internal class Program
    {
        private delegate string RunOnThreadPool(out int threadID);

        private static void Callbakc(IAsyncResult asyncResult)
        {
            Console.WriteLine("start callback");
            Console.WriteLine($"state passed to callback {asyncResult.AsyncState}");
            Console.WriteLine($"is thread pool {Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"thread pool worker {Thread.CurrentThread.ManagedThreadId}");
        }

        private static string Test(out int threadID) 
        {
            Thread.Sleep(2000);
            threadID = Thread.CurrentThread.ManagedThreadId;
            return $"worker threadid is {threadID}";
        }
        static void Main(string[] args)
        {
            //已经不用APM了 现在使用TAP
            //int threadID = 0;
            //RunOnThreadPool poolDelegate = Test;
            //var t = new Thread(() => { Test(out threadID); });
            //t.Start();
            //t.Join();
            //Console.WriteLine($"threadID is {threadID}");

            ////
            //IAsyncResult asyncResult = poolDelegate.BeginInvoke(out threadID, Callbakc, "a delegate async call");
            //asyncResult.AsyncWaitHandle.WaitOne();
            //string result = poolDelegate.EndInvoke(out threadID, asyncResult);
            //Console.WriteLine($"threadID is {threadID}");
            //Console.WriteLine(result);

            //Thread.Sleep(2000);
        }
    }
}