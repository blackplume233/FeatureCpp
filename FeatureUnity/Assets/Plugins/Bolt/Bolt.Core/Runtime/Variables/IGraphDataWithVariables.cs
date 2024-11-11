using Ludiq;

namespace Bolt
{
    public interface IGraphDataWithVariables : IGraphData
    {
        VariableDeclarations variables { get; }
    }
}
