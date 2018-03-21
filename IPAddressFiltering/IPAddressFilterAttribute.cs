using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Controllers;
using System.Web;
using System.Web.Http;
using System.Net.Sockets;
using IPAddressFiltering.Configuration;
using IPAddressFiltering.Models;

namespace IPAddressFiltering

{
    public class IPAddressFilterAttribute : AuthorizeAttribute
    {
        #region Fields

        private IList<IPAddress> ipAddresses;
        private IList<AddressRange> ipAddressRanges;
        private IList<AddressMask> ipAddressMasks;

        private IConfiguration configuration;
        #endregion

        #region Constructors
        public IPAddressFilterAttribute(String configurationKey)
        {
            this.ipAddresses = new List<IPAddress>();
            this.ipAddressRanges = new List<AddressRange>();
            this.ipAddressMasks = new List<AddressMask>();

            this.configuration = new ApplicationConfiguration();
            String config = configuration.GetConfiguration(configurationKey);
            this.ParseConfig(configurationKey);
        }

        #endregion

        protected override bool IsAuthorized(HttpActionContext context)
        {
            IPAddress ipAddress = IPAddress.Parse(((HttpContextWrapper)context.Request.Properties["MS_HttpContext"]).Request.UserHostName);
            
            foreach(var range in this.ipAddressRanges)
            {
                if (!BelongsToRange(ipAddress, range.StartIPAddress, range.EndIPAddress))
                {
                    return false;
                }
           }


            foreach (var mask in this.ipAddressMasks)
            {
                if (!BelongsToSubnet(ipAddress, mask.Address, mask.CIDR))
                {
                    return false;
                }
            }


            if (!BelongsToList(ipAddress, ipAddresses))
            {
                return false;
            }

            return true;
        }


        #region Methods

        private void ParseConfig(String configKey)
        {
            var segments = this.configuration.Configurations[configKey].Split(',').Select(v => v.Trim());
            foreach (var segment in segments)
            {
                if (segment.Contains("-"))
                {
                    var range = segment.Split('-').Select(a => IPAddress.Parse(a)).ToList();
                    if (range.Count != 2)
                    {
                        throw new FormatException("Invalid IP filtering configuration for IP range");
                    }
                    else
                    {
                        this.ipAddressRanges.Add(new AddressRange(range.First(), range.Last()));
                    }
                }
                else if (segment.Contains("/"))
                {
                    var mask = segment.Split('/');
                    if (mask.Length != 2)
                    {
                        throw new FormatException("Invalid IP filtering configuration for IP mask");
                    }

                    if (int.Parse(mask.Last()) > 32)
                    {
                        throw new FormatException("Invalid IP filtering configuration for IP mask. CIDR cannot be grater than 32");
                    }

                    this.ipAddressMasks.Add(new AddressMask(IPAddress.Parse(mask.First()), int.Parse(mask.Last())));
                }
                else
                {
                    this.ipAddresses.Add(IPAddress.Parse(segment));
                }
            }
        }


        private bool BelongsToRange(IPAddress address, IPAddress start, IPAddress end)
        {

            AddressFamily addressFamily = start.AddressFamily;
            byte[] lowerBytes = start.GetAddressBytes();
            byte[] upperBytes = end.GetAddressBytes();

            if (address.AddressFamily != addressFamily)
            {
                return false;
            }

            byte[] addressBytes = address.GetAddressBytes();

            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }

                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }

            return true;
        }

        //https://social.msdn.microsoft.com/Forums/en-US/29313991-8b16-4c53-8b5d-d625c3a861e1/ip-address-validation-using-cidr?forum=netfxnetcom
        //https://doc.m0n0.ch/quickstartpc/intro-CIDR.html
        private bool BelongsToSubnet(IPAddress address, IPAddress matchAddress, int cidr)
        {
            int baseAddress = BitConverter.ToInt32(matchAddress.GetAddressBytes(), 0);
            int addressInt = BitConverter.ToInt32(address.GetAddressBytes(), 0);
            int mask = IPAddress.HostToNetworkOrder(-1 << (32 - cidr));
            return ((baseAddress & mask) == (addressInt & mask));
        }

        private bool BelongsToList(IPAddress address, IEnumerable<IPAddress> addresses)
        {
            return addresses.Contains(address);
        }
        #endregion

    }
}
