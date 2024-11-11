using System;

namespace Bolt
{
    public interface IUnitValuePort : IUnitPort
    {
        Type type { get; }
    }
}