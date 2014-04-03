using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// An exception that will be thrown at some point if Check() is not called on validations.
    /// </summary>
    [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public sealed class FailedToCheckValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCheckValidationException"/> class.
        /// </summary>
        public FailedToCheckValidationException() : base(Strings.Validation_NoCheck) { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCheckValidationException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public FailedToCheckValidationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedToCheckValidationException"/> class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException"> The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public FailedToCheckValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
