using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPScan
{
    /// <summary>
    /// The base scanner class containing the iteration logic.
    /// </summary>
    public abstract class IPScanner
    {
        /// <summary>
        /// Delegate for the IPAddressGenerated event.
        /// </summary>
        /// <param name="sender">The base IPScanner sender.</param>
        /// <param name="args">The IPScan arguments containing the IP address.</param>
        public delegate void IPScanHandler(object sender, ScanArgs args);

        /// <summary>
        /// The event that will be triggered when a new IP address has been generated using the provided mask(s).
        /// </summary>
        public event IPScanHandler IPAddressGenerated;
        
        /// <summary>
        /// Generates IP addresses based on the given IP mask.
        /// </summary>
        /// <param name="mask">The IP mask you wish to generate.</param>
        public void Scan(string mask)
        {
            if (IPAddressGenerated == null)
                throw new NullReferenceException("IPAddressGenerated event not handled.");

            ScanMask(mask.ToLower()).Wait();
        }

        /// <summary>
        /// Generates IP addresses based on a list of IP masks.
        /// </summary>
        /// <param name="masks">A string list of IP masks you wish to generate.</param>
        public Task Scan(List<string> masks)
        {
            if (IPAddressGenerated == null)
                throw new NullReferenceException("IPAddressGenerated event not handled.");

            var tasks = masks.Select(mask => Task.Factory.StartNew(() => Scan(mask))).ToList();

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Override this method with logic on whether the mask should be processed or not.
        /// </summary>
        /// <param name="mask">The mask to be checked.</param>
        /// <returns>True if the mask will be used or false if it will be skipped.</returns>
        internal abstract bool ShouldProcess(string mask);

        /// <summary>
        /// Performs recursive iterations of the given IP mask.
        /// </summary>
        /// <param name="mask">The IP mask to iterate, any X values will be iterated through 0-255.</param>
        private Task ScanMask(string mask)
        {
            int index = mask.IndexOf("x") + 1;
            string prefix = mask.Substring(0, index);
            string suffix = mask.Substring(index);

            var tasks = new List<Task>();

            for (int i = 0; i <= 255; i++)
            {
                string address = prefix.Replace("x", i.ToString());

                if (!ShouldProcess(address))
                    continue;

                if (suffix == string.Empty)
                {
                    IPAddressGenerated(this, new ScanArgs(address));
                }
                else
                {
                    tasks.Add(Task.Factory.StartNew(() => ScanMask(address + suffix)));
                }
            }
            
            return Task.WhenAll(tasks);
        }
    }
}
