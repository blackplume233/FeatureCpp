namespace Ludiq
{
    [Inspector(typeof(long))]
    public class LongInspector : ContinuousNumberInspector<long>
    {
        public LongInspector(Metadata metadata) : base(metadata) { }
    }
}