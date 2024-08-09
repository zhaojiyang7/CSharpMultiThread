using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._3Mutex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string mutexName = "mutex1";
            using(var mutex = new Mutex(false, mutexName))
            {
                // waitone按照请求顺序处理依次唤醒
                if(!mutex.WaitOne(50000,false))
                {
                    Console.WriteLine("second isrunning");
                }
                else
                {
                    Console.WriteLine("running");
                    Console.ReadLine();
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}