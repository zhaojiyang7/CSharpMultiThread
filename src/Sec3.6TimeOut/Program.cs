using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using static System.Threading.Thread;
namespace Sec3._6TimeOut
{
    internal class Program
    {
        public static void RunOperation(TimeSpan timeSpan)
        {
            using (var evt = new ManualResetEvent(false))
            using (var cts = new CancellationTokenSource())
            {
                var worker = ThreadPool.RegisterWaitForSingleObject(evt,
                    (state,isTimeout) => WorkerOperationWait(cts,isTimeout),
                    null,
                    timeSpan,
                    true);

                ThreadPool.QueueUserWorkItem(_ => WorkerOperation(cts.Token, evt));

                Sleep(timeSpan.Add(TimeSpan.FromSeconds(2)));
                worker.Unregister(evt);
            }
        }

        static void WorkerOperation(CancellationToken token, ManualResetEvent evt)
        {
            for(int  i = 0; i < 10; i++)
            {
                Console.WriteLine($"working {i}");
                if (token.IsCancellationRequested) return;
                Sleep(1000);
            }
            evt.Set();
        }

        static void WorkerOperationWait(CancellationTokenSource cts,bool isTimeout)
        {
            if(isTimeout)
            {
                cts.Cancel();
                Console.WriteLine("timeout and cancel");
            }
            else
            {
                Console.WriteLine("succeed");
            }
        }
        static void Main(string[] args)
        {
            RunOperation(TimeSpan.FromSeconds(5));
            RunOperation(TimeSpan.FromSeconds(11));
        }
    }
}