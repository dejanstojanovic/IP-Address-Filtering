using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web;
using System.Web.Http;

namespace IPAddressFiltering

{



    public class IPAddressFilterAttribute : AuthorizeAttribute
    {
        private IEnumerable<IPAddress> ipAddresses;
        private IEnumerable<IPAddressRange> ipAddressRanges;
        private IPAddressFilteringAction filteringType;

        public IEnumerable<IPAddress> IPAddresses
        {
            get
            {
                return this.ipAddresses;
            }
        }

        public IEnumerable<IPAddressRange> IPAddressRanges
        {
            get
            {
                return this.ipAddressRanges;
            }
        }

        public IPAddressFilterAttribute(string ipAddress, IPAddressFilteringAction filteringType)
           : this(new IPAddress[] { IPAddress.Parse(ipAddress) }, filteringType)
        {

        }

        public IPAddressFilterAttribute(IPAddress ipAddress, IPAddressFilteringAction filteringType)
            : this(new IPAddress[] { ipAddress }, filteringType)
        {

        }

        public IPAddressFilterAttribute(IEnumerable<string> ipAddresses, IPAddressFilteringAction filteringType)
            : this(ipAddresses.Select(a => IPAddress.Parse(a)), filteringType)
        {

        }

        public IPAddressFilterAttribute(IEnumerable<IPAddress> ipAddresses, IPAddressFilteringAction filteringType)
        {
            this.ipAddresses = ipAddresses;
            this.filteringType = filteringType;
        }



        protected override bool IsAuthorized(HttpActionContext context)
        {
            string ipAddressString = ((HttpContextWrapper)context.Request.Properties["MS_HttpContext"]).Request.UserHostName;
            IPAddress ipAddress = IPAddress.Parse(ipAddressString);

            if (this.filteringType == IPAddressFilteringAction.Allow)
            {
                if (this.ipAddresses != null && this.ipAddresses.Any() &&
                    !IsAddressInList(ipAddressString.Trim()))
                {
                    return false;
                }
                else if (this.ipAddressRanges != null && this.ipAddressRanges.Any() &&
                    !this.ipAddressRanges.Select(r => ipAddress.IsInRange(r.StartIPAddress, r.EndIPAddress)).Any())
                {
                    return false;
                }
            }
            else
            {
                if (this.ipAddresses != null && this.ipAddresses.Any() &&
                   IsAddressInList(ipAddressString.Trim()))
                {
                    return false;
                }
                else if (this.ipAddressRanges != null && this.ipAddressRanges.Any() &&
                    this.ipAddressRanges.Select(r => ipAddress.IsInRange(r.StartIPAddress, r.EndIPAddress)).Any())
                {
                    return false;
                }
            }

            return true;
        }



        private bool IsAddressInList(string IpAddress)
        {
            if (!string.IsNullOrWhiteSpace(IpAddress))
            {
                IEnumerable<string> addresses = this.ipAddresses.Select(a => a.ToString());
                return addresses.Where(a => a.Trim().Equals(IpAddress, StringComparison.InvariantCultureIgnoreCase)).Any();
            }
            return false;
        }

    }
}
