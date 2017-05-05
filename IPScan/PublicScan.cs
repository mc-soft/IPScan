using System.Collections.Generic;

namespace IPScan
{
    /// <summary>
    /// Scans the entire public IP address range.
    /// </summary>
    public class PublicScan
    {
        private readonly BlacklistScan _scan;

        /// <summary>
        /// Constructor for generic public IP scan.
        /// </summary>
        /// <param name="ipAddressGeneratedHandler">The event handler that will be called when an IP address is generated.</param>
        public PublicScan(IPScanner.IPScanHandler ipAddressGeneratedHandler)
        {
            _scan = new BlacklistScan(GetPrivateRangeMask());
            _scan.IPAddressGenerated += ipAddressGeneratedHandler;
        }

        /// <summary>
        /// Constructor for public IP scan with minimum thread count set.
        /// </summary>
        /// <param name="ipAddressGeneratedHandler">The event handler that will be called when an IP address is generated.</param>
        /// <param name="minimumThreadCount">The minimum number of worker threads that can be active concurrently.</param>
        public PublicScan(IPScanner.IPScanHandler ipAddressGeneratedHandler, int minimumThreadCount)
        {
            _scan = new BlacklistScan(GetPrivateRangeMask());
            _scan.IPAddressGenerated += ipAddressGeneratedHandler;
            _scan.MinimumThreadCount = minimumThreadCount;
        }

        /// <summary>
        /// Constructor for public IP scan with minimum and maximum thread count set.
        /// </summary>
        /// <param name="ipAddressGeneratedHandler">The event handler that will be called when an IP address is generated.</param>
        /// <param name="minimumThreadCount">The minimum number of worker threads that can be active concurrently.</param>
        /// <param name="maximumThreadCount">The maximum number of worker threads that can be active concurrently.</param>
        public PublicScan(IPScanner.IPScanHandler ipAddressGeneratedHandler, int minimumThreadCount,
            int maximumThreadCount)
        {
            _scan = new BlacklistScan(GetPrivateRangeMask());
            _scan.IPAddressGenerated += ipAddressGeneratedHandler;
            _scan.MinimumThreadCount = minimumThreadCount;
            _scan.MaximumThreadCount = maximumThreadCount;
        }

        /// <summary>
        /// Begin scanning the public IP address range.
        /// </summary>
        public void Scan()
        {
            _scan.Scan("X.X.X.X");
        }

        /// <summary>
        /// Gets the private IP range masks. (Including localhost addresses)
        /// </summary>
        /// <returns>A list of strings containing the private IP masks.</returns>
        private static List<string> GetPrivateRangeMask()
        {
            var privateRangeMask = new List<string>()
            {
                "192.168.X.X",
                "10.X.X.X",
                "127.X.X.X"
            };

            for (int i = 16; i <= 31; i++)
                privateRangeMask.Add($"172.{i}.X.X");

            return privateRangeMask;
        }
    }
}
