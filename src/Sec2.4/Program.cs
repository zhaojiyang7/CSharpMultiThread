using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._4
{
    internal class Program
    {
        // mutex只允许一个线程访问，semaphoreSlim时计数信号量
        static SemaphoreSlim _semaphore = new SemaphoreSlim(4);
        static void Eat(string name,int seconds)
        {
            Console.WriteLine($"{name} is waiting");
            _semaphore.Wait();
            Console.WriteLine($"{name} is eating");
            Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine($"{name} ok");
            _semaphore.Release();
        }

        static void Main(string[] args)
        {
            for(int i=0;i<6;i++)
            {
                string name = "People" + i.ToString();
                var t = new Thread(() => { Eat(name,i); });
                t.Start();
            }
        }
    }
}