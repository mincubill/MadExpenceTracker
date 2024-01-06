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
        Amount GetAmount(Guid expencesId, Guid incomesId);
        Amount GetAmount(Guid id);
        Amounts GetAmounts();
        Amounts Create(Amount amount);
    }
}
