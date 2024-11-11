using UnityEngine.UI;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnToggleValueChangedMessageListener : MessageListener
    {
        private void Start() => GetComponent<Toggle>()?.onValueChanged?.AddListener((value) =>
            EventBus.Trigger(EventHooks.OnToggleValueChanged, gameObject, value));
    }
}