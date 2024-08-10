using System;
using System.Collections;
using System.Threading;
using static System.Threading.Thread;
namespace Sec3._3
{
    internal class Program
    {
        private static void AsyncOperation(object state)
        {
            Console.WriteLine($"operation state {state ?? "(null)"}");
            Console.WriteLine($"worker thread id {CurrentThread.ManagedThreadId}");
            Sleep( 1000 );
        }
        // 闭包练习
        public static void Test()
        {
            List<UserModel> users = new List<UserModel>
            {
                new UserModel {Name="aaa", Age=11},
                new UserModel {Name="bbb", Age=22},
                new UserModel {Name="ccc", Age=33},
            };
            for(int i = 0; i < 3; i++)
            {
                var u = users[i];
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Sleep(1000);
                    // i在外部最终是3
                    //Console.WriteLine(i);
                    //UserModel u = users[i];
                    Console.WriteLine(u.Name);
                });
            }
        }
        static void Main(string[] args)
        {
            Test();
            Sleep(5000);
            /*int a = 5;
            const int x = 1, y = 2;
            const string lambdaState = "lambdastate2";

            ThreadPool.QueueUserWorkItem(AsyncOperation);
            Sleep( 1000 );

            ThreadPool.QueueUserWorkItem(AsyncOperation,"asyncState");
            Sleep( 1000 );

            a = x + y;
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine($"state is {state}");
                Console.WriteLine($"worker thread id is {CurrentThread.ManagedThreadId}");
                Sleep(1000);
            }, "lambda state");

            // 匿名函数捕获外部变量，就是闭包
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Console.WriteLine($"operation state {a}, {lambdaState}");
                Console.WriteLine($"worker thread id is {CurrentThread.ManagedThreadId}");
                Sleep(1000);
            }, "lambda state");*/
        }
    }


    public class UserModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


}