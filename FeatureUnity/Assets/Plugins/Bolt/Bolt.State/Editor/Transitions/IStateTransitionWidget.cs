using Ludiq;

namespace Bolt
{
    public interface IStateTransitionWidget : INodeWidget
    {
        Edge sourceEdge { get; }
    }
}