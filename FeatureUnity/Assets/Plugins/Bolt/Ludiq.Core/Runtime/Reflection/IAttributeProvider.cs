using System;

namespace Ludiq
{
    public interface IAttributeProvider
    {
        Attribute[] GetCustomAttributes(bool inherit);
    }
}