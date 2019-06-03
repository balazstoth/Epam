using System;
using System.Collections.Generic;
using System.Text;

namespace Airports
{
    class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string msg) : base(msg)
        {
        }
    }
}
