using UnityEngine.UI;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnButtonClickMessageListener : MessageListener
    {
        private void Start()
        {
            GetComponent<Button>()?.onClick
                ?.AddListener(() => EventBus.Trigger(EventHooks.OnButtonClick, gameObject));
        }
    }
}