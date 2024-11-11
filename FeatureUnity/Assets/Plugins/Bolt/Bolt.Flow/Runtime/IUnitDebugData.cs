using Ludiq;

namespace Bolt
{
    public interface IUnitDebugData : IGraphElementDebugData
    {
        int lastInvokeFrame { get; set; }

        float lastInvokeTime { get; set; }
    }
}
