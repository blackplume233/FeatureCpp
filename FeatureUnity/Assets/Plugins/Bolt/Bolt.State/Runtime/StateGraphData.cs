using Ludiq;

namespace Bolt
{
    public sealed class StateGraphData : GraphData<StateGraph>, IGraphEventListenerData
    {
        public bool isListening { get; set; }

        public StateGraphData(StateGraph definition) : base(definition)
        {

        }
    }
}
