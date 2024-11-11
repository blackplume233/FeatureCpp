using System;
using Ludiq.FullSerializer;

namespace Ludiq
{
    public class SerializationVersionAttribute : fsObjectAttribute
    {
        public SerializationVersionAttribute(string versionString, params Type[] previousModels) : base(versionString, previousModels) { }
    }
}