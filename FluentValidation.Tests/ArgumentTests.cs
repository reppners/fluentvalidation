using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Linq;
using System.Threading;

namespace FluentValidationNA.Tests
{
    [TestClass]
    public class ArgumentTests
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void Argument_ValidateCoreArgument()
        {
            //arrange
            int Arg1 = 1;
            int? Arg2 = 2;
            int? Arg3 = null;
            string Arg4 = "dude";
            
            //act
            var valObj1 = Validate.Argument(Arg1, "test1");
            var valObj2 = Validate.Argument(Arg2, "test2");
            var valObj3 = Validate.Argument(Arg3, "");
            var valObj4 = Validate.Argument(Arg4);
            var valObj5 = Validate.Argument(Arg3, " ");
            
            //Assert
            Assert.AreEqual("test1", valObj1.ParameterName);
            Assert.AreEqual(Arg1, valObj1.ArgumentValue);
            Assert.AreEqual("test2", valObj2.ParameterName);
            Assert.AreEqual(Arg2, valObj2.ArgumentValue);
            Assert.AreEqual("", valObj3.ParameterName);
            Assert.AreEqual(Arg3, valObj3.ArgumentValue);
            Assert.AreEqual(null, valObj4.ParameterName);
            Assert.AreEqual(Arg4, valObj4.ArgumentValue);

            Assert.AreEqual(" ", valObj5.ParameterName);
        }

        [TestMethod]
        public void Argument_ValidateNotNull()
        {
            //arrange
            string value1 = null;
            string value2 = "";
            string value3 = "value";

            int? value4 = 5;
            int? value5 = null;

            //act
            Helpers.ExpectException<ArgumentNullException>(() => Validate.Argument(value1).IsNotNull().Check());
            Validate.Argument(value2).IsNotNull().Check();
            Validate.Argument(value3).IsNotNull().Check();

            Validate.Argument(value4).IsNotNull().Check();
            Helpers.ExpectException<ArgumentNullException>(() => Validate.Argument(value5).IsNotNull().Check());
        }

        [TestMethod]
        public void Argument_ValidateIsNotDefault()
        {
            //arrange
            int value1 = 0;
            int value2 = 1;

            //act
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value1).IsNotDefault().Check());
            Validate.Argument(value2).IsNotDefault().Check();
        }

        [TestMethod]
        public void Argument_ValidateIsDefault()
        {
            //arrange
            int value1 = 1;
            int value2 = 0;

            //act
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value1).IsDefault().Check());
            Validate.Argument(value2).IsDefault().Check();
        }

        [TestMethod]
        public void Argument_ValidateNull()
        {
            //arrange
            string value1 = null;
            string value2 = "";
            string value3 = "value";

            int? value4 = 5;
            int? value5 = null;

            //act
            Validate.Argument(value1).IsNull().Check();
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value2).IsNull().Check());
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value3).IsNull().Check());

            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value4).IsNull().Check());
            Validate.Argument(value5).IsNull().Check();
        }


        [TestMethod]
        public void Argument_ValidateEmptyString()
        {
            //arrange
            string value1 = null;
            string value2 = "";
            string value3 = " \t";
            string value4 = "value";

            //act
            Validate.Argument(value1).IsNotEmpty().Check(); //nulls are skipped
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value2).IsNotEmpty().Check());
            Validate.Argument(value3).IsNotEmpty().Check();
            Validate.Argument(value4).IsNotEmpty().Check();
        }

        [TestMethod]
        public void Argument_ValidateNotWhiteSpace()
        {
            //arrange
            string value1 = null;
            string value2 = "";
            string value3 = " ";
            string value4 = " \t";
            string value5 = "value";

            //act
            Validate.Argument(value1).IsNotWhiteSpace().Check(); //nulls are skipped
            Validate.Argument(value2).IsNotWhiteSpace().Check(); //empty strings are skipped
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value3).IsNotWhiteSpace().Check());
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value4).IsNotWhiteSpace().Check());
            Validate.Argument(value5).IsNotWhiteSpace().Check();
        }

        [TestMethod]
        public void Argument_ValidateNotEmptyEnumerable()
        {
            //arrange
            IEnumerable value1 = null;
            IEnumerable value2 = Helpers.EmptyEnumerable(); //We cannot use Enumerable.Empty<object>(), since it is implemented by an empty array, which is also a collection.
            IEnumerable value3 = Enumerable.Range(1, 10);
            IEnumerable value4 = new int[] { }; //collection that should support Count
            IEnumerable value5 = new int[] { 1, 2, 3 }; //collection that should support Count

            //act
            Validate.Argument(value1).IsNotEmpty().Check(); //nulls are skipped
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value2).IsNotEmpty().Check());
            Validate.Argument(value3).IsNotEmpty().Check();
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value4).IsNotEmpty().Check());
            Validate.Argument(value5).IsNotEmpty().Check();
        }

        [TestMethod]
        public void Argument_ValidateRange()
        {
            //arrange
            int value1 = 5;
            double value2 = -3;
            int? value3 = 5;
            int? value4 = null;

            
            //act
            Validate.Argument(value1).IsInRange(n => n > 0).Check();
            Helpers.ExpectException<ArgumentOutOfRangeException>(() => Validate.Argument(value1).IsInRange(n => n < 0).Check());

            Validate.Argument(value2).IsInRange(n => n <= 0).Check();
            Helpers.ExpectException<ArgumentOutOfRangeException>(() => Validate.Argument(value2).IsInRange(n => n >= 0).Check());

            Validate.Argument(value3).IsInRange(n => n > 0).Check();
            Helpers.ExpectException<ArgumentOutOfRangeException>(() => Validate.Argument(value3).IsInRange(n => n < 0).Check());

            Validate.Argument(value4).IsInRange(n => n > 0).Check();
            Validate.Argument(value4).IsInRange(n => n < 0, "test {0}", "formatting").Check();
        }

        [TestMethod]
        public void Argument_That()
        { 
            var value1 = 5;
            string value2 = null;

            Validate.Argument(value1).That(v => v == 5, "test {0}", "formatting").Check();
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(value1).That(v => v != 5, "msg").Check());

            Validate.Argument(value2).That(v => v == "fail", "msg").Check();
        }


        [TestMethod]
        public void Argument_CheckAll()
        {
            //Arrange
            Action singleFailCheck = () => Validate.Argument((string)null).IsNotNull().Check();
            Action multiFailCheck = () => Validate.Argument(" ").IsNull().IsNotWhiteSpace().Check();

            Action singleFailCheckAll = () => Validate.Argument((string)null).IsNotNull().CheckAll();
            Action multiFailCheckAll = () => Validate.Argument(" ").IsNull().IsNotWhiteSpace().CheckAll();


            //Act
            Helpers.ExpectException<ArgumentNullException>(singleFailCheck);
            Helpers.ExpectException<ArgumentException>(multiFailCheck);
            Helpers.ExpectException<ArgumentNullException>(singleFailCheckAll);
            Helpers.ExpectException<AggregateException>(multiFailCheckAll);
        }

        [TestMethod]
        public void Argument_ComplexTest()
        {
            //arrange
            Action success1 = () => ComplexFunction("", null, " ", 5, "A");
            Action success2 = () => ComplexFunction("asd", "test", "asd ", 15, "A");
            Action success3 = () => ComplexFunction("asd", "test", "asd ", 15, "A");

            Action fail1 = () => ComplexFunction(null, null, " ", 5, "A");
            Action fail2 = () => ComplexFunction("", null, "", 5, "A");
            Action fail3 = () => ComplexFunction("", null, " ", 0, "A");
            Action fail4 = () => ComplexFunction("asd", null, "asd ", 15, "a");
            Action fail5 = () => ComplexFunction("", "", "test", 5, "TEST");
            
            //act
            success1();
            success2();
            success3();

            Helpers.ExpectException<ArgumentNullException>(fail1);
            Helpers.ExpectException<ArgumentException>(fail2);
            Helpers.ExpectException<ArgumentOutOfRangeException>(fail3);
            Helpers.ExpectException<ArgumentException>(fail4);
            Helpers.ExpectException<ArgumentException>(fail5);
        }     

        static void ComplexFunction(
            string cannotBeNullStr,
            string cannotBeEmptyStr,
            string cannotBeNullOrEmptyStr, 
            int mustBeGreaterThanZero, 
            string mustBeAllCaps)
        {
            Validate.Argument(cannotBeNullStr, "cannotBeNullStr").IsNotNull().Check()
                    .Argument(cannotBeEmptyStr, "cannotBeEmptyStr").IsNotEmpty().Check()
                    .Argument(cannotBeNullOrEmptyStr, "cannotBeNullOrEmptyStr").IsNotNull().IsNotEmpty().Check()
                    .Argument(mustBeGreaterThanZero, "mustBeGreaterThanZero").IsInRange(v => v > 0).Check()
                    .Argument(mustBeAllCaps, "mustBeAllCaps").That(s => s.ToUpper() == s, "Value must be all caps").Check();
            
        }

        [TestMethod]
        public void Argument_FootprintTest()
        {

            //set these to zero since previous tests may have moved them.
            ArgumentValidationCounter.Reset();

            Argument_ComplexTest();

            Assert.AreEqual(0, ArgumentValidationCounter.MissingCount);

            var createCount = ArgumentValidationCounter.CreationCount;

            for (int i = 0; i < 100; i++) Argument_ComplexTest();

            Assert.AreEqual(createCount, ArgumentValidationCounter.CreationCount);
            Assert.AreEqual(0, ArgumentValidationCounter.MissingCount);
        }

        [TestMethod]
        public void Argument_CanConvertTo()
        {
            string test1 = "123";
            string test2 = "bla";

            int result1;
            int result2;

            Validate.Argument(test1).CanConvertTo(out result1).Check();
            Helpers.ExpectException<ArgumentException>(() => Validate.Argument(test2).CanConvertTo(out result2).Check());

            Assert.AreEqual(123, result1);
        }
    }
}
