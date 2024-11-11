using UnityEngine;

namespace Ludiq
{
    public interface IMachine : IGraphRoot, IGraphNester, IAotStubbable
    {
        IGraphData graphData { get; set; }

        GameObject threadSafeGameObject { get; }
    }
}