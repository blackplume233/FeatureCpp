namespace Ludiq
{
    [Inspector(typeof(ulong))]
    public class UlongInspector : ContinuousNumberInspector<ulong>
    {
        public UlongInspector(Metadata metadata) : base(metadata) { }
    }
}