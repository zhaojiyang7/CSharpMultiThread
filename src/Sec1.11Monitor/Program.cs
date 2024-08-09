using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec1._11Monitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var c = new Counter();
            var t1 = new Thread(() => { test(c); });
            var t2 = new Thread(() => { test2(c); });

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine(c.count);
        }
        public static void test2(Counter c)
        {
            for(int i = 0; i < 10; i++)
            {            
                c.Increment2();
            }
        }
        public static void test(Counter c)
        {
            for(int i = 0; i < 10; i++)
            {            
                c.Increment();
            }
        }
    }

    public class Counter
    {
        public int count;
        public object lock1 = new object();
        public object lock2 = new object();

        public void Increment()
        {
            if(Monitor.TryEnter(lock1, 100))

            {
                count++;
                if(Monitor.TryEnter(lock2, 100))
                {
                    Console.WriteLine("timeout1");
                    Sleep(100);
                }
            }
        }
        public void Increment2()
        {
                            if(Monitor.TryEnter(lock2, 100))

            {
                count++;
                if(Monitor.TryEnter(lock1, 100))
                {
                    Console.WriteLine("timeout2");

                    Sleep(100);
                }
            }
        }
    }
}