using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPAddressFiltering.Testing
{
    [TestClass]
    public class MultipleIPAddressRangeFiltering
    {
        [TestMethod]
        public void TestMultipleIPRestrictMatch()
        {
            Assert.AreEqual<bool>(false, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Restrict));
        }

        [TestMethod]
        public void TestMultipleIPRestrictNoMatch()
        {
            Assert.AreEqual<bool>(true, CheckIPAddress("94.201.252.100", IPAddressFilteringAction.Restrict));
        }

        [TestMethod]
        public void TestMultipleIPAllowMatch()
        {
            Assert.AreEqual<bool>(true, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Allow));
        }

        [TestMethod]
        public void TestMultipleIPAllowNoMatch()
        {
            Assert.AreEqual<bool>(false, CheckIPAddress("94.201.252.100", IPAddressFilteringAction.Allow));
        }

        private bool CheckIPAddress(string requestIP, IPAddressFilteringAction action)
        {

            IPAddressFilterAttribute attribute = new IPAddressFilterAttribute(
                new IPAddressRange[] {
                new IPAddressRange("94.123.252.5", "94.130.252.100"),
                new IPAddressRange("94.201.252.5", "94.201.252.90"),
                new IPAddressRange("94.201.242.1", "94.201.242.101"),
                new IPAddressRange("34.201.232.5", "54.201.242.200"),
                }, action);
            return Common.IsIPAddressAllowed(attribute, requestIP);

        }
    }
}
