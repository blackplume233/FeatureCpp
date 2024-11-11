using UnityEngine;

namespace Ludiq
{
    public interface INodeWidget : IGraphElementWidget
    {
        Rect outerPosition { get; }
        Rect edgePosition { get; }
        Rect innerPosition { get; }
    }
}