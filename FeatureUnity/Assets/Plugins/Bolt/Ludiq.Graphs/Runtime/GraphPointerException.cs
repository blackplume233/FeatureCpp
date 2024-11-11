using System;

namespace Ludiq
{
    public sealed class GraphPointerException : Exception
    {
        public GraphPointer pointer { get; }

        public GraphPointerException(string message, GraphPointer pointer) : base(message + "\n" + pointer)
        {
            this.pointer = pointer;
        }
    }
}
