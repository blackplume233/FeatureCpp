using System;

namespace Ludiq
{
    public interface INotifyCollectionChanged<T>
    {
        event Action<T> ItemAdded;

        event Action<T> ItemRemoved;

        event Action CollectionChanged;
    }
}
