using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides state information regarding the argument currently being validated.
    /// </summary>
    /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
    public sealed class ArgumentValidation<TArgType> : Validation 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentValidation{TArgType}"/> class.
        /// </summary>
        /// <param name="paramName">The name of the Parameter being validated.</param>
        /// <param name="value">The value of the argument being validated.</param>
        public ArgumentValidation(string paramName, TArgType value)
        {
            ArgumentValue = value;
            ParameterName = paramName;
        }

        /// <summary>
        /// The name of the Parameter being validated.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// The value of the argument being validated.
        /// </summary>
        public TArgType ArgumentValue { get; private set; }
    }
}
