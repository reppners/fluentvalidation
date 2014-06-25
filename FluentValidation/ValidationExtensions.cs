using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidationNA
{
    /// <summary>
    /// Provides common checks for all types of validations.
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// Performs the validation for the current validation. The first pending validation exception is thrown here.
        /// </summary>
        /// <param name="validation">The current validation.</param>
        /// <returns>A <c>null</c> placeholder.</returns>
        public static IValidation Check(this Validation validation)
        {
            if (validation != null)
            {
                var exceptions = validation.GetExceptions();

                var returnable = validation as IPoolReturnable;

                if (returnable != null) returnable.Return();

                if (exceptions != null) throw exceptions[0];
            }

            return null;
        }

        /// <summary>
        /// Performs the validation for the current validation. All pending validation exception are thrown here.
        /// </summary>
        /// <param name="validation">The current validation.</param>
        /// <returns>A <c>null</c> placeholder.</returns>
        public static IValidation CheckAll(this Validation validation)
        {
            if (validation != null)
            {
                var exceptions = validation.GetExceptions();

                var returnable = validation as IPoolReturnable;

                if (returnable != null) returnable.Return();

                if (exceptions != null)
                {
                    if (exceptions.Count > 1) throw new AggregateException(Strings.DefaultAggregateExceptionMessage, exceptions);

                    throw exceptions[0];
                }
            }

            return null;
        }
    }
}
