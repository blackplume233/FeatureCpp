using Ludiq;

namespace Bolt
{
    [TypeIconPriority]
    public interface ISelectUnit : IUnit
    {
        ValueOutput selection { get; }
    }
}