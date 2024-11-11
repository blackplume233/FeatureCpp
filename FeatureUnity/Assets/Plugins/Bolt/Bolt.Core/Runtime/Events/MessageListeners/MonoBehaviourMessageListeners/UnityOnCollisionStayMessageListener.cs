using UnityEngine;

namespace Bolt
{
    [AddComponentMenu("")]
    public sealed class UnityOnCollisionStayMessageListener : MessageListener
    {
        private void OnCollisionStay(Collision collision)
        {
            EventBus.Trigger(EventHooks.OnCollisionStay, gameObject, collision);
        }
    }
}