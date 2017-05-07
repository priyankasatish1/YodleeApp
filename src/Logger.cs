using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApiProj
{
    abstract class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(2000);
        }
    }
}
