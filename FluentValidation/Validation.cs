using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public abstract class Validation
    {
        internal Exception Exception { get; private set; }

        LinkedList<Exception> _exceptionClauses;


        internal Validation() { }

        internal void SetException(Exception ex)
        {
            if (Exception != null) throw new InvalidOperationException();

            Exception = ex;
        }


        internal void NewClause()
        {
            NewClause(false);
        }

        private void NewClause(bool isCheck)
        {
            if (isCheck && _exceptionClauses == null && Exception == null) return;

            if (_exceptionClauses == null) _exceptionClauses = new LinkedList<Exception>();

            _exceptionClauses.AddLast(Exception);
            Exception = null;
        }

        internal Exception Check()
        {
            NewClause(true);

            if (_exceptionClauses == null) return null;

            //if any groups have no exceptions, then the group passed.
            if (_exceptionClauses.Any(ex => ex == null)) return null;

            var exceptions = _exceptionClauses.ToArray();

            if (exceptions.Length == 1) return exceptions[0];

            //otherwise, we return them all
            return new AggregateException(exceptions);
        }
    }

    
}
