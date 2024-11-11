namespace Ludiq
{
    [Inspector(typeof(ushort))]
    public class UshortInspector : DiscreteNumberInspector<ushort>
    {
        public UshortInspector(Metadata metadata) : base(metadata) { }
    }
}