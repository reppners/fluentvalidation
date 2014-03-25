using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// A place holder for subsequent validation checks.  Provides support for the Fluent Validation library and not intended to be used directly. 
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification="It has to be an empty interface")]
    public interface IValidation { }
}
