
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation.Tests
{
    static class Helpers
    {
        public static void ExpectException<TException>(Action action) where TException : Exception
        {
            Exception ex = null;

            try
            {
                action();
            }
            catch (TException exception)
            {
                if (exception.GetType() == typeof(TException)) return;

                ex = exception;
            }
            catch(Exception exception)
            {
                ex = exception;
            }

            Assert.Fail("Expected Exception did not occur.");
        }

        public static IEnumerable EmptyEnumerable()
        {
            return new EmptyEnumerableImpl();
        }

        class EmptyEnumerableImpl : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                return new EmptyEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return new EmptyEnumerator();
            }
        }

        class EmptyEnumerator : IEnumerator
        {
            public object Current
            {
                get { throw new NotImplementedException(); }
            }

            public bool MoveNext()
            {
                return false;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
