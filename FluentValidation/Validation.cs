using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidationNA
{
    /// <summary>
    /// The base class for various validation state objects and place holders.  Provides support for the Fluent Validation library and not intended to be used directly.
    /// </summary>
    public abstract class Validation 
    {
        List<Exception> _exceptions;

        internal Validation()
        {
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

            return ex;
        }


        
    }

}
