using Ludiq.FullSerializer;

namespace Ludiq
{
    public class SerializeAsAttribute : fsPropertyAttribute
    {
        public SerializeAsAttribute(string name) : base(name) { }
    }
}