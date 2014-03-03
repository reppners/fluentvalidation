using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public static class ValidationExtensions
    {
        #region Supporting Extensions

        internal static bool AcceptCall(this Validation validation)
        {
            if (validation == null) return true;

            return validation.Exception == null;
        }

        #endregion

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
