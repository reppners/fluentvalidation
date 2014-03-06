using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidation.Tests
{
    [TestClass]
    public class AssumptionsTests
    {
        [TestMethod]
        public void Assumptions_TestBasics()
        {
            Validate.Assumptions().IsTrue(() => true)
                                  .IsFalse(() => false)

                                  .IsNull(() => (string)null)
                                  .IsNotNull(() => "")

                                  .IsNull(() => (int?)null)
                                  .IsNotNull(() => (int?)5)

                                  .IsNotNullOrEmpty(() => "d")
                                  .IsNotNullOrEmpty(() => new int[] { 1, 2, 3 })

                                  .IsType<string>(() => "test")

                                  .ServicePresent(() => "test")

                                  .Check();
        }
    }
}
