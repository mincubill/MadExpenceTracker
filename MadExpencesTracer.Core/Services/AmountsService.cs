using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using System.Net.WebSockets;

namespace MadExpenceTracker.Core.Services
{
    public class AmountsService : IAmountsService
    {
        private readonly IAmountsPersistence _amountPersistence;
        private readonly IExpencePersistence _expencePersistence;
        private readonly IIncomePersistence _incomePersistence;
        private readonly IConfigurationPersistence _configurationPersistence;

        public AmountsService(IAmountsPersistence amountPersistence, IExpencePersistence expencePersistence, IIncomePersistence incomePersistence, IConfigurationPersistence configurationPersistence)
        {
            _amountPersistence = amountPersistence;
            _expencePersistence = expencePersistence;
            _incomePersistence = incomePersistence;
            _configurationPersistence = configurationPersistence;
        }

        public Amount GetAmount(Guid expencesId, Guid incomesId)
        {
            IEnumerable<Expence> expences = _expencePersistence.Get(expencesId).Expence;
            long totalBaseExpences = expences.Where(e => e.ExpenceType == ExpenceType.Base).Sum(e => e.Amount);
            long totalAditionalExpences = expences.Where(e => e.ExpenceType == ExpenceType.Aditional).Sum(e => e.Amount);
            long totalIncomes = _incomePersistence.Get(incomesId).Income.Sum(i => i.Amount);
            byte savingRate = _configurationPersistence.GetConfiguration().SavingsRate;
            return new Amount
            {
                Id = Guid.NewGuid(),
                TotalBaseExpences = totalBaseExpences,
                TotalAditionalExpences = totalAditionalExpences,
                TotalIncomes = totalIncomes,
                Savings = Convert.ToInt64(totalIncomes * (savingRate/100f))
            };
        }

        public Amounts GetAmounts()
        {
            return _amountPersistence.GetAmounts().FirstOrDefault() ?? new Amounts();
        }

        public Amount GetAmount(Guid id)
        {
            return _amountPersistence.GetAmount(id).Amount.First(a => a.Id == id);
        }

        public Amounts Create(Amount amount)
        {
            return _amountPersistence.AddAmount(amount);
        }
    }
}
