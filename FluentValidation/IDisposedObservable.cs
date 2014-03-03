using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    public interface IDisposedObservable : IDisposable
    {
        bool IsDisposed { get; }
    }
}
