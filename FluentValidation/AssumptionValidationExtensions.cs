using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidationNA
{
    /// <summary>
    /// Provides checks for assumption validations.
    /// </summary>
    public static class AssumptionValidationExtensions
    {
        /// <summary>
        /// Begins a new Assumptions validation.
        /// </summary>
        /// <param name="validation">Extensions placeholder. Can be <c>null</c>.</param>
        /// <returns>A null place holder.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validation", Justification = "validation is only a placeholder, but is required to get fluent validation to work as intended")]
        public static AssumptionValidation Assumptions(this IValidation validation)
        {
            return null;
        }

        internal static AssumptionValidation SetException(this AssumptionValidation validation, Exception ex)
        {
            if (validation == null) validation = new AssumptionValidation();

            validation.SetExceptionInternal(ex);

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is not null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is <c>null</c>.</exception>
        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, T value)
            where T : class
        {
            if (value == null)
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is not null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Must to allow Nullables to work")]
        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, T? value)
            where T : struct
        {
            if (!value.HasValue)
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not <c>null</c>.</exception>
        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, T value)
            where T : class
        {
            if (value != null)
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Must to allow Nullables to work")]
        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, T? value)
            where T : struct
        {
            if (value.HasValue)
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is not its default value.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is its default value.</exception>
        public static AssumptionValidation IsNotDefault<T>(this AssumptionValidation validation, T value)
            where T : struct
        {
            if (value.Equals(default(T)))
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is its default value.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not its default value.</exception>
        public static AssumptionValidation IsDefault<T>(this AssumptionValidation validation, T value)
            where T : struct
        {
            if (!value.Equals(default(T)))
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated string is not null or empty.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The string to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is <c>null</c> or empty.</exception>
        public static AssumptionValidation IsNotNullOrEmpty(this AssumptionValidation validation, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated enumerable is not null or empty.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="enumerable">The enumerable to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is <c>null</c> or empty.</exception>
        public static AssumptionValidation IsNotNullOrEmpty<T>(this AssumptionValidation validation, T enumerable)
            where T : IEnumerable
        {
            if( enumerable == null || enumerable.IsEnumEmpty())
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated object is of type T.  If value is null, no check is performed.
        /// </summary>
        /// <typeparam name="T">The type that value must be a type of.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The object to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not of type T.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="T is used as a type comparison.  The explicit requirement of specified T is intended")]
        public static AssumptionValidation IsType<T>(this AssumptionValidation validation, object value)
        {
            if (value == null) return validation;

            if( !(value is T))
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated bool is false.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to false, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not false.</exception>
        public static AssumptionValidation IsFalse(this AssumptionValidation validation, bool condition, string message)
        {
            if (condition)
            {
                return validation.SetException(new InternalErrorException(message));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated bool is false.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to false, or it will fail the validation.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not false.</exception>
        public static AssumptionValidation IsFalse(this AssumptionValidation validation, bool condition)
        {
            if (condition)
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }


        /// <summary>
        /// Checks that the evaluated bool is true.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not true.</exception>
        public static AssumptionValidation IsTrue(this AssumptionValidation validation, bool condition, string message)
        {
            if (!condition)
            {
                return validation.SetException(new InternalErrorException(message));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated bool is true.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the value is not true.</exception>
        public static AssumptionValidation IsTrue(this AssumptionValidation validation, bool condition)
        {
            if( !condition )
            {
                return validation.SetException(new InternalErrorException());
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated service object is not null.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="service">The service to verify existance.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown if the service is null.</exception>
        public static AssumptionValidation IsServicePresent<T>(this AssumptionValidation validation, T service)
        {
            if (service == null)
            {
#if NET35
                var coreType = typeof(T);
#else
                var coreType = Helpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));
#endif
                return validation.SetException(new InternalErrorException(Format(Strings.ServiceMissing, coreType.FullName)));
            }

            return validation;
        }

        private static string Format(string format, params object[] arguments)
        {
            return Helpers.FormatError(format, arguments);
        }

    }


}
