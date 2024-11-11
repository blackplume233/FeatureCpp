using System.Collections;
using System.Collections.Generic;

namespace Ludiq
{
    public interface IGraphDebugData
    {
        IGraphElementDebugData GetOrCreateElementData(IGraphElementWithDebugData element);

        IGraphDebugData GetOrCreateChildGraphData(IGraphParentElement element);

        IEnumerable<IGraphElementDebugData> elementsData { get; }
    }
}
