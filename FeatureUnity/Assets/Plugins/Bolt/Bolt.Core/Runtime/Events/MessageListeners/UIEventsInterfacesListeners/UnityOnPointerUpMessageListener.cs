using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnPointerUpMessageListener : MessageListener, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnPointerUp, gameObject, eventData);
        }
    }
}