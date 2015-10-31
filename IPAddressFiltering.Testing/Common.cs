using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressFiltering.Testing
{
    public static class Common
    {
        public static bool IsIPAddressAllowed(IPAddressFilterAttribute attribute, string ip, IPAddressFilteringAction action)
        {
           return bool.Parse( typeof(IPAddressFilterAttribute).GetMethod("IsIPAddressAllowed").Invoke(attribute ,new object[] { ip, action }));
        }
    }
}
