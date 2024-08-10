using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._6ManualResetEventSlim
{
    internal class Program
    {
        // 一个大门，调用set就会打开，reset就关闭
        static ManualResetEventSlim _mainEvent = new ManualResetEventSlim(false);
        static void ToEat(string threadName, int seconds)
        {
            Console.WriteLine($"{threadName} begin to sleep");
            Sleep( seconds );
            Console.WriteLine($"{threadName} wait for gate open");
            _mainEvent.Wait();
            Console.WriteLine($"{threadName} eat");
        }
        static void Main(string[] args)
        {
            var t1 = new Thread(() => { ToEat("people1", 1000); });
            var t2 = new Thread(() => { ToEat("people2", 2000); });
            var t3 = new Thread(() => { ToEat("people3", 3000); });
            t1.Start();
            t2.Start();
            t3.Start();
            Console.WriteLine("gate open");
            _mainEvent.Set();
            Sleep(2000);
            _mainEvent.Reset();
            Sleep(5000);
            Console.WriteLine("gate close");
            _mainEvent.Set();
            Sleep(2000);
            _mainEvent.Reset();


        }
    }
}