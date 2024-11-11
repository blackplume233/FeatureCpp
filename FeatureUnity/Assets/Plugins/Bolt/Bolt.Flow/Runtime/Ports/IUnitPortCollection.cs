using Ludiq;

namespace Bolt
{
    public interface IUnitPortCollection<TPort> : IKeyedCollection<string, TPort> where TPort : IUnitPort
    {
        TPort Single();
    }
}