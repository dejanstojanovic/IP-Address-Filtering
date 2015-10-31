using System.Net;
using System.Net.Sockets;

namespace IPAddressFiltering
{
    public static class IPAddressExtensions
    {
        /// <summary>
        /// Check if the IP address is in a range if IP addresses
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static bool IsInRange(this IPAddress address, IPAddress lower, IPAddress upper)
        {
            AddressFamily addressFamily = lower.AddressFamily;
            byte[] lowerBytes = lower.GetAddressBytes();
            byte[] upperBytes = upper.GetAddressBytes();

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

    }
}
