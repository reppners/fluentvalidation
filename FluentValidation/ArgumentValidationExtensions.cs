using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides checks for argument validations.
    /// </summary>
    public static class ArgumentValidationExtensions
    {

        /// <summary>
        /// Begins a new Argument validation.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">Extensions placeholder. Can be <c>null</c>.</param>
        /// <param name="value">The value of the argument being validated.</param>
        /// <param name="paramName">The name of the parameter being validated. Optional.</param>
        /// <returns>A new <see cref="ArgumentValidation{TArgType}"/> instance.</returns>
        public static ArgumentValidation<TArgType> Argument<TArgType>(this IValidation validation, TArgType value, string paramName = null)
        {
            return ArgumentValidation<TArgType>.Borrow(paramName, value);
        }


        /// <summary>
        /// Performs the validation for the current argument.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>A <c>null</c> placeholder.</returns>
        public static IValidation Check<TArgType>(this ArgumentValidation<TArgType> validation)
        {
            var exception = validation.BaseCheck();

            validation.Return();

            if (exception != null) throw exception;

            return null;
        }


        #region Null Values

        /// <summary>
        /// Checks that the argument is not null.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is <c>null</c>.</exception>
        public static ArgumentValidation<TArgType> IsNotNull<TArgType>(this ArgumentValidation<TArgType> validation)
            where TArgType : class
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue == null) validation.SetException(new ArgumentNullException(validation.ParameterName));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the argument is not null.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is <c>null</c>.</exception>
        public static ArgumentValidation<TArgType?> IsNotNull<TArgType>(this ArgumentValidation<TArgType?> validation)
            where TArgType : struct
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue == null) validation.SetException(new ArgumentNullException(validation.ParameterName));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the argument is null.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is not <c>null</c>.</exception>
        public static ArgumentValidation<TArgType> IsNull<TArgType>(this ArgumentValidation<TArgType> validation)
            where TArgType : class
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue != null)
                    validation.SetException(new ArgumentException(
                        Format(Strings.Argument_NotNullValue, typeof(TArgType).Name), validation.ParameterName));
            }
            
            return validation;
        }

        /// <summary>
        /// Checks that the argument is null.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is not <c>null</c>.</exception>
        public static ArgumentValidation<TArgType?> IsNull<TArgType>(this ArgumentValidation<TArgType?> validation)
            where TArgType : struct
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue != null)
                    validation.SetException(new ArgumentException(
                        Format(Strings.Argument_NotNullValue, typeof(TArgType).Name), validation.ParameterName));
            }

            return validation;
        }

        #endregion

        #region String Values

        /// <summary>
        /// Checks that the string argument is not null or empty.
        /// </summary>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during<see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the string argument is empty.</exception>
        public static ArgumentValidation<string> IsNotNullOrEmpty(this ArgumentValidation<string> validation)
        {
            if (validation.AcceptCall())
            {
                var argVal = validation.ArgumentValue;

                if (argVal == null)
                {
                    validation.SetException(new ArgumentNullException(validation.ParameterName));
                }
                else if (argVal.Length == 0)
                {
                    validation.SetException(new ArgumentException(Format(Strings.Argument_EmptyString), validation.ParameterName));
                }
            }
            
            return validation;
        }

        /// <summary>
        /// Checks that the string argument is not null, empty or only white space.
        /// </summary>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the string argument is empty or only white space.</exception>
        public static ArgumentValidation<string> IsNotNullOrWhiteSpace(this ArgumentValidation<string> validation)
        {
            if (validation.AcceptCall())
            {
                var argVal = validation.ArgumentValue;

                if (argVal == null)
                {
                    validation.SetException(new ArgumentNullException(validation.ParameterName));
                }
#if NET35
                else if (argVal.IsEmptyOrWhiteSpace())
#else
                else if (String.IsNullOrWhiteSpace(argVal))
#endif
                {
                    validation.SetException(new ArgumentException(Format(Strings.Argument_WhiteSpaceString), validation.ParameterName));
                }
            }

            return validation;
        }

        #endregion

        #region Enumerable Values

        /// <summary>
        /// Checks that the enumerable is not null and that it contains at least 1 element.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.  Must implement interface IEnumerable.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if the enumerable is empty.</exception>
        public static ArgumentValidation<TArgType> IsNotNullOrEmpty<TArgType>(this ArgumentValidation<TArgType> validation)
            where TArgType : class, IEnumerable
        {
            if (validation.AcceptCall())
            {
                var argVal = validation.ArgumentValue;

                if (argVal == null)
                {
                    validation.SetException(new ArgumentNullException(validation.ParameterName));

                    return validation;
                }
                else
                {
                    if (!argVal.IsEnumEmpty()) return validation;
                }

                validation.SetException(new ArgumentException(Format(
                    Strings.Argument_EmptyEnumerable, typeof(TArgType).Name), validation.ParameterName));
            }

            return validation;
        }

        #endregion

        #region General Values

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentOutOfRangeException"/> is thrown.  If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArgType> Range<TArgType>(
            this ArgumentValidation<TArgType> validation,
            Predicate<TArgType> condition,
            string message = null)
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue != null && !condition(validation.ArgumentValue))
                {
                    validation.SetException(new ArgumentOutOfRangeException(message));
                }
            }

            return validation;
        }

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentOutOfRangeException"/> is thrown.  If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="format"> A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArgType> Range<TArgType>(
            this ArgumentValidation<TArgType> validation,
            Predicate<TArgType> condition,
            string format,
            params object[] args)
        {
            return Range(validation, condition, Format(format, args));
        }

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentException"/> is thrown.  If the argument is null, this check is ignored.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArgType> That<TArgType>(
            this ArgumentValidation<TArgType> validation,
            Predicate<TArgType> condition,
            string message)
        {
            if (validation.AcceptCall())
            {
                if ( validation.ArgumentValue != null && !condition(validation.ArgumentValue))
                {
                    validation.SetException(new ArgumentException(message));
                }
            }

            return validation;
        }

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentException"/> is thrown.  If the argument is null, this check is ignored.
        /// </summary>
        /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="format"> A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArgType}(ArgumentValidation{TArgType})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArgType> That<TArgType>(
                 this ArgumentValidation<TArgType> validation,
                 Predicate<TArgType> condition,
                 string format,
                 params object[] args)
        {
            return That(validation, condition, Format(format, args));
        }

        #endregion

        private static string Format(string format, params object[] arguments)
        {
            return Helpers.FormatError(format, arguments);
        }

    }
}
