using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using IPScan;

namespace Examples
{
    internal class Examples
    {
        private static void Main(string[] args)
        {
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
        /// Generates the whole 192.X.X.X range with modified active worker threads.
        /// </summary>
        private static void DefaultScanWithModifiedThreadCount()
        {
            var scan = new DefaultScan();
            scan.IPAddressGenerated += IPGenerated;
            scan.MinimumThreadCount = 50;
            scan.MaximumThreadCount = 100;

            scan.Scan("192.X.X.X");
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
        /// Generates the whole public IP range with min/max threads set.
        /// </summary>
        private static void PublicScanWithIncreasedThreads()
        {
            // Sets the minimum number of active worker threads to 100.
            var scan = new PublicScan(IPGenerated, 100);
            scan.Scan();

            // Sets the minimum number of active worker threads to 100 and the maximum to 150.
            var scan2 = new PublicScan(IPGenerated, 100, 150);
            scan2.Scan();
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
        /// Generates the whole private IP range with min/max threads set.
        /// </summary>
        private static void PrivateScanWithIncreasedThreads()
        {
            // Sets the minimum number of active worker threads to 100.
            var scan = new PrivateScan(IPGenerated, 100);
            scan.Scan();

            // Sets the minimum number of active worker threads to 100 and the maximum to 150.
            var scan2 = new PrivateScan(IPGenerated, 100, 150);
            scan2.Scan();
        }

        /// <summary>
        /// The IPAddressGenerated event handler.
        /// </summary>
        /// <param name="sender">The sender IPScanner base class.</param>
        /// <param name="e">The ScanArgs, containing the IP address generated.</param>
        private static void IPGenerated(object sender, ScanArgs e)
        {
            if (IsPingable(e.IPAddress))
                Console.WriteLine($"[{e.IPAddress}] Ping response received.");
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
                    var response = p.Send(IPAddress.Parse(ip), 1500);
                    if (response != null && response.Status == IPStatus.Success)
                        return true;
                }
                catch { p.Dispose(); }
            }

            return false;
        }
    }
}
