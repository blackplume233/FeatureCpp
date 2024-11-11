using Ludiq;

namespace Bolt
{
    public interface IStateTransitionDebugData : IGraphElementDebugData
    {
        int lastBranchFrame { get; }

        float lastBranchTime { get; }
    }
}
