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
            string aa1 = "";
            Thread t1 = new Thread(() => value(out aa1));
            t1.Start();
            t1.Join();

            Console.WriteLine(aa1);
        }

        static void value(out string value1)
        {
            value1 = "asdf";
        }

    }
}