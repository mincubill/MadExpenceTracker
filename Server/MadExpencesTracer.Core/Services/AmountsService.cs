using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Util;

namespace MadExpenceTracker.Core.Services
{
    public class AmountsService : IAmountsService
    {
        private readonly IAmountsPersistence _amountPersistence;
        private readonly IExpencePersistence _expencePersistence;
        private readonly IIncomesPersistence _incomePersistence;
        private readonly IConfigurationPersistence _configurationPersistence;

        public AmountsService(IAmountsPersistence amountPersistence, IExpencePersistence expencePersistence, IIncomesPersistence incomePersistence, IConfigurationPersistence configurationPersistence)
        {
            _amountPersistence = amountPersistence;
            _expencePersistence = expencePersistence;
            _incomePersistence = incomePersistence;
            _configurationPersistence = configurationPersistence;
        }

        public Amount GetAmount(Guid expencesId, Guid incomesId)
        {
            Expences expences = _expencePersistence.Get(expencesId) ?? throw new NotFoundException("Expences not found");
            Incomes incomes = _incomePersistence.Get(incomesId) ?? throw new NotFoundException("Incomes not found");
            Amount calculatedAmount = Calculations.CalculateAmounts(expences, incomes, _configurationPersistence.GetConfiguration());
            return calculatedAmount;
        }

        public Amount GetAmount(Guid id)
        {
            Amounts foundAmounts = _amountPersistence.GetAmounts(id) ?? throw new NotFoundException("Amount not found");
            Amount foundAmount = foundAmounts.Amount?.FirstOrDefault(a => a.Id == id) ?? throw new NotFoundException("Amount not found");
            foundAmount.RemainingBaseExpences = foundAmount.SugestedBaseExpences - foundAmount.TotalBaseExpences;
            foundAmount.RemainingAditionalExpences = foundAmount.SugestedAditionalExpences - foundAmount.TotalAditionalExpences;
            return foundAmount;
        }

        public Amounts GetAmounts()
        {
            Amounts amounts = _amountPersistence.GetAmounts()?.FirstOrDefault() ?? new Amounts();
            amounts.Amount = amounts.Amount?.Select(a => new Amount
            {
                Id = a.Id,
                Savings = a.Savings,
                SugestedAditionalExpences = a.SugestedAditionalExpences,
                TotalAditionalExpences = a.TotalAditionalExpences,
                SugestedBaseExpences = a.SugestedBaseExpences,
                TotalBaseExpences = a.TotalBaseExpences,
                TotalIncomes = a.TotalIncomes,
                RemainingAditionalExpences = a.SugestedAditionalExpences - a.TotalAditionalExpences,
                RemainingBaseExpences = a.SugestedBaseExpences - a.TotalBaseExpences
            });
            return amounts;
        }

        public Amounts Create(Amount amount)
        {
            return _amountPersistence.AddAmount(amount);
        }
    }
}
