using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnDeselectMessageListener : MessageListener, IDeselectHandler
    {
        public void OnDeselect(BaseEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnDeselect, gameObject, eventData);
        }
    }
}