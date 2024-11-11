using Ludiq;

namespace Bolt
{
    [TypeIconPriority]
    public interface IBranchUnit : IUnit
    {
        ControlInput enter { get; }
    }
}