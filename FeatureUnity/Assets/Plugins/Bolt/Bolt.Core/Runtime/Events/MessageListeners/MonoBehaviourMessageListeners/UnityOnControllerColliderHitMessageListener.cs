using UnityEngine;

namespace Bolt
{
    [AddComponentMenu("")]
    public sealed class UnityOnControllerColliderHitMessageListener : MessageListener
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            EventBus.Trigger(EventHooks.OnControllerColliderHit, gameObject, hit);
        }
    }
}