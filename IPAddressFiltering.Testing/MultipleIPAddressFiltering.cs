using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressFiltering.Testing
{
    [TestClass]
    public class MultipleIPAddressFiltering
    {
        [TestMethod]
        public void TestMultipleIPRestrictMatch()
        {
            Assert.AreEqual<bool>(false, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Restrict));
        }

        [TestMethod]
        public void TestMultipleIPRestrictNoMatch()
        {
            Assert.AreEqual<bool>(true, CheckIPAddress("94.201.252.26", IPAddressFilteringAction.Restrict));
        }

        [TestMethod]
        public void TestMultipleIPAllowMatch()
        {
            Assert.AreEqual<bool>(true, CheckIPAddress("94.201.252.25", IPAddressFilteringAction.Allow));
        }

        [TestMethod]
        public void TestMultipleIPAllowNoMatch()
        {
            Assert.AreEqual<bool>(false, CheckIPAddress("94.201.252.26", IPAddressFilteringAction.Allow));
        }

        private bool CheckIPAddress(string requestIP, IPAddressFilteringAction action)
        {

            IPAddressFilterAttribute attribute =
                new IPAddressFilterAttribute(new string[] {
                           "94.201.252.21",
                           "94.201.252.22",
                           "94.201.252.23",
                           "94.201.252.24",
                           "94.201.252.25", //Matching IP
                           "94.201.252.26",
                           "94.201.252.27"
            }, action);
            return Common.IsIPAddressAllowed(attribute, requestIP);

        }
    }
}
