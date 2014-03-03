using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public sealed class StateValidation<T> : Validation
    {
        public StateValidation(T obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            Object = obj;
        }
        
        public T Object { get; private set; }
    }
}
