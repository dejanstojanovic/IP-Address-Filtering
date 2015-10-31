using System.Net;

namespace IPAddressFiltering
{
    public class IPAddressRange
    {
        private IPAddress startIPAddress;
        private IPAddress endIPAddress;

        public IPAddress StartIPAddress
        {
            get
            {
                return this.startIPAddress;
            }
        }

        public IPAddress EndIPAddress
        {
            get
            {
                return this.endIPAddress;
            }
        }

        public IPAddressRange(string startIPAddress, string endIPAddress)
            : this(IPAddress.Parse(startIPAddress), IPAddress.Parse(endIPAddress))
        {

        }
        public IPAddressRange(IPAddress startIPAddress, IPAddress endIPAddress)
        {
            this.startIPAddress = startIPAddress;
            this.endIPAddress = endIPAddress;
        }
    }
}
