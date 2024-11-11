using System;

namespace Ludiq
{
    public class InvalidConnectionException : Exception
    {
        public InvalidConnectionException() : base("") { }
        public InvalidConnectionException(string message) : base(message) { }
    }
}