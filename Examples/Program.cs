using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using IPScan;

namespace Examples
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Beginning public IP scan....");
            
            PublicScan();
        }

        /// <summary>
        /// This will generate the whole 192.168.X.X range.
        /// </summary>
        private static void DefaultScanSingle()
        {
            var scan = new DefaultScan();
            scan.IPAddressGenerated += IPGenerated;
            
            scan.Scan("192.168.X.X");
        }

        /// <summary>
        /// This will generate the 192.168.1.X and 192.168.0.X ranges.
        /// </summary>
        private static void DefaultScanList()
        {
            var scan = new DefaultScan();
            scan.IPAddressGenerated += IPGenerated;

            var targetMasks = new List<string>
            {
                "192.168.1.X",
                "192.168.0.X"
            };

            scan.Scan(targetMasks);
        }

        /// <summary>
        /// This will generate the 192.168.X.X range, excluding any addresses that fall within the 192.168.0.X range.
        /// </summary>
        private static void BlacklistScan()
        {
            var blacklist = new List<string>()
            {
                "192.168.0.X"
            };

            var scan = new BlacklistScan(blacklist);
            scan.IPAddressGenerated += IPGenerated;

            scan.Scan("192.168.X.X");
        }

        /// <summary>
        /// This generates the whole public IP range.
        /// </summary>
        private static void PublicScan()
        {
            var scan = new PublicScan(IPGenerated);
            scan.Scan();
        }

        /// <summary>
        /// This generates the whole private IP range.
        /// </summary>
        private static void PrivateScan()
        {
            var scan = new PrivateScan(IPGenerated);
            scan.Scan();
        }

        /// <summary>
        /// The IPAddressGenerated event handler.
        /// </summary>
        /// <param name="sender">The sender IPScanner base class.</param>
        /// <param name="e">The ScanArgs, containing the IP address generated.</param>
        private static void IPGenerated(object sender, ScanArgs e)
        {
            Task.Factory.StartNew(() =>
                {
                    if (IsPingable(e.IPAddress))
                        Console.WriteLine($"[{e.IPAddress}] Ping response received.");
                });
        }
        

        /// <summary>
        /// Quickly check if an IP address is pingable (Timeout = 2500).
        /// </summary>
        /// <param name="ip">The IP address to ping.</param>
        /// <returns>True if a ping response was received, false if otherwise.</returns>
        private static bool IsPingable(string ip)
        {
            using (var p = new Ping())
            {
                try
                {
                    var response = p.Send(ip, 1500);
                    if (response != null && response.Status == IPStatus.Success)
                        return true;
                }
                catch { }
            }

            return false;
        }
    }
}
