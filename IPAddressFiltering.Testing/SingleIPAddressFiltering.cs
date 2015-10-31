using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPAddressFiltering.Testing
{
    [TestClass]
    public class SingleIPAddressFiltering
    {
        string ip = "";

        [TestMethod]
        public void TestSingleIPBlock()
        {
            bool result;
            IPAddressFilterAttribute attribute = new IPAddressFilterAttribute(this.ip, IPAddressFilteringAction.Restrict);
            result = attribute.is
            Assert.AreEqual<bool>(false, result);
        }

        [TestMethod]
        public void TestSingleIPAllow()
        {

        }
    }
}
