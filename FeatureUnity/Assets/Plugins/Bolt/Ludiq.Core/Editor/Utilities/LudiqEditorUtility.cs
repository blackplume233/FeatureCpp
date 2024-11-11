using UnityObject = UnityEngine.Object;

namespace Ludiq
{
    public static class LudiqEditorUtility
    {
        public static OverrideStack<UnityObject> editedObject { get; } = new OverrideStack<UnityObject>(null);
    }
}