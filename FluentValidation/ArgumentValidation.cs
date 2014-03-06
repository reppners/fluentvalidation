using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides state information regarding the argument currently being validated.
    /// </summary>
    /// <typeparam name="TArgType">The type of the argument being validated.</typeparam>
    public sealed class ArgumentValidation<TArgType> : Validation 
    {
        [ThreadStatic]
        static Queue<ArgumentValidation<TArgType>> _validationPool = new Queue<ArgumentValidation<TArgType>>();
        

        private ArgumentValidation() { }


        /// <summary>
        /// The name of the Parameter being validated.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// The value of the argument being validated.
        /// </summary>
        public TArgType ArgumentValue { get; private set; }

        internal static ArgumentValidation<TArgType> Borrow(string paramName, TArgType argValue)
        {
            ArgumentValidation<TArgType> valObj;

            if (_validationPool.Count > 0)
            {
                valObj = _validationPool.Dequeue();
            }
            else
            {
                valObj = new ArgumentValidation<TArgType>();

#if DEBUG
                ArgumentValidationCounter.CreationCount++;
#endif
            }

            valObj.ParameterName = paramName;
            valObj.ArgumentValue = argValue;

#if DEBUG
            ArgumentValidationCounter.MissingCount++;
#endif

            return valObj;
        }

        internal void Return()
        {
            ParameterName = null;
            ArgumentValue = default(TArgType);

            Clear();

            _validationPool.Enqueue(this);

        #if DEBUG
            ArgumentValidationCounter.MissingCount--;
        #endif
        }


    }

#if DEBUG
    internal static class ArgumentValidationCounter
    {
        public static int CreationCount { get; set; }

        public static int MissingCount { get; set; }
    }
#endif

}
