using System.Collections.Generic;

namespace IPScan
{
    /// <summary>
    /// This class can be used to exclude certain IP masks from a given scan mask.
    /// </summary>
    public class BlacklistScan : IPScanner
    {
        private readonly List<string> _blacklist;

        /// <summary>
        /// Constructor for BlacklistScan.
        /// </summary>
        /// <param name="blacklist">The string list of IP masks to skip, these masks will not be generated.</param>
        public BlacklistScan(List<string> blacklist)
        {
            _blacklist = blacklist.ConvertAll(s => s.ToLower().Replace(".x", ""));
        }
        
        internal override bool ShouldProcess(string mask)
        {
            return !_blacklist.Contains(mask);
        }
    }
}
