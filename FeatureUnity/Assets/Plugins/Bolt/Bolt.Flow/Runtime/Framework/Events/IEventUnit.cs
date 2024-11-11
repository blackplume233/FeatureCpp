using System;

namespace Bolt
{
    public interface IEventUnit : IUnit, IGraphEventListener
    {
        bool coroutine { get; }
    }
    public interface IGameObjectEventUnit : IEventUnit
    {
        Type MessageListenerType { get; }
    }
}
