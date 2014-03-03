using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public static class Validate
    {
        public static ArgumentValidation<TArgType> Argument<TArgType>(TArgType value, string paramName = null)
        {
            return new ArgumentValidation<TArgType>(paramName, value);
        }

        public static StateValidation<T> State<T>(T objectToValidate)
        {
            return new StateValidation<T>(objectToValidate);
        }

        public static AssumptionValidation Assumptions()
        {
            return null;
        }
    }

    public interface IValidation { }
}
