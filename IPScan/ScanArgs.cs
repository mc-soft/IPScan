using System;

namespace IPScan
{
    public class ScanArgs : EventArgs
    {
        public ScanArgs(string ip)
        {
            IPAddress = ip;
        }
        
        public string IPAddress { get; }
    }
}
