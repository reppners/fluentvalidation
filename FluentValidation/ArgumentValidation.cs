using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// Provides state information regarding the argument currently being validated.
    /// </summary>
    /// <typeparam name="TArg">The type of the argument being validated.</typeparam>
    public sealed class ArgumentValidation<TArg> : Validation 
    {
        [ThreadStatic]
        static Queue<ArgumentValidation<TArg>> _validationPool;
        

        private ArgumentValidation() { }


        /// <summary>
        /// The name of the Parameter being validated.
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// The value of the argument being validated.
        /// </summary>
        public TArg ArgumentValue { get; private set; }

        internal static ArgumentValidation<TArg> Borrow(string paramName, TArg argValue)
        {
            if (_validationPool == null) _validationPool = new Queue<ArgumentValidation<TArg>>();

            ArgumentValidation<TArg> valObj;

            if (_validationPool.Count > 0)
            {
                valObj = _validationPool.Dequeue();
            }
            else
            {
                valObj = new ArgumentValidation<TArg>();

#if DEBUG
                ArgumentValidationCounter.AddCreationCount();
#endif
            }

            valObj.ParameterName = paramName;
            valObj.ArgumentValue = argValue;

#if DEBUG
            ArgumentValidationCounter.AddMissingCount();
#endif

            return valObj;
        }

        internal void Return()
        {
            ParameterName = null;
            ArgumentValue = default(TArg);

            Clear();

            _validationPool.Enqueue(this);

        #if DEBUG
            ArgumentValidationCounter.SubtractMissingCount();
        #endif
        }


    }

#if DEBUG
    internal static class ArgumentValidationCounter
    {
        static object _syncObj = new object();

        public static void AddCreationCount()
        {
            lock(_syncObj ) { CreationCount++;}
        }

        public static void AddMissingCount()
        {
            lock (_syncObj) { MissingCount++; }
        }

        public static void SubtractMissingCount()
        {
            lock (_syncObj) { MissingCount--; }
        }

        public static void Reset()
        {
            lock(_syncObj )
            {
                CreationCount = 0;
                MissingCount = 0;
            }
        }

        public static int CreationCount { get; private set; }

        public static int MissingCount { get; private set; }
    }
#endif

}
