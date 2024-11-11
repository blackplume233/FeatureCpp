using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnEndDragMessageListener : MessageListener, IEndDragHandler
    {
        public void OnEndDrag(PointerEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnEndDrag, gameObject, eventData);
        }
    }
}