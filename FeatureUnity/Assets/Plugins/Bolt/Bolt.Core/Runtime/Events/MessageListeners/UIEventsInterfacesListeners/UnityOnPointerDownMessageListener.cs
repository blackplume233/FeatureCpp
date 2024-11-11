using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnPointerDownMessageListener : MessageListener, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnPointerDown, gameObject, eventData);
        }
    }
}