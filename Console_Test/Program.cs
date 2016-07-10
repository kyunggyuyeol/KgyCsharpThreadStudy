using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Console_Test
{
    class Program
    {
        static void Main(string[] args) //Barrier 생성자 사용
        {
            new Thread(read) { IsBackground = true }.Start();
            new Thread(read) { IsBackground = true }.Start();
            new Thread(read) { IsBackground = true }.Start();

            new Thread(() => Write("thread1")) { IsBackground = true }.Start();
            new Thread(() => Write("thread2")) { IsBackground = true }.Start();
            Thread.Sleep(TimeSpan.FromSeconds(30));

        }

        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _items = new Dictionary<int, int>();

        static void read()
        {
            Console.WriteLine("Reading contents of a Dictionary");
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var key_in in _items.Keys)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }

        static void Write(string ThreadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(250);
                    _rw.EnterUpgradeableReadLock();
                    if (!_items.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _items[newKey] = 1;
                            Console.WriteLine("New key {0} is added to a dictionary by a  {1}", newKey, ThreadName);
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }
    }
}