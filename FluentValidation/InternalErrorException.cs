using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{

    /// <summary>
    /// The exception that is thrown when an Assumption Check fails.
    /// </summary>
#if NET35
    [Serializable]
#endif
    public class InternalErrorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
        /// </summary>
        public InternalErrorException()
            : base(Strings.InternalExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
        /// </summary>
        /// <param name="message">An optional message that will replace the default exception message.</param>
        public InternalErrorException(string message)
            : base(message ?? Strings.InternalExceptionMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
        /// </summary>
        /// <param name="message">An optional message that will replace the default exception message.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public InternalErrorException(string message, Exception innerException)
            : base(message ?? Strings.InternalExceptionMessage, innerException)
        {
        }
    }
}
