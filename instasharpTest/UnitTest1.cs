using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using instasharp;

namespace instasharpTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ViewModel vm = new ViewModel();
            String cb = vm.RandomString();
            Assert.AreEqual(cb, "ABCDEFGHIJK");
        }
    }
}
