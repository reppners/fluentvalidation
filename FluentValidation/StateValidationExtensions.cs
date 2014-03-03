using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public static class StateValidationExtensions
    {
        public static StateValidation<T> State<T>(this IValidation validation, T objectToValidate)
        {
            return new StateValidation<T>(objectToValidate);
        }


        public static IValidation Check<T>(this StateValidation<T> validation)
        {
            var exception = validation.Check();

            if (exception != null) throw exception;

            return null;
        }

        #region Operation Calls

        public static StateValidation<T> Operation<T>(this StateValidation<T> validation, Predicate<T> condition, string message = null)
        {
            if( validation.AcceptCall() )
            {
                if (!condition(validation.Object))
                {
                    validation.SetException(message == null ? new InvalidOperationException() : new InvalidOperationException(message));
                }
            }

            return validation;
        }

        public static StateValidation<T> Operation<T>(this StateValidation<T> validation, Predicate<T> condition, string format, params object[] args)
        {
            return Operation(validation, condition, Format(format, args));
        }

        #endregion

        #region Disposed State

        public static StateValidation<T> NotDisposed<T>(this StateValidation<T> validation, Predicate<T> condition, string message = null)
        {
            if( validation.AcceptCall() )
            {
                if (!condition(validation.Object))
                {
                    var objectName = typeof(T).FullName;

                    if (message == null)
                        validation.SetException(new ObjectDisposedException(objectName));
                    else
                        validation.SetException(new ObjectDisposedException(objectName, message));
                }
            }

            return validation;
        }

        public static StateValidation<T> NotDisposed<T>(this StateValidation<T> validation, string message = null)
            where T : IDisposedObservable
        {
            return NotDisposed(validation, obj => !obj.IsDisposed, message);
        }

        #endregion

        #region Helpers

        private static string Format(string format, params object[] arguments)
        {
            return Helpers.FormatError(format, arguments);
        }

        #endregion
    }
}
