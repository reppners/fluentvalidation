﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentValidationNA.Tests
{
    [TestClass]
    public class AssumptionsTests
    {
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void Assumptions_Positive()
        {
            Validate.Assumptions().IsTrue(true)
                                  .IsFalse(false)

                                  .IsNull((string)null)
                                  .IsNotNull("")

                                  .IsNull((int?)null)
                                  .IsNotNull((int?)5)

                                  .IsDefault(0)
                                  .IsNotDefault(1)

                                  .IsNotNullOrEmpty("d")
                                  .IsNotNullOrEmpty(new int[] { 1, 2, 3 })

                                  .IsType<string>("test")

                                  .IsServicePresent("test")
#if !NET35
                                  .IsServicePresent(new Lazy<string>( () => "test"))
#endif

                                  .Check()

                                  .Assumptions().Check(); //check continuing assumptions

            
        }

        [TestMethod]
        public void Assumptions_Negative()
        {
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsTrue(false).Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsFalse(true).Check());

            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNull("").Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotNull((string)null).Check());

            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNull((int?)5).Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotNull((int?)null).Check());

            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsDefault(5).Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotDefault(0).Check());

            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotNullOrEmpty((string)null).Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotNullOrEmpty("").Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotNullOrEmpty((int[])null).Check());
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsNotNullOrEmpty(new int[0]).Check());

            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsType<string>(5).Check());

            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsServicePresent((string)null).Check());

            #if !NET35
            Helpers.ExpectException<InternalErrorException>(() => Validate.Assumptions().IsServicePresent((Lazy<string>)null).Check());
            #endif
        }

    }
}
