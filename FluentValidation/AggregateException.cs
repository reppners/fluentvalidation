using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidationNA
{
#if NET35
    /// <summary>
    /// A mimic of the AggregateException defined in .NET 4.0+ to support the return of multiple exceptions.  Since it is a mimic, it 
    /// is not defined for use outside of the scope of the FluentValidation project.
    /// </summary>

    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification="No plan to make this exception serializable")]
    public sealed class AggregateException : Exception
    {

        internal AggregateException(string message, IEnumerable<Exception> innerExceptions)
            : base(message, innerExceptions.First())
        {
            InnerExceptions = new List<Exception>(innerExceptions).AsReadOnly();
        }

        /// <summary>
        /// Gets a read-only collection of the <see cref="System.Exception"/> instances that caused the current exception.
        /// </summary>
        public ReadOnlyCollection<Exception> InnerExceptions { get; private set; }
    }
#endif
}
