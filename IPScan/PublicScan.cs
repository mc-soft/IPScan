using System.Collections.Generic;

namespace IPScan
{
    /// <summary>
    /// Scans the entire public IP address range.
    /// </summary>
    public class PublicScan
    {
        private readonly List<string> _privateRangeMask;
        private readonly IPScanner.IPScanHandler _handler;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="IPAddressGeneratedHandler">The method that will be called when an IP address is generated.</param>
        public PublicScan(IPScanner.IPScanHandler IPAddressGeneratedHandler)
        {
            _handler = IPAddressGeneratedHandler;

            _privateRangeMask = new List<string>()
            {
                "192.168.X.X",
                "10.X.X.X",
                "127.X.X.X"
            };

            for (int i = 16; i <= 31; i++)
                _privateRangeMask.Add($"172.{i}.X.X");
        }

        /// <summary>
        /// Begin scanning the public IP address range.
        /// </summary>
        public void Scan()
        {
            var scan = new BlacklistScan(_privateRangeMask);
            scan.IPAddressGenerated += _handler;

            scan.Scan("X.X.X.X");
        }
    }
}
