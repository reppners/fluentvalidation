using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// The base class for various validation state objects and place holders.  Provides support for the Fluent Validation library and not intended to be used directly.
    /// </summary>
    public abstract class Validation 
    {
        Exception _exception;

        List<Exception> _exceptionClauses = new List<Exception>(3);

        internal Validation() { }

        internal void SetException(Exception ex)
        {
#if DEBUG
            if (_exception != null) throw new InvalidOperationException();
#endif

            _exception = ex;
        }

        internal bool HasException { get { return _exception != null; } }


        internal void NewClause()
        {
            NewClause(false);
        }

        private void NewClause(bool isCheck)
        {
            if (isCheck && _exceptionClauses.Count == 0 && _exception == null) return;

            _exceptionClauses.Add(_exception);
            _exception = null;
        }

        internal Exception BaseCheck()
        {
            NewClause(true);

            var size = _exceptionClauses.Count;

            if (size == 0) return null;

            //if any groups have no exceptions, then the group passed.
            for (int i = 0; i < size; i++)
            {
                if (_exceptionClauses[i] == null) return null;
            }

            if (_exceptionClauses.Count == 1) return _exceptionClauses[0];

            //otherwise, we return them all
            return new AggregateException(_exceptionClauses);
        }

        internal void Clear()
        {
            _exception = null;
            _exceptionClauses.Clear();
        }
    }

    
}
