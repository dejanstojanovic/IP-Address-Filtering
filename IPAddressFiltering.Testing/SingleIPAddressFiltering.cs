using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPAddressFiltering.Testing
{
    [TestClass]
    public class SingleIPAddressFiltering
    {

        [TestMethod]
        public void TestSingleIPRestrictMatch()
        {
            Assert.AreEqual<bool>(false, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Restrict));
        }

        [TestMethod]
        public void TestSingleIPRestrictNoMatch()
        {
            Assert.AreEqual<bool>(true, CheckIPAddress("94.201.252.26", IPAddressFilteringAction.Restrict));
        }

        [TestMethod]
        public void TestSingleIPAllowMatch()
        {
            Assert.AreEqual<bool>(true, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Allow));
        }

        [TestMethod]
        public void TestSingleIPAllowNoMatch()
        {
            Assert.AreEqual<bool>(false, CheckIPAddress("94.201.252.26", IPAddressFilteringAction.Allow));
        }

        private bool CheckIPAddress(string requestIP, IPAddressFilteringAction action)
        {

            IPAddressFilterAttribute attribute = new IPAddressFilterAttribute("94.201.252.25", action);
            return Common.IsIPAddressAllowed(attribute, requestIP);

        }

    }
}
