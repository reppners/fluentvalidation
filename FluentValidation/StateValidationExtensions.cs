using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides checks for state validations.
    /// </summary>
    public static class StateValidationExtensions
    {
        /// <summary>
        /// Begins a new State validation.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">Extensions placeholder. Can be <c>null</c>.</param>
        /// <param name="objectToValidate">The object to be validated.</param>
        /// <returns>A new <see cref="StateValidation{T}"/> instance.</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "validation", Justification = "validation is only a placeholder, but is required to get fluent validation to work as intended")]
        public static StateValidation<T> State<T>(this IValidation validation, T objectToValidate)
        {
            return StateValidation<T>.Borrow(objectToValidate);
        }

        /// <summary>
        /// Performs the validation for the current object.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <returns>A <c>null</c> placeholder.</returns>
        public static IValidation Check<T>(this StateValidation<T> validation)
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
        /// <typeparam name="T">The type of the argument being validated.</typeparam>
        /// <param name="validation">The validation currently being checked.</param>
        /// <returns>The current object validation to check against.</returns>
        public static StateValidation<T> Or<T>(this StateValidation<T> validation)
        {
            if (validation == null) throw new ArgumentNullException("validation");

            validation.NewClause();

            return validation;
        }

        #region Operation Calls

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="InvalidOperationException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static StateValidation<T> Operation<T>(this StateValidation<T> validation, Predicate<T> condition, string message)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            if (validation.AcceptCall())
            {
                if (!condition(validation.Object))
                {
                    validation.SetException(message == null ? new InvalidOperationException() : new InvalidOperationException(message));
                }
            }

            return validation;
        }


        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="InvalidOperationException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static StateValidation<T> Operation<T>(this StateValidation<T> validation, Predicate<T> condition)
        {
            return Operation(validation, condition, null);
        }

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="format"> A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="InvalidOperationException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static StateValidation<T> Operation<T>(this StateValidation<T> validation, Predicate<T> condition, string format, params object[] args)
        {
            return Operation(validation, condition, Format(format, args));
        }

        #endregion

        #region Disposed State

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ObjectDisposedException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="ObjectDisposedException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static StateValidation<T> IsNotDisposed<T>(this StateValidation<T> validation, Predicate<T> condition, string message)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            if (validation.AcceptCall())
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

        /// <summary>
        /// Checks that the provided condition evaluated to True.  If not, an <see cref="ObjectDisposedException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <param name="condition">An expression that must evaluate to true, or it will fail the validation.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="ObjectDisposedException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if <paramref name="condition"/> evaluated to false.</exception>
        public static StateValidation<T> IsNotDisposed<T>(this StateValidation<T> validation, Predicate<T> condition)
        {
            return IsNotDisposed(validation, condition, null);
        }

        /// <summary>
        /// Checks that the provided <see cref="IDisposedObservable"/> is not disposed. If so, an <see cref="ObjectDisposedException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated. Must implement <see cref="IDisposedObservable"/>.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <param name="message">An optional message to throw with the exception.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="ObjectDisposedException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if the object is disposed.</exception>
        public static StateValidation<T> IsNotDisposed<T>(this StateValidation<T> validation, string message)
            where T : IDisposedObservable
        {
            return IsNotDisposed(validation, obj => !obj.IsDisposed, message);
        }

        /// <summary>
        /// Checks that the provided <see cref="IDisposedObservable"/> is not disposed. If so, an <see cref="ObjectDisposedException"/> is thrown.
        /// </summary>
        /// <typeparam name="T">The type of the object being validated. Must implement <see cref="IDisposedObservable"/>.</typeparam>
        /// <param name="validation">The current object validation to check against.</param>
        /// <returns>The current object validation to check against.</returns>
        /// <exception cref="ObjectDisposedException">Thrown during <see cref="Check{T}(StateValidation{T})"/> if the object is disposed.</exception>
        public static StateValidation<T> IsNotDisposed<T>(this StateValidation<T> validation)
            where T : IDisposedObservable
        {
            return IsNotDisposed(validation, obj => !obj.IsDisposed, null);
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
