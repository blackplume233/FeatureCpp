using UnityEngine;

namespace Bolt
{
    [AddComponentMenu("")]
    public sealed class UnityOnTriggerStayMessageListener : MessageListener
    {
        private void OnTriggerStay(Collider other)
        {
            EventBus.Trigger(EventHooks.OnTriggerStay, gameObject, other);
        }
    }
}