using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnSelectMessageListener : MessageListener, ISelectHandler
    {
        public void OnSelect(BaseEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnSelect, gameObject, eventData);
        }
    }
}