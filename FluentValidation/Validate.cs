using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// The starting location for the Fluent Validation API.
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Begins a new Argument validation.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="value">The value of the argument being validated.</param>
        /// <param name="parameterName">The name of the parameter being validated. Optional.</param>
        /// <returns>A new <see cref="ArgumentValidation{TArg}"/> instance.</returns>
        public static ArgumentValidation<TArg> Argument<TArg>([ValidatedNotNull] TArg value, string parameterName)
        {
            return ArgumentValidation<TArg>.Borrow(parameterName, value);
        }

        /// <summary>
        /// Begins a new Argument validation.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="value">The value of the argument being validated.</param>
        /// <returns>A new <see cref="ArgumentValidation{TArg}"/> instance.</returns>
        public static ArgumentValidation<TArg> Argument<TArg>([ValidatedNotNull] TArg value)
        {
            return ArgumentValidation<TArg>.Borrow(null, value);
        }

        /// <summary>
        /// Begins a new State validation.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="objectToValidate">The object to be validated.</param>
        /// <returns>A new <see cref="StateValidation{T}"/> instance.</returns>
        public static StateValidation<T> State<T>(T objectToValidate)
        {
            return StateValidation<T>.Borrow(objectToValidate);
        }

        /// <summary>
        /// Begins a new Assumptions validation.
        /// </summary>
        /// <returns>A null place holder.</returns>
        public static AssumptionValidation Assumptions()
        {
            return null;
        }
    }


}
