using System.Collections.Generic;

namespace Ludiq
{
    public class GraphElementAnalysis : IGraphElementAnalysis
    {
        public List<Warning> warnings { get; set; } = new List<Warning>();
    }
}
