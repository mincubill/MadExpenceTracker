using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Services
{
    public class AmountsService : IAmountsService
    {
        private readonly IAmountsPersistence _persistence;

        public AmountsService(IAmountsPersistence persistence)
        {
            _persistence = persistence;
        }

        public Amounts GetAmounts(Guid id)
        {
            return _persistence.GetAmounts().First(a => a.Id == id);

        }

        public Amount GetAmount(Guid id)
        {
            return _persistence.GetAmount(id).Amount.First(a => a.Id == id);
        }

        public Amounts Create(Amount amount)
        {
            return _persistence.AddAmount(amount);
        }

    }
}
