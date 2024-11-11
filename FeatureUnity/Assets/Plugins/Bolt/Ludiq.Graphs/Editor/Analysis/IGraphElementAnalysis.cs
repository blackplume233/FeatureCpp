using System.Collections.Generic;

namespace Ludiq
{
    public interface IGraphElementAnalysis : IAnalysis
    {
        List<Warning> warnings { get; }
    }
}
