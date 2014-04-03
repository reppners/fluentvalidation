using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// The base class for various validation state objects and place holders.  Provides support for the Fluent Validation library and not intended to be used directly.
    /// </summary>
    public abstract class Validation 
    {

        internal static volatile bool OutstandingValidationsDetected;

        List<Exception> _exceptions;

        bool _outstanding = false;

        internal Validation()
        {
        }

        internal void SetOutstandingFlag()
        {
            _outstanding = true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1821:RemoveEmptyFinalizers")]
        ~Validation()
        {
            if (_exceptions != null || _outstanding) Validation.OutstandingValidationsDetected = true;
        }

        internal void SetExceptionInternal(Exception ex)
        {
            if (_exceptions == null) _exceptions = new List<Exception>(1);

            _exceptions.Add(ex);
        }


        internal bool HasExceptions { get { return _exceptions != null; } }

        internal IList<Exception> GetExceptions()
        {
            if (_exceptions == null) return null;
            
            var ex = _exceptions;

            _exceptions = null;
            _outstanding = false;

            return ex;
        }


        
    }

}
