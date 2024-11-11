using System;
using System.Collections.Generic;

namespace Ludiq
{
    public interface IMergedCollection<T> : ICollection<T>
    {
        bool Includes<TI>() where TI : T;
        bool Includes(Type elementType);
    }
}