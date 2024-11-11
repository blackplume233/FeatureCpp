using System.Collections.Generic;

namespace Ludiq
{
    public class ConnectionCollection<TConnection, TSource, TDestination> : ConnectionCollectionBase<TConnection, TSource, TDestination, List<TConnection>>
        where TConnection : IConnection<TSource, TDestination>
    {
        public ConnectionCollection() : base(new List<TConnection>()) { }
    }
}