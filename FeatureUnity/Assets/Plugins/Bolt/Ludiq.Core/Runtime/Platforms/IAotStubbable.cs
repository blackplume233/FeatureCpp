using System.Collections.Generic;

namespace Ludiq
{
    public interface IAotStubbable
    {
        IEnumerable<object> aotStubs { get; }
    }
}