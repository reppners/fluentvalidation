using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides common checks for all types of validations.
    /// </summary>
    public static class ValidationExtensions
    {
        #region Supporting Extensions

        internal static bool AcceptCall(this Validation validation)
        {
            if (validation == null) return true;

            return validation.Exception == null;
        }

        #endregion

        /// <summary>
        /// Allows a check to pass if all validation on the left or all validations on the right pass.
        /// </summary>
        /// <typeparam name="TValidation">The type of the validation being checked.</typeparam>
        /// <param name="validation">The validation currently being checked.</param>
        /// <returns>The validation currently being checked.</returns>
        public static TValidation Or<TValidation>(this TValidation validation)
            where TValidation : Validation 
        {
            if (validation != null)
            {
                validation.NewClause();
            }

            return validation;
        }

    }
}
