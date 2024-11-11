using Ludiq;

namespace Bolt
{
    public interface IStateWidget : IGraphElementWidget
    {
        IState state { get; }
    }
}
