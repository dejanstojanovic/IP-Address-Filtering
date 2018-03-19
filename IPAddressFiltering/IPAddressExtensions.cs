using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace IPAddressFiltering
{
    public static class IPAddressExtensions
    {
        public static bool IsInRange(this IPAddress address, IPAddress start, IPAddress end)
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


        // https://social.msdn.microsoft.com/Forums/en-US/29313991-8b16-4c53-8b5d-d625c3a861e1/ip-address-validation-using-cidr?forum=netfxnetcom
        public static bool IsInCIDR(this IPAddress address, IPAddress matchAddress, int cidr)
        {
            int baseAddress = BitConverter.ToInt32(matchAddress.GetAddressBytes(), 0);
            int addressInt = BitConverter.ToInt32(address.GetAddressBytes(), 0);
            int mask = IPAddress.HostToNetworkOrder(-1 << (32 - cidr));
            return ((baseAddress & mask) == (addressInt & mask));
        }

        public static bool IsInList(this IPAddress address, IEnumerable<IPAddress> addresses)
        {
            throw new NotImplementedException();
        }


    }
}
