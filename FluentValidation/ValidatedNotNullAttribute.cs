using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Indicates to Code Analysis that a method validates a particular parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    [Conditional("CODE_ANALYSIS")]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
