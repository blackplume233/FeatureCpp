using System;

namespace Ludiq
{
    public interface IGraphElementDebugData
    {
        // Being lazy with the interfaces here to simplify things
        Exception runtimeException { get; set; }
    }
}