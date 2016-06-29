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
        static void Main(string[] args)
        {
            Thread t = new Thread(FaultyThread);
            t.Start();
            t.Join();

            Console.WriteLine("--------------------------------------------------");
            try
            {
                t = new Thread(badFaultyThread);
                t.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine("We wont't get here!");
            }
        }

        static void badFaultyThread()
        {
            Console.WriteLine("Starting a faulty thread...");
            Thread.Sleep(2000);
            throw new Exception("Boom!");
        }

        static void FaultyThread()
        {
            try
            {
                Console.WriteLine("Starting a fault thread");
                Thread.Sleep(1000);
                throw new Exception("Boom!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Handled: {0}", ex.Message);
            }
        }
    }
}