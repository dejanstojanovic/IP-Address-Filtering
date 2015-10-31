using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace IPAddressFiltering.Testing
{
    public static class Common
    {
        public static bool IsIPAddressAllowed(IPAddressFilterAttribute attribute, string ip)
        {
            return (bool)typeof(IPAddressFilterAttribute).GetMethod("IsIPAddressAllowed", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(attribute, new object[] { ip });
        }
    }
}
