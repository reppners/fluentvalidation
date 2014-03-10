using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// A place holder for assumption validations. Provides support for the Fluent Validation library and not intended to be used directly.
    /// </summary>
    public sealed class AssumptionValidation : Validation 
    {
        [ThreadStatic]
        static Queue<AssumptionValidation> _validationPool;

        private AssumptionValidation() { }

        internal static AssumptionValidation Borrow()
        {
            if (_validationPool == null) _validationPool = new Queue<AssumptionValidation>();

            AssumptionValidation valObj;

            if (_validationPool.Count > 0)
            {
                valObj = _validationPool.Dequeue();
            }
            else
            {
                valObj = new AssumptionValidation();
            }
            
            return valObj;
        }

        internal void Return()
        {
            Clear();

            _validationPool.Enqueue(this);
        }
    }

}
