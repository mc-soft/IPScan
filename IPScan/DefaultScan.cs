namespace IPScan
{
    /// <summary>
    /// The default scan class, provides no conditional logic regarding masks.
    /// </summary>
    public class DefaultScan : IPScanner
    {
        internal override bool ShouldProcess(string mask)
        {
            return true;
        }
    }
}
