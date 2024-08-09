using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._2
{
    // internallock提供了数学基本原子运算Increment,Decrement,Add，类比c++ atomic
    internal class Program
    {
        static void Main(string[] args)
        {
            Counter c = new Counter();
            c.Count = 0;
            var t1 = new Thread(()=>Test(c));
            var t2 = new Thread(()=>Test(c));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine(c.Count);
        }

        public static void Test(Counter c)
        {
            for (int i = 0; i < 100; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }
    }

    public class Counter
    {
        private object _lock = new object();
        private int _count;
        public int Count
        {
            get { return _count; }
            set {_count = value;}
        }
        public void Increment()
        {
            lock(_lock)
            {
                Count++;
            }
            //Interlocked.Increment(ref _count);
        }
        public void Decrement()
        {
            lock(_lock)
            {
                Count--;
            }
            //Interlocked.Decrement(ref _count);
        }
    }
}