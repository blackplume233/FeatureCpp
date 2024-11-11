using System;

namespace Ludiq
{
    public class InvalidImplementationException : Exception
    {
        public InvalidImplementationException() : base() { }
        public InvalidImplementationException(string message) : base(message) { }
    }
}