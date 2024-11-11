using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnDragMessageListener : MessageListener, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnDrag, gameObject, eventData);
        }
    }
}