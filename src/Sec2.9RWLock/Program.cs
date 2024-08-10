using System;
using System.Collections;
using System.Threading;
using static System.Threading.Thread;
namespace Sec2._9RWLock
{
    internal class Program
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<string, int> _items = new Dictionary<string, int>();

        static void Read(string name)
        {
            Console.WriteLine("take item");
            while(true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach(var key in _items.Keys)
                    {
                        Console.WriteLine(name + "take " +key);
                        Sleep(100);
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
            Console.WriteLine("take item end");

        }

        static void Write(string Name)
        {
            while(true)
            {
                try
                {
                    string newItemName = "item" + new Random().Next(100).ToString();
                    _rw.EnterUpgradeableReadLock();
                    if(!_items.ContainsKey(newItemName))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _items[newItemName] = 1;
                            Console.WriteLine($"{Name} add {newItemName}");
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Sleep(100);
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }
        static void Main(string[] args)
        {
            new Thread(() => { Read("player1"); }) { IsBackground = true}.Start();
            new Thread(() => { Read("player2"); }) { IsBackground = true }.Start();
            new Thread(() => { Read("player3"); }) { IsBackground = true }.Start();
            new Thread(()=> { Write("business man"); }) { IsBackground = true }.Start();
            new Thread(()=> { Write("hotel man"); }) { IsBackground = true }.Start();

            Sleep(10000);
        }
    }
}