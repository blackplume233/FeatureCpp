using System;

namespace Bolt
{
    public sealed class MissingValuePortInputException : Exception
    {
        public MissingValuePortInputException(string key) : base($"Missing input value for '{key}'.") { }
    }
}