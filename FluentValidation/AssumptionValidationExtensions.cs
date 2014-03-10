using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FluentValidation
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
        public static AssumptionValidation Assumptions(this IValidation validation)
        {
            return null;
        }

        /// <summary>
        /// Performs the validation for the current assumptions.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <returns>A <c>null</c> placeholder.</returns>
        public static IValidation Check(this AssumptionValidation validation)
        {
            if (validation != null)
            {
                var exception = validation.BaseCheck();

                validation.Return();

                if (exception != null)
                {
                    throw exception;
                }
            }

            return null;
        }

        /// <summary>
        /// Allows a check to pass if all validation on the left OR all validations on the right pass.
        /// </summary>
        /// <param name="validation">The validation currently being checked.</param>
        /// <returns>The current object validation to check against.</returns>
        public static AssumptionValidation Or(this AssumptionValidation validation)
        {
            if (validation == null) validation = AssumptionValidation.Borrow();

            validation.NewClause();

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated result is not null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is <c>null</c>.</exception>
        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, Func<T> value)
            where T : class
        {
            return IsTrue(validation, () => value() != null);
        }

        /// <summary>
        /// Checks that the evaluated result is not null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is <c>null</c>.</exception>
        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, Func<T?> value)
            where T : struct
        {
            return IsTrue(validation, () => value().HasValue);
        }

        /// <summary>
        /// Checks that the evaluated result is null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not <c>null</c>.</exception>
        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, Func<T> value)
            where T : class
        {
            return IsTrue(validation, () => value() == null);
        }

        /// <summary>
        /// Checks that the evaluated result is null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not <c>null</c>.</exception>
        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, Func<T?> value)
            where T : struct
        {
            return IsTrue(validation, () => !value().HasValue);
        }

        /// <summary>
        /// Checks that the evaluated string is not null or empty.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The string to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is <c>null</c> or empty.</exception>
        public static AssumptionValidation IsNotNullOrEmpty(this AssumptionValidation validation, Func<string> value)
        {
            return IsTrue(validation, () => !String.IsNullOrEmpty(value()));
        }

        /// <summary>
        /// Checks that the evaluated enumerable is not null or empty.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="enumerable">The enumerable to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is <c>null</c> or empty.</exception>
        public static AssumptionValidation IsNotNullOrEmpty<T>( this AssumptionValidation validation, Func<T> enumerable)
            where T : IEnumerable
        {
            return IsTrue(validation, () =>
            {
                var value = enumerable();

                return value != null && !value.IsEnumEmpty();
            });
        }

        /// <summary>
        /// Checks that the evaluated object is of type T.
        /// </summary>
        /// <typeparam name="T">The type that value must be a type of.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The object to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not of type T.</exception>
        public static AssumptionValidation IsType<T>(this AssumptionValidation validation, Func<object> value)
        {
            return IsTrue(validation, () => value() is T);
        }

        /// <summary>
        /// Checks that the evaluated bool is false.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to false, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not false.</exception>
        public static AssumptionValidation IsFalse(this AssumptionValidation validation, Func<bool> condition, string message = null)
        {
            return IsTrue(validation, () => !condition());
        }

        /// <summary>
        /// Checks that the evaluated bool is true.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not true.</exception>
        public static AssumptionValidation IsTrue(this AssumptionValidation validation, Func<bool> condition, string message = null)
        {
            if (validation.AcceptCall())
            {
                if (!condition())
                {
                    Fail(ref validation, message);
                }
            }

            return validation;
        }

        /// <summary>
        /// Checks that the evaluated service object is not null.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="service">The service to verify existance.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the service is null.</exception>
        public static AssumptionValidation ServicePresent<T>(this AssumptionValidation validation, Func<T> service)
        {
            if (validation.AcceptCall())
            {
                if (service() == null)
                {
#if NET35
                    var coreType = typeof(T);
#else
                    var coreType = Helpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));
#endif

                    Fail(ref validation, Format(Strings.ServiceMissing, coreType.FullName));
                }
            }

            return validation;
        }


        static void Fail(ref AssumptionValidation validation, string message)
        {
            var exeption = new InternalErrorException(message);

            if (validation == null) validation = AssumptionValidation.Borrow();

            validation.SetException(exeption);
        }

        private static string Format(string format, params object[] arguments)
        {
            return Helpers.FormatError(format, arguments);
        }

    }


}
