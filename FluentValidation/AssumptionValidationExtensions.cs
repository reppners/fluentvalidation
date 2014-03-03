using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public static class AssumptionValidationExtensions
    {
        public static AssumptionValidation Assumptions(this IValidation validation)
        {
            return null;
        }

        public static IValidation Check(this AssumptionValidation validation)
        {
            if (validation != null)
            {
                var exception = validation.Check();

                if (exception != null)
                {
                    throw exception;
                }
            }

            return null;
        }

        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, Func<T> value)
            where T : class
        {
            return IsTrue(validation, () => value() != null);
        }

        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, Func<T> value)
            where T : class
        {
            return IsTrue(validation, () => value() == null);
        }


        public static AssumptionValidation IsNotNull<T>(this AssumptionValidation validation, Func<T?> value)
            where T : struct
        {
            return IsTrue(validation, () => value().HasValue);
        }

        public static AssumptionValidation IsNull<T>(this AssumptionValidation validation, Func<T?> value)
            where T : struct
        {
            return IsTrue(validation, () => !value().HasValue);
        }

        public static AssumptionValidation IsNotNullOrEmpty(this AssumptionValidation validation, string value)
        {
            return IsTrue(validation, () => !String.IsNullOrEmpty(value));
        }

        public static AssumptionValidation IsNotNullOrEmpty<T>( this AssumptionValidation validation, T enumerable)
            where T : IEnumerable
        {
            return IsTrue(validation, () => !enumerable.IsEnumEmpty());
        }

        public static AssumptionValidation Is<T>( this AssumptionValidation validation, object value)
        {
            return IsTrue(validation, () => value is T);
        }

        public static AssumptionValidation IsFalse(this AssumptionValidation validation, Func<bool> condition, string message = null)
        {
            return IsTrue(validation, () => !condition());
        }

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


        public static AssumptionValidation ServicePresent<T>(this AssumptionValidation validation, T service)
        {
            if (validation.AcceptCall())
            {
                if (service == null)
                {
                    var coreType = Helpers.TrimGenericWrapper(typeof(T), typeof(Lazy<>));

                    Fail(ref validation, Format(Strings.ServiceMissing, coreType.FullName));
                }
            }

            return validation;
        }


        static void Fail(ref AssumptionValidation validation, string message)
        {
            var exeption = new InternalErrorException(message);

            if (validation == null) validation = new AssumptionValidation();

            validation.SetException(exeption);
        }
        private static string Format(string format, params object[] arguments)
        {
            return Helpers.FormatError(format, arguments);
        }

    }


}
