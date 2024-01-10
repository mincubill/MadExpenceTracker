using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Exceptions
{
    public class NoConfigurationException : Exception
    {
        public NoConfigurationException(string? message) : base(message)
        {
        }
    }
}
