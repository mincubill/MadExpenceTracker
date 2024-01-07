using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Exceptions
{
    public class MonthCloseException : Exception
    {
        public MonthCloseException(string? message) : base(message)
        {
        }
    }
}
