using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Exceptions
{
    public class CannotUpdateException : Exception
    {
        public CannotUpdateException(string? message) : base(message)
        {
        }
    }
}
