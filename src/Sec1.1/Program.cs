using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec1._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var t1 = new Thread(PrintNumDelay);
            var t2 = new Thread(() => { Ab(t1); });
            t1.Priority = ThreadPriority.Lowest;
            t2.Priority = ThreadPriority.Highest;

            t2.Start();
            t1.Start();
            t1.Join();
            
            PrintNum();
        }

        private static void Ab(object? obj)
        {
            Console.ReadLine();
            (obj as Thread).Abort();
        }

        public static void PrintNum()
        {
            for(int i=0; i < 10; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine(CurrentThread.ThreadState);
            }
        }
        public static void PrintNumDelay()
        {
            Sleep(2000);
            for(int i=0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}