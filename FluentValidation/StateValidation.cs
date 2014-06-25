using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace FluentValidationNA
{
    /// <summary>
    /// Provides state information regarding the object currently being validated.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    public sealed class StateValidation<T> : Validation, IPoolReturnable
    {

        [ThreadStatic]
        static Queue<StateValidation<T>> _validationPool;

        private StateValidation() { }


        /// <summary>
        /// The object being validated.
        /// </summary>
        public T Object { get; private set; }


        internal static StateValidation<T> Borrow(T obj)
        {
            if (_validationPool == null) _validationPool = new Queue<StateValidation<T>>();

            StateValidation<T> valObj;

            if (_validationPool.Count > 0)
            {
                valObj = _validationPool.Dequeue();
            }
            else
            {
                valObj = new StateValidation<T>();
            }

            valObj.Object = obj;

            return valObj;
        }

        void IPoolReturnable.Return()
        {
            Object = default(T);

            _validationPool.Enqueue(this);
        }

    }
}
