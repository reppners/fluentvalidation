using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{

    public class InternalErrorException : Exception
    {
        public InternalErrorException(string message)
            : base(message ?? Strings.InternalExceptionMessage)
        {

        }
    }
}
