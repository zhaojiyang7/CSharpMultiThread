using System;
using System.Threading;
using static System.Threading.Thread;
namespace Sec1._12Exception
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var t1 = new Thread(() => Test());
            t1.Start();
            try
            {
                var t2 = new Thread(() => BadTest());
                t2.Start();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void BadTest()
        {
            Console.WriteLine("BadTest");
            throw new Exception("BadTest");
        }

        public static void Test()
        {
            try
            {
                Console.WriteLine("Test");
                throw new Exception("Test");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}