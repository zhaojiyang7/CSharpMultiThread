using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using static System.Threading.Thread;
namespace Sec3._5Cancel
{
    internal class Program
    {
        static void AsyncOp1(CancellationToken cancellationToken)
        {
            Console.WriteLine("start first task");
            for(int i=0;i<5;i++)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("first is cancelled");
                    return;
                }
                Sleep(1000);
            }
        }

        static void AsyncOp2(CancellationToken cancellationToken) 
        {
            try
            {
                Console.WriteLine("start second task");
                for (int i = 0; i < 5; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("second is cancelled");
            }
        }

        static void AsyncOp3(CancellationToken cancellationToken)
        {
            bool cancelFlag = false;
            // 取消时回调
            cancellationToken.Register(() => { cancelFlag = true; });
            for(int i = 0; i < 5;i++)
            {
                if (cancelFlag)
                {
                    Console.WriteLine("third is canceled");
                    return;
                }
                Sleep(1000);
            }
            
        }
        static void Main(string[] args)
        {
            using(var cts= new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOp1(token));
                Sleep(2000);
                cts.Cancel();
            }

            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOp2(token));
                Sleep(2000);
                cts.Cancel();
            }

            using (var cts = new CancellationTokenSource())
            {
                CancellationToken token = cts.Token;
                ThreadPool.QueueUserWorkItem(_ => AsyncOp3(token));
                Sleep(2000);
                cts.Cancel();
            }

            Sleep(2000);

        }
    }
}