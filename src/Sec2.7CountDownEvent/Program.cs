using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._7CountDownEvent
{
    internal class Program
    {
        // 等待多个操作都完成才可以
        static CountdownEvent _countDown = new CountdownEvent(2);
        static void Count(string message, int seconds)
        {
            Sleep(seconds);
            Console.WriteLine(message);
            _countDown.Signal();
        }
        static void Main(string[] args)
        {
            var t1 = new Thread(() => { Count("t1 complete", 2000); });
            var t2 = new Thread(() => { Count("t2 complete", 4000); });
            t1.Start();
            t2.Start();
            _countDown.Wait();
            Console.WriteLine("all complete");
            _countDown.Dispose();
        }
    }
}