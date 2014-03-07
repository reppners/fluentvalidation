using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace FluentValidation.Tests
{
    [TestClass]
    public class StateTests
    {
        [TestMethod]
        public void State_ValidateOperation()
        {
            Validate.State(1).Operation(n => n > 0).Check().State(-1).Operation(n => n < 0).Check();
            Helpers.ExpectException<InvalidOperationException>(() => Validate.State(1).Operation(n => n < 0).Check());
            Helpers.ExpectException<InvalidOperationException>(() => Validate.State(1).Operation(n => n < 0,"test {0}", "format" ).Check());
        }

        [TestMethod]
        public void State_ValidateNotDisposed()
        {
            //arrange
            object test = new object();
            
            var disposed = Mock.Create<IDisposedObservable>();
            Mock.Arrange(() => disposed.IsDisposed).Returns(true);

            var notDisposed = Mock.Create<IDisposedObservable>();
            Mock.Arrange(() => notDisposed.IsDisposed).Returns(false);
            

            //Act
            Validate.State(test).NotDisposed(t => true, "msg").Check();
            Helpers.ExpectException<ObjectDisposedException>(() => Validate.State(test).NotDisposed(t => false, "msg").Check());

            Helpers.ExpectException<ObjectDisposedException>(() => Validate.State(disposed).NotDisposed().Check());
            Validate.State(notDisposed).NotDisposed().Check();
        }
    }
}
