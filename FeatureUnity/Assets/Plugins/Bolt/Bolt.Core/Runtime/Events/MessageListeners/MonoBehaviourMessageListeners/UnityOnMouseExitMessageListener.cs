namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnMouseExitMessageListener : MessageListener
    {
        private void OnMouseExit()
        {
            EventBus.Trigger(EventHooks.OnMouseExit, gameObject);
        }
    }
}