﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">Extensions placeholder. Can be <c>null</c>.</param>
        /// <param name="value">The value of the argument being validated.</param>
        /// <param name="parameterName">The name of the parameter being validated. Optional.</param>
        /// <returns>A new <see cref="ArgumentValidation{TArg}"/> instance.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validation", Justification="validation is only a placeholder, but is required to get fluent validation to work as intended")]
        public static ArgumentValidation<TArg> Argument<TArg>(this IValidation validation, [ValidatedNotNull] TArg value, string parameterName)
        {
            return ArgumentValidation<TArg>.Borrow(parameterName, value);
        }

        /// <summary>
        /// Begins a new Argument validation.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">Extensions placeholder. Can be <c>null</c>.</param>
        /// <param name="value">The value of the argument being validated.</param>
        /// <returns>A new <see cref="ArgumentValidation{TArg}"/> instance.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validation", Justification = "validation is only a placeholder, but is required to get fluent validation to work as intended")]
        public static ArgumentValidation<TArg> Argument<TArg>(this IValidation validation, [ValidatedNotNull] TArg value)
        {
            return ArgumentValidation<TArg>.Borrow(null, value);
        }

        /// <summary>
        /// Performs the validation for the current argument.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>A <c>null</c> placeholder.</returns>
        public static IValidation Check<TArg>(this ArgumentValidation<TArg> validation)
        {
            if (validation != null)
            {
                var exception = validation.BaseCheck();

                validation.Return();

                if (exception != null) throw exception;
            }

            return null;
        }
        
        /// <summary>
        /// Allows a check to pass if all validation on the left OR all validations on the right pass.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The validation currently being checked.</param>
        /// <returns>The current argument that is being validated.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "For optimization, and as this is an extension method that should never get called directly, no verification of non-null is necessary")]
        public static ArgumentValidation<TArg> Or<TArg>(this ArgumentValidation<TArg> validation)
        {
            validation.NewClause();

            return validation;
        }


        #region Null/Empty Values

        /// <summary>
        /// Checks that the argument is not null.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is <c>null</c>.</exception>
        public static ArgumentValidation<TArg> IsNotNull<TArg>(this ArgumentValidation<TArg> validation)
            where TArg : class
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
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification="Must to allow Nullables to work")]
        public static ArgumentValidation<TArg?> IsNotNull<TArg>(this ArgumentValidation<TArg?> validation)
            where TArg : struct
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
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is not <c>null</c>.</exception>
        public static ArgumentValidation<TArg> IsNull<TArg>(this ArgumentValidation<TArg> validation)
            where TArg : class
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue != null)
                    validation.SetException(new ArgumentException(
                        Format(Strings.Argument_NotNullValue, typeof(TArg).Name), validation.ParameterName));
            }
            
            return validation;
        }

        /// <summary>
        /// Checks that the argument is null.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is not <c>null</c>.</exception>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Must to allow Nullables to work")]
        public static ArgumentValidation<TArg?> IsNull<TArg>(this ArgumentValidation<TArg?> validation)
            where TArg : struct
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue != null)
                    validation.SetException(new ArgumentException(
                        Format(Strings.Argument_NotNullValue, typeof(TArg).Name), validation.ParameterName));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the argument is not its default value.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is its default value.</exception>
        public static ArgumentValidation<TArg> IsNotDefault<TArg>(this ArgumentValidation<TArg> validation)
            where TArg : struct
        {
            if (validation.AcceptCall())
            {
                if (Object.Equals(validation.ArgumentValue, default(TArg)))
                    validation.SetException(new ArgumentException(
                        Format(Strings.Argument_EmptyValue, typeof(TArg).Name), validation.ParameterName));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the argument is its default value.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is not its default value.</exception>
        public static ArgumentValidation<TArg> IsDefault<TArg>(this ArgumentValidation<TArg> validation)
            where TArg : struct
        {
            if (validation.AcceptCall())
            {
                if (!Object.Equals(validation.ArgumentValue, default(TArg)))
                    validation.SetException(new ArgumentException(
                        Format(Strings.Argument_NotEmptyValue, typeof(TArg).Name), validation.ParameterName));
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
        /// <exception cref="ArgumentNullException">Thrown during<see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the string argument is empty.</exception>
        [Obsolete("Use IsNotNull and IsNotEmpty")]
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
        /// Checks that the string argument is not empty. If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the string argument is empty.</exception>
        public static ArgumentValidation<string> IsNotEmpty(this ArgumentValidation<string> validation)
        {
            if (validation.AcceptCall() && validation.ArgumentValue != null)
            {
                var argVal = validation.ArgumentValue;

                if (argVal.Length == 0)
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
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the string argument is empty or only white space.</exception>
        [Obsolete("Use IsNotNull, IsNotEmpty, and IsNotWhiteSpace")]
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

        /// <summary>
        /// Checks that the string argument does not contain only white space. If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the string argument is empty or only white space.</exception>
        public static ArgumentValidation<string> IsNotWhiteSpace(this ArgumentValidation<string> validation)
        {
            if (validation.AcceptCall() && validation.ArgumentValue != null)
            {
                if (validation.ArgumentValue.IsWhiteSpace())
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
        /// <typeparam name="TArg">The type of the argument being validated. Must implement interface IEnumerable.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentNullException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the enumerable is empty.</exception>
        [Obsolete("Use IsNotNull and IsNotEmpty")]
        public static ArgumentValidation<TArg> IsNotNullOrEmpty<TArg>(this ArgumentValidation<TArg> validation)
            where TArg : class, IEnumerable
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
                    Strings.Argument_EmptyEnumerable, typeof(TArg).Name), validation.ParameterName));
            }

            return validation;
        }

        /// <summary>
        /// Checks that the enumerable contains at least 1 element. If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated. Must implement interface IEnumerable.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if the enumerable is empty.</exception>
        public static ArgumentValidation<TArg> IsNotEmpty<TArg>(this ArgumentValidation<TArg> validation)
            where TArg : class, IEnumerable
        {
            if (validation.AcceptCall() && validation.ArgumentValue != null)
            {
                if (validation.ArgumentValue.IsEnumEmpty())
                {
                    validation.SetException(new ArgumentException(Format(
                        Strings.Argument_EmptyEnumerable, typeof(TArg).Name), validation.ParameterName));
                }
            }

            return validation;
        }

        #endregion

        #region General Values

        /// <summary>
        /// Checks that the argument can be converted to another type.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <typeparam name="T">The type to convert the argument value into.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="conversionResult">The resulting value of the conversion.</param>
        /// <returns>The current argument that is being validated.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Justification="Must use since Validation object must be returned")]
        public static ArgumentValidation<TArg> CanConvertTo<TArg, T>(this ArgumentValidation<TArg> validation, out T conversionResult)
        {
            conversionResult = default(T);

            if (validation.AcceptCall() && validation.ArgumentValue != null)
            {
                try
                {
                    conversionResult = (T)Convert.ChangeType(validation.ArgumentValue, typeof(T), CultureInfo.CurrentCulture);
                }
                catch (InvalidCastException)
                {
                    validation.SetException(new ArgumentException(Format(Strings.Argument_ConvertStringFail, typeof(TArg).Name, typeof(T).Name), validation.ParameterName));
                }
                catch (FormatException)
                {
                    validation.SetException(new ArgumentException(Format(Strings.Argument_ConvertStringFail, typeof(TArg).Name, typeof(T).Name), validation.ParameterName));
                }
            }

            return validation;
        }


        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentOutOfRangeException"/> is thrown.  If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArg> IsInRange<TArg>(
            this ArgumentValidation<TArg> validation,
            Predicate<TArg> condition,
            string message)
        {
            if (condition == null) throw new ArgumentNullException("condition");

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
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArg> IsInRange<TArg>(
            this ArgumentValidation<TArg> validation,
            Predicate<TArg> condition)
        {
            return IsInRange(validation, condition, null);
        }

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentOutOfRangeException"/> is thrown.  If the argument is <c>null</c>, this check is ignored.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="format"> A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArg> IsInRange<TArg>(
            this ArgumentValidation<TArg> validation,
            Predicate<TArg> condition,
            string format,
            params object[] args)
        {
            return IsInRange(validation, condition, Format(format, args));
        }

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ArgumentException"/> is thrown.  If the argument is null, this check is ignored.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArg> That<TArg>(
            this ArgumentValidation<TArg> validation,
            Predicate<TArg> condition,
            string message)
        {
            if (condition == null) throw new ArgumentNullException("condition");

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
        /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
        /// <param name="validation">The current argument that is being validated.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="format"> A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The current argument that is being validated.</returns>
        /// <exception cref="ArgumentException">Thrown during <see cref="Check{TArg}(ArgumentValidation{TArg})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static ArgumentValidation<TArg> That<TArg>(
                 this ArgumentValidation<TArg> validation,
                 Predicate<TArg> condition,
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
