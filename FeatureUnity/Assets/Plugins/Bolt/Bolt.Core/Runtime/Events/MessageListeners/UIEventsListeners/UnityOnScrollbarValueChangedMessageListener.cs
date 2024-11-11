using UnityEngine.UI;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnScrollbarValueChangedMessageListener : MessageListener
    {
        private void Start()
        {
            GetComponent<Scrollbar>()?.onValueChanged?.AddListener((value) =>
                EventBus.Trigger(EventHooks.OnScrollbarValueChanged, gameObject, value));
        }
    }
}