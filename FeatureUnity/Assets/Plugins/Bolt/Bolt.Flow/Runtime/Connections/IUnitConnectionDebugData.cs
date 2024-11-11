using Ludiq;

namespace Bolt
{
    public interface IUnitConnectionDebugData : IGraphElementDebugData
    {
        int lastInvokeFrame { get; set; }

        float lastInvokeTime { get; set; }
    }
}
