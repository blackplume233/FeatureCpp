using UnityEngine.UI;

namespace Bolt
{
    [UnityEngine.AddComponentMenu("")]
    public sealed class UnityOnInputFieldValueChangedMessageListener : MessageListener
    {
        private void Start()
        {
            GetComponent<InputField>()?.onValueChanged?.AddListener((value) =>
                EventBus.Trigger(EventHooks.OnInputFieldValueChanged, gameObject, value));
        }
    }
}