using System;

namespace Bolt
{
    /// <summary>
    /// Called when a collider exits the trigger.
    /// </summary>
    public sealed class OnTriggerExit : TriggerEventUnit
    {
        public override Type MessageListenerType => typeof(UnityOnTriggerExitMessageListener);
        protected override string hookName => EventHooks.OnTriggerExit;
    }
}