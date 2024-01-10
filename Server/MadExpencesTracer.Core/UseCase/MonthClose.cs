using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using System.Globalization;

namespace MadExpenceTracker.Core.UseCase
{
    public class MonthClose : IMonthClose
    {
        private readonly IExpencePersistence _expencePersistence;
        private readonly IIncomesPersistence _incomePersistence;
        private readonly IAmountsPersistence _amountsPersistence;
        private readonly IConfigurationPersistence _configuration;
        private readonly IMonthIndexPersistence _indexPersistence;
        private readonly string CURRENT_MONTH = $"{DateTime.Now.Year}/{DateTime.Now.Month}";

        public MonthClose(
            IExpencePersistence expencePersistence,
            IIncomesPersistence incomePersistence,
            IAmountsPersistence amountsPersistence,
            IConfigurationPersistence configuration,
            IMonthIndexPersistence indexPersistence)
        {
            _expencePersistence = expencePersistence;
            _incomePersistence = incomePersistence;
            _amountsPersistence = amountsPersistence;
            _configuration = configuration;
            _indexPersistence = indexPersistence;
        }

        public MonthIndex CloseMonth(string monthToClose, Expences expences, Incomes incomes, Amount amount)
        {
            if(!DateTime.TryParseExact(monthToClose, "yyyy/MM", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime date))
            {
                throw new ArgumentException("month to close couldn't be parsed");
            }

            CheckIfExpencesMonthIsClosed(date.ToString("yyyy/M"));
            CheckIfIncomesMonthIsClosed(date.ToString("yyyy/M"));

            if (CreateNewExpencesCollection() &&
                CreateNewIncomesCollection() &&
                CloseExpencesMonth(date.ToString("yyyy/M")) &&
                CloseIncomesMonth(date.ToString("yyyy/M")))
            {
                amount = CreateAmount(amount);
                return CreateIndexEntry(expences, incomes, amount);
            }
            throw new MonthCloseException("failed to close the month");
        }

        private MonthIndex CreateIndexEntry(Expences expences, Incomes incomes, Amount amount)
        {
            byte savingsRate = _configuration.GetConfiguration().SavingsRate;
            MonthIndex monthIndex = new MonthIndex()
            {
                Id = Guid.NewGuid(),
                ExpencesId = expences.Id,
                IncomesId = incomes.Id,
                AmountsId = amount.Id,
                Month = $"{DateTime.Now.Year}/{DateTime.Now.Month}",
                SavingsRate = savingsRate
            };
            _indexPersistence.AddMonthIndex(monthIndex);
            return monthIndex;
        }

        private bool CreateNewExpencesCollection()
        {
            return _expencePersistence.CreateNewExpencesDocument(CURRENT_MONTH);
        }

        private bool CreateNewIncomesCollection()
        {
            return _incomePersistence.CreateNewIncomeDocument(CURRENT_MONTH);
        }

        private bool CloseExpencesMonth(string monthToClose)
        {
            return _expencePersistence.UpdateExpencesIsActive(false, monthToClose);
        }

        private bool CloseIncomesMonth(string monthToClose)
        {
            return _incomePersistence.UpdateIncomesIsActive(false, monthToClose);
        }

        private Amount CreateAmount(Amount amount) 
        {
            amount.Id = amount.Id == Guid.Empty ? Guid.NewGuid() : amount.Id;
            _amountsPersistence.AddAmount(amount);
            return amount;
        }

        private void CheckIfExpencesMonthIsClosed(string monthToClose)
        {
            if(!_expencePersistence.IsMonthClosed(monthToClose))
            {
                throw new InvalidOperationException("Month of expences is already closed");
            }
        }

        private void CheckIfIncomesMonthIsClosed(string monthToClose)
        {
            if (!_incomePersistence.IsMonthClosed(monthToClose))
            {
                throw new InvalidOperationException("Month of incomes is already closed");
            }
        }
    }
}
