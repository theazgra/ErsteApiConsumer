using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErsteTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1.5, 1.5);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsFalse(1 != 1);
        }


    }
}
