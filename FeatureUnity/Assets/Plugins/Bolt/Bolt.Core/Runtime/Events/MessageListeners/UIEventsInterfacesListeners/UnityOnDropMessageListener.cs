using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnDropMessageListener : MessageListener, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnDrop, gameObject, eventData);
        }
    }
}