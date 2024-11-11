using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnCancelMessageListener : MessageListener, ICancelHandler
    {
        public void OnCancel(BaseEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnCancel, gameObject, eventData);
        }
    }
}