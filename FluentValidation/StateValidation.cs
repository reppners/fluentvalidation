using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides state information regarding the object currently being validated.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    public sealed class StateValidation<T> : Validation
    {
        [ThreadStatic]
        static Queue<StateValidation<T>> _validationPool = new Queue<StateValidation<T>>();

        private StateValidation() { }


        /// <summary>
        /// The object being validated.
        /// </summary>
        public T Object { get; private set; }


        internal static StateValidation<T> Borrow(T obj)
        {
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

        internal void Return()
        {
            Object = default(T);

            Clear();

            _validationPool.Enqueue(this);
        }

    }
}
