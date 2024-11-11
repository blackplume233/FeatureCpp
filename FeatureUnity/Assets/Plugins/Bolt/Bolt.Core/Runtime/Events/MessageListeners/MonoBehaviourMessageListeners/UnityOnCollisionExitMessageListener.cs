using UnityEngine;

namespace Bolt
{
    [AddComponentMenu("")]
    public sealed class UnityOnCollisionExitMessageListener : MessageListener
    {
        private void OnCollisionExit(Collision collision)
        {
            EventBus.Trigger(EventHooks.OnCollisionExit, gameObject, collision);
        }
    }
}