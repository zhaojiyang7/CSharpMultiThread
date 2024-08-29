using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using static System.Threading.Thread;
namespace Sec5._3
{
    internal class Program
    {
        static async Task AsyncProcessing()
        {
            Func<string,Task<string>> asyncLambda = async name => {
                await Task.Delay(2000);
                return $"task {name} running on {CurrentThread.ManagedThreadId}";
            };
            string result = await asyncLambda("zjy");
            Console.WriteLine(result);
        }
        static async Task<string> GetInfoAsync(string name)
        {
            Console.WriteLine($"Task {name} started");
            await Task.Delay(2000);
            return $"task {name} is runnning";
        }
        static async Task Test()
        {
            var res = await GetInfoAsync("niuniuniu");
            Console.WriteLine(res);
            res = await GetInfoAsync("zjy");
            Console.WriteLine(res);
        }
        static void Main(string[] args)
        {
            /*            var t = AsyncProcessing();
                        t.Wait();*/
            var t = Test();
            t.Wait();
        }
    }
}