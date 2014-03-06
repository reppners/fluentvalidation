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
                ComplexFunction("", " ", 5, null, "A");
            }
        }

        static void ComplexFunction(
            string cannotBeNullStr,
            string cannotBeEmptyStr,
            int mustBeGreaterThanZero,
            double? mustBeLessThanZeroOrNull,
            string mustBeAllCaps)
        {
            Validate.Argument(cannotBeNullStr, "cannotBeNullStr").IsNotNull().Check()
                    .Argument(cannotBeEmptyStr, "cannotBeEmptyStr").IsNotNullOrEmpty().Check()
                    .Argument(mustBeGreaterThanZero, "mustBeGreaterThanZero").Range(v => v > 0).Check()
                    .Argument(mustBeLessThanZeroOrNull, "mustBeLessThanZeroOrNull").Range(v => v < 0).Or().IsNull().Check();
                    //.Argument(mustBeAllCaps, "mustBeAllCaps").That(s => s.ToUpper() == s, "Value must be all caps").Check();

        }
    }
}
