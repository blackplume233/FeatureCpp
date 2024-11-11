using UnityEngine;

namespace Ludiq
{
    public abstract class SerializedPropertyProvider<T> : ScriptableObject, ISerializedPropertyProvider
    {
        [SerializeField]
        protected T item;

        object ISerializedPropertyProvider.item
        {
            get
            {
                return item;
            }
            set
            {
                item = (T)value;
            }
        }
    }
}