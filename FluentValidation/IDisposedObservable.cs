using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation
{
    /// <summary>
    /// A disposable object that also provides a safe way to query its disposed status.
    /// </summary>
    public interface IDisposedObservable : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
        bool IsDisposed { get; }
    }
}
