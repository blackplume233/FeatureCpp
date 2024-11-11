using System;

namespace Bolt
{
    public interface IUnitValuePortDefinition : IUnitPortDefinition
    {
        Type type { get; }
    }
}