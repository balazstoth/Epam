using System;

namespace Airports
{
    class RegexNotFoundException : Exception
    {
        public RegexNotFoundException(string msg) : base(msg)
        {
        }
    }
}
