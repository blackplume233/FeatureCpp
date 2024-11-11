namespace Ludiq
{
    [Inspector(typeof(short))]
    public class ShortInspector : DiscreteNumberInspector<short>
    {
        public ShortInspector(Metadata metadata) : base(metadata) { }
    }
}