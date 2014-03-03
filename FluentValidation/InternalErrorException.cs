using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{

    /// <summary>
    /// The exception that is thrown when an Assumption Check fails.
    /// </summary>
    public class InternalErrorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InternalErrorException"/> class.
        /// </summary>
        /// <param name="message">An optional message that will replace the default exception message.</param>
        public InternalErrorException(string message = null)
            : base(message ?? Strings.InternalExceptionMessage)
        {
        }
    }
}
