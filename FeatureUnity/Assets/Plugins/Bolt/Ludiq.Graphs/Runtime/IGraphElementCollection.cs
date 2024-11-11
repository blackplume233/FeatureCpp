using System;

namespace Ludiq
{
    public interface IGraphElementCollection<T> : IKeyedCollection<Guid, T>, INotifyCollectionChanged<T> where T : IGraphElement
    {

    }
}
