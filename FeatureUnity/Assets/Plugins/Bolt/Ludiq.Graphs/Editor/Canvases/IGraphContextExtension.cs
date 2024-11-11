using System.Collections.Generic;

namespace Ludiq
{
    public interface IGraphContextExtension : IDragAndDropHandler
    {
        IEnumerable<GraphContextMenuItem> contextMenuItems { get; }
    }
}