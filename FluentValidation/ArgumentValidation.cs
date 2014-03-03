using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public sealed class ArgumentValidation<TArgType> : Validation 
    {
        public ArgumentValidation(string paramName, TArgType value)
        {
            ArgumentValue = value;
            ParameterName = paramName;
        }

        public string ParameterName { get; private set; }

        public TArgType ArgumentValue { get; private set; }
    }
}
