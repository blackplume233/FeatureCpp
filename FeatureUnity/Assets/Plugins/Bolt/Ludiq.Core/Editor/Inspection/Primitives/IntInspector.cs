namespace Ludiq
{
    [Inspector(typeof(int))]
    public class IntInspector : DiscreteNumberInspector<int>
    {
        public IntInspector(Metadata metadata) : base(metadata) { }
    }
}