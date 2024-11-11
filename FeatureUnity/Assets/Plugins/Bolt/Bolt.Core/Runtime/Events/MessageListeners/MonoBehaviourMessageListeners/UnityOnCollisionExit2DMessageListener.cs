using UnityEngine;

namespace Bolt
{
    [AddComponentMenu("")]
    public sealed class UnityOnCollisionExit2DMessageListener : MessageListener
    {
        private void OnCollisionExit2D(Collision2D collision)
        {
            EventBus.Trigger(EventHooks.OnCollisionExit2D, gameObject, collision);
        }
    }
}