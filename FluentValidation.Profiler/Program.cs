using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FluentValidation.Profiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);

            for (int i = 0; i < 10000; i++)
            {
                //SimpleArgumentTest();
                SimpleStateTest();
            }
        }

        static void SimpleArgumentTest()
        {
            Validate.Argument("string", "string").That(str => true, "msg");
        }

        static void SimpleStateTest()
        {
            Validate.State("test").IsNotDisposed( str => true ).Operation( str => true );
        }


    }
}
