using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#if !NET35
using System.Threading.Tasks;
#endif

namespace FluentValidation.Tests
{
    [TestClass]
    public class MultithreadingTests
    {

#if !NET35
        [TestMethod]
        public void Argument_Threading()
        {
            //arrange
            Action notNullCheck = () =>
            {
                for (int j = 0; j < 1000; j++)
                {
                    Validate.Argument("").IsNotNull().Check();
                }
            };

            ArgumentValidationCounter.Reset();

            List<Task> tasks = new List<Task>();


            //act
            for (int i = 0; i < 50; i++)
            {
                tasks.Add(Task.Run(notNullCheck));
            }

            Task.WaitAll(tasks.ToArray());


            //assert
            Assert.IsTrue(ArgumentValidationCounter.CreationCount <= 20);
            Assert.AreEqual(0, ArgumentValidationCounter.MissingCount);
        }

        [TestMethod]
        public void State_Threading()
        {
            //arrange
            Action notNullCheck = () => Validate.State("").Operation(str => true).Check();
            
            List<Task> tasks = new List<Task>();


            //act
            for (int i = 0; i < 50; i++)
            {
                tasks.Add(Task.Run(notNullCheck));
            }

            Task.WaitAll(tasks.ToArray());

        }

        [TestMethod]
        public void Assumptions_Threading()
        {
            //arrange
            Action notNullCheck = () => Validate.Assumptions().IsFalse(false).Check();

            List<Task> tasks = new List<Task>();


            //act
            for (int i = 0; i < 50; i++)
            {
                tasks.Add(Task.Run(notNullCheck));
            }

            Task.WaitAll(tasks.ToArray());

        }
#endif

    }
}
