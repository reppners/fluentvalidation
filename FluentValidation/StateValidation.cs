using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides state information regarding the object currently being validated.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    public sealed class StateValidation<T> : Validation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateValidation{T}"/> class.
        /// </summary>
        /// <param name="obj">The object to be validated.</param>
        public StateValidation(T obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            Object = obj;
        }
        
        /// <summary>
        /// The object being validated.
        /// </summary>
        public T Object { get; private set; }
    }
}
