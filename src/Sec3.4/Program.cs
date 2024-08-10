using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using static System.Threading.Thread;
namespace Sec3._4
{
    internal class Program
    {
        public static void UseThreads(int number)
        {
            using(var countdown = new CountdownEvent(number))
            {
                Console.WriteLine("creating threads");
                for(int i = 0; i < number; i++)
                {
                    var t = new Thread(() =>
                    {
                        //Console.WriteLine($"{CurrentThread.ManagedThreadId},");
                        Sleep(1000);
                        countdown.Signal();
                    });
                    t.Start();
                }
                countdown.Wait();
            }
        }

        public static void UseThreadPool(int number)
        {
            using (var countDown = new CountdownEvent(number))
            {
                Console.WriteLine("pool creating threads");

                for (int i = 0; i < number;i++)
                {
                    ThreadPool.QueueUserWorkItem( _ =>
                    {
                        //Console.WriteLine($"{CurrentThread.ManagedThreadId},");
                        Sleep(1000);
                        countDown.Signal();
                    });
                }
                countDown.Wait();
            }
        }
        static void Main(string[] args)
        {
            const int number = 500;

            var sw = new Stopwatch();
            sw.Start();
            UseThreads(number);
            sw.Stop();
            Console.WriteLine($"{sw.ElapsedMilliseconds}");

            sw.Reset();
            sw.Start();
            UseThreadPool(number);
            sw.Stop();
            Console.WriteLine($"pool {sw.ElapsedMilliseconds}");

        }
    }
}