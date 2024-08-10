using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._8Barrier
{
    internal class Program
    {
        static Barrier _barrier = new Barrier(2, 
            b => Console.WriteLine($"end of phase {b.CurrentPhaseNumber + 1}"));

        static void PickHero(string name,int seconds)
        {
            for(int i=0;i<5;i++)
            {
                Sleep(seconds);
                Console.WriteLine($"{name} pick , hero {i}");
                _barrier.SignalAndWait();
            }
        }
        static void Main(string[] args)
        {
            var t1 = new Thread(() => { PickHero("rng", 2000); });
            var t2 = new Thread(() => { PickHero("edg", 5000); });
            // barrier设置为2,只能控制两个线程
            //var t3 = new Thread(() => { PickHero("fpx", 5000); });

            t1.Start();
            t2.Start();
            //t3.Start();
        }
    }
}