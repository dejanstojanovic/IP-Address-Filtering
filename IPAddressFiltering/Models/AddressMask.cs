using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressFiltering.Models
{
    public class AddressMask
    {
        private IPAddress ipAddress;
        private int cidr;


        public AddressMask(IPAddress ipAddress, int cidr)
        {
            this.ipAddress = ipAddress;
            this.cidr = cidr;
        }

        public AddressMask(String ipAddress, String cidr) : this(IPAddress.Parse(ipAddress), int.Parse(cidr)) { }

    }
}
