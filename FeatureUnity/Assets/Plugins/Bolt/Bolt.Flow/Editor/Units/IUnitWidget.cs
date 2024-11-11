using Ludiq;

namespace Bolt
{
    public interface IUnitWidget : IGraphElementWidget
    {
        IUnit unit { get; }

        Inspector GetPortInspector(IUnitPort port, Metadata metadata);
    }
}
