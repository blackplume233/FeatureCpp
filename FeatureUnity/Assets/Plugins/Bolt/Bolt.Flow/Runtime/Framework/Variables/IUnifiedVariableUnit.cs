namespace Bolt
{
    public interface IUnifiedVariableUnit : IUnit
    {
        VariableKind kind { get; }
        ValueInput name { get; }
    }
}