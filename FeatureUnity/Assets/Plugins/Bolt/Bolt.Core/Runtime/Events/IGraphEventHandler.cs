using Ludiq;

namespace Bolt
{
    public interface IGraphEventHandler<TArgs>
    {
        EventHook GetHook(GraphReference reference);

        void Trigger(GraphReference reference, TArgs args);

        bool IsListening(GraphPointer pointer);
    }
}