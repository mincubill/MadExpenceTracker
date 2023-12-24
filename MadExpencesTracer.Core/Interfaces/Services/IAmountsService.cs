using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IAmountsService
    {
        Amounts GetAmounts(Guid id);
        Amount GetAmount(Guid id);
        Amounts Create(Amount amount);
    }
}
