namespace Sec1._10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var c = new CounterLock();
            var t1 = new Thread(() => test(c));
            var t2 = new Thread(() => test(c));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            Console.WriteLine(c.count);
        }
        public static void test(CounterLock c)
        {
            for(int i=0; i < 1000000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }
    }
    public class Counter
    {
        public int count;
        public Counter()
        {
            count = 0;
        }
        
        public void Increment()
        {
            count++;
        }
        public void Decrement()
        {
            count--;
        }
    }
    public class CounterLock
    {
        public int count;
        private readonly object _lock = new Object();
        public CounterLock()
        {
            count = 0;
        }
        
        public void Increment()
        {
            lock(_lock)
            {
                count++;
            }
        }
        public void Decrement()
        {
            lock(_lock)
            {
                            count--;
            }
        }
    }
}