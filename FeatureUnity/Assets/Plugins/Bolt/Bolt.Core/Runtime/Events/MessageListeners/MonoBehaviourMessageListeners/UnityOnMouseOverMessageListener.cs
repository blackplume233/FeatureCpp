namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnMouseOverMessageListener : MessageListener
    {
        private void OnMouseOver()
        {
            EventBus.Trigger(EventHooks.OnMouseOver, gameObject);
        }
    }
}