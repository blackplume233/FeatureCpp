using Ludiq;

namespace Bolt
{
    public interface IGraphEventListenerData : IGraphData
    {
        bool isListening { get; }
    }
}
