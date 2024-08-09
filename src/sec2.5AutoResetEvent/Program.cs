using System;
using System.Threading;
using static System.Threading.Thread;
namespace sec2._5AutoResetEvent
{
    internal class Program
    {
        //AutoResetEvent 的工作方式可以类比于一个门闩或红绿灯。
        //线程可以通过等待门闩打开（WaitOne）来阻塞自己的执行，
        //而其他线程则可以通过设置门闩打开（Set）来唤醒等待的线程
        static AutoResetEvent producerEvent = new AutoResetEvent(false);
        static AutoResetEvent consumerEvent = new AutoResetEvent(true);
        static int food = 0;

        static void Produce()
        {
            for(int i=0;i<5;i++)
            {
                //consumerEvent.WaitOne();
                food = i;
                Console.WriteLine($"Produce {i}");
                Sleep(TimeSpan.FromSeconds(i));
                producerEvent.Set();
            }
        }

        static void Consume()
        {
            for(int i=0;i<5;i++)
            {
                producerEvent.WaitOne();
                Console.WriteLine($"consume {i}");
                //consumerEvent.Set();
            }
        }
        static void Main(string[] args)
        {
            var producer = new Thread(Produce);
            var consumer = new Thread(Consume);

            producer.Start();
            consumer.Start();

            producer.Join();
            consumer.Join();
        }
    }
}