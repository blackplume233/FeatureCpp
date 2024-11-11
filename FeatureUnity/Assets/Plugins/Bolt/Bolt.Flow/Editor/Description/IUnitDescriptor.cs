using Ludiq;

namespace Bolt
{
    public interface IUnitDescriptor : IDescriptor
    {
        IUnit unit { get; }

        new UnitDescription description { get; }

        string Title();

        string ShortTitle();

        string Surtitle();

        string Subtitle();

        string Summary();

        EditorTexture Icon();

        void DescribePort(IUnitPort port, UnitPortDescription description);
    }
}