using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public static class ArgumentValidationExtensions
    {
        public static ArgumentValidation<TArgType> Argument<TArgType>(this IValidation validation, TArgType value, string paramName = null)
        {
            return new ArgumentValidation<TArgType>(paramName, value);
        }


        public static IValidation Check<TArgType>(this ArgumentValidation<TArgType> validation)
        {
            var exception = validation.Check();

            if (exception != null) throw exception;

            return null;
        }

        #region Null Values

        public static ArgumentValidation<TArgType> IsNotNull<TArgType>(this ArgumentValidation<TArgType> validation)
            where TArgType : class
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue == null) validation.SetException(new ArgumentNullException(validation.ParameterName));
            }

            return validation;
        }

        public static ArgumentValidation<TArgType?> IsNotNull<TArgType>(this ArgumentValidation<TArgType?> validation)
            where TArgType : struct
        {
            if (validation.AcceptCall())
            {
                if (validation.ArgumentValue == null) validation.SetException(new ArgumentNullException(validation.ParameterName));
            }

            return validation;
        }

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

        public static ArgumentValidation<string> IsNotNullOrWhiteSpace(this ArgumentValidation<string> validation)
        {
            if (validation.AcceptCall())
            {
                var argVal = validation.ArgumentValue;

                if (argVal == null)
                {
                    validation.SetException(new ArgumentNullException(validation.ParameterName));
                }
                else if (String.IsNullOrWhiteSpace(argVal))
                {
                    validation.SetException(new ArgumentException(Format(Strings.Argument_WhiteSpaceString), validation.ParameterName));
                }
            }

            return validation;
        }

        #endregion

        #region Enumerable Values

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

        public static ArgumentValidation<TArgType> Range<TArgType>(
            this ArgumentValidation<TArgType> validation,
            Predicate<TArgType> condition,
            string format,
            params object[] args)
        {
            return Range(validation, condition, Format(format, args));
        }

        public static ArgumentValidation<TArgType> That<TArgType>(
            this ArgumentValidation<TArgType> validation,
            Predicate<TArgType> condition,
            string message)
        {
            if (validation.AcceptCall())
            {
                if (!condition(validation.ArgumentValue))
                {
                    validation.SetException(new ArgumentException(message));
                }
            }

            return validation;
        }

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
