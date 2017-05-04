using System.Collections.Generic;

namespace IPScan
{
    public class PrivateScan
    {
        private readonly List<string> _privateRangeMask;
        private readonly IPScanner.IPScanHandler _handler;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="IPAddressGeneratedHandler">The method that will be called when an IP address is generated.</param>
        public PrivateScan(IPScanner.IPScanHandler IPAddressGeneratedHandler)
        {
            _handler = IPAddressGeneratedHandler;

            _privateRangeMask = new List<string>()
            {
                "192.168.X.X",
                "10.X.X.X"
            };

            for (int i = 16; i <= 31; i++)
                _privateRangeMask.Add($"172.{i}.X.X");
        }

        /// <summary>
        /// Begin scanning the public IP address range.
        /// </summary>
        public void Scan()
        {
            var scan = new DefaultScan();
            scan.IPAddressGenerated += _handler;

            scan.Scan(_privateRangeMask).Wait();
        }
    }
}
