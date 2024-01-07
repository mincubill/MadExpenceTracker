using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Exceptions
{
    internal class MapperException : Exception
    {
        public MapperException(string? message) : base(message)
        {
        }
    }
}
