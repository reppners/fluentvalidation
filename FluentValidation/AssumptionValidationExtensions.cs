using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validation", Justification = "validation is only a placeholder, but is required to get fluent validation to work as intended")]
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
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => value() != null, null);
        }

        /// <summary>
        /// Checks that the evaluated result is not null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Must to allow Nullables to work")]
        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, Func<T?> value)
            where T : struct
        {
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => value().HasValue, null);
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
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => value() == null, null);
        }

        /// <summary>
        /// Checks that the evaluated result is null.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Must to allow Nullables to work")]
        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, Func<T?> value)
            where T : struct
        {
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => !value().HasValue, null);
        }

        /// <summary>
        /// Checks that the evaluated result is not its default value.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is its default value.</exception>
        public static AssumptionValidation IsNotDefault<T>(this AssumptionValidation validation, Func<T> value)
            where T : struct
        {
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => !Object.Equals(value(), default(T)), null);
        }

        /// <summary>
        /// Checks that the evaluated result is its default value.
        /// </summary>
        /// <typeparam name="T">The type to be evaluated.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The value to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not its default value.</exception>
        public static AssumptionValidation IsDefault<T>(this AssumptionValidation validation, Func<T> value)
            where T : struct
        {
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => Object.Equals(value(), default(T)), null);
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
            if (value == null) throw new ArgumentNullException("value");

            return IsTrueInternal(validation, () => !String.IsNullOrEmpty(value()), null);
        }

        /// <summary>
        /// Checks that the evaluated enumerable is not null or empty.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="enumerable">The enumerable to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is <c>null</c> or empty.</exception>
        public static AssumptionValidation IsNotNullOrEmpty<T>(this AssumptionValidation validation, Func<T> enumerable)
            where T : IEnumerable
        {
            if (enumerable == null) throw new ArgumentNullException("enumerable");

            return IsTrueInternal(validation,
                () =>
                {
                    var value = enumerable();

                    return value != null && !value.IsEnumEmpty();
                },
            null);
        }

        /// <summary>
        /// Checks that the evaluated object is of type T.  If value is null, no check is performed.
        /// </summary>
        /// <typeparam name="T">The type that value must be a type of.</typeparam>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="value">The object to be evaluated.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not of type T.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification="T is used as a type comparison.  The explicit requirement of specified T is intended")]
        public static AssumptionValidation IsType<T>(this AssumptionValidation validation, Func<object> value)
        {
            if (value == null) return validation;

            return IsTrueInternal(validation, () => value() is T, null);
        }

        /// <summary>
        /// Checks that the evaluated bool is false.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to false, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not false.</exception>
        public static AssumptionValidation IsFalse(this AssumptionValidation validation, Func<bool> condition, string message)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            return IsTrueInternal(validation, () => !condition(), message);
        }

        /// <summary>
        /// Checks that the evaluated bool is false.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to false, or it will fail the validation.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not false.</exception>
        public static AssumptionValidation IsFalse(this AssumptionValidation validation, Func<bool> condition)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            return IsTrueInternal(validation, () => !condition(), null);
        }


        /// <summary>
        /// Checks that the evaluated bool is true.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not true.</exception>
        public static AssumptionValidation IsTrue(this AssumptionValidation validation, Func<bool> condition, string message)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            return IsTrueInternal(validation, condition, message);
        }

        /// <summary>
        /// Checks that the evaluated bool is true.
        /// </summary>
        /// <param name="validation">The current assumptions that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <returns>The current assumptions that is being validated.</returns>
        /// <exception cref="InternalErrorException">Thrown during <see cref="Check(AssumptionValidation)"/> if the value is not true.</exception>
        public static AssumptionValidation IsTrue(this AssumptionValidation validation, Func<bool> condition)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            return IsTrueInternal(validation, condition, null);
        }

        static AssumptionValidation IsTrueInternal(AssumptionValidation validation, Func<bool> condition, string message)
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
        public static AssumptionValidation IsServicePresent<T>(this AssumptionValidation validation, Func<T> service)
        {
            if (service == null) throw new ArgumentNullException("service");

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
