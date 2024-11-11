using Ludiq;

namespace Bolt
{
    public interface IStateDebugData : IGraphElementDebugData
    {
        int lastEnterFrame { get; }

        float lastExitTime { get; }
    }
}
