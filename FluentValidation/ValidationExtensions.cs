using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides common checks for all types of validations.
    /// </summary>
    internal static class ValidationExtensions
    {
        #region Supporting Extensions

        internal static bool AcceptCall(this Validation validation)
        {
            if (validation == null) return true;

            return !validation.HasException;
        }

        #endregion

    }
}
