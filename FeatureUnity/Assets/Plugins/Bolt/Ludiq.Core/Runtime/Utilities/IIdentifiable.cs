using System;

namespace Ludiq
{
    public interface IIdentifiable
    {
        Guid guid { get; }
    }
}