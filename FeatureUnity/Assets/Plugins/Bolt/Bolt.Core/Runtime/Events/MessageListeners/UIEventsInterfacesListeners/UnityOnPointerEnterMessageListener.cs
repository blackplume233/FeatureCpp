using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnPointerEnterMessageListener : MessageListener, IPointerEnterHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnPointerEnter, gameObject, eventData);
        }
    }
}