using Ludiq;

namespace Bolt
{
    [Inspector(typeof(IUnit))]
    public class UnitInspector : ReflectedInspector
    {
        public UnitInspector(Metadata metadata) : base(metadata) { }
    }
}