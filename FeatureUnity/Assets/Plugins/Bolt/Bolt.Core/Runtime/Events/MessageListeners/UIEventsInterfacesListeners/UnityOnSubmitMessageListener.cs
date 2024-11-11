using UnityEngine.EventSystems;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnSubmitMessageListener : MessageListener, ISubmitHandler
    {
        public void OnSubmit(BaseEventData eventData)
        {
            EventBus.Trigger(EventHooks.OnSubmit, gameObject, eventData);
        }
    }
}