using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Util;
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

        public MonthIndex CloseMonth(string monthToClose, Guid expencesId, Guid incomesId)
        {
            if(!DateTime.TryParseExact(monthToClose, "yyyy/M", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime date))
            {
                throw new ArgumentException("month to close couldn't be parsed");
            }

            Configuration config = _configuration.GetConfiguration();
            Expences expences = _expencePersistence.Get(expencesId) ?? throw new NotFoundException("");
            Incomes incomes = _incomePersistence.Get(incomesId) ?? throw new NotFoundException("");
            Amount amount = CalculateAmount(expences, incomes, config);

            CheckIfExpencesMonthIsClosed(date.ToString("yyyy/M"));
            CheckIfIncomesMonthIsClosed(date.ToString("yyyy/M"));

            if (CreateNewExpencesCollection() &&
                CreateNewIncomesCollection() &&
                CloseExpencesMonth(date.ToString("yyyy/M")) &&
                CloseIncomesMonth(date.ToString("yyyy/M")))
            {
                CreateAmount(amount);
                return CreateIndexEntry(monthToClose, expencesId, incomesId, amount, config);
            }
            throw new MonthCloseException("failed to close the month");
        }

        private MonthIndex CreateIndexEntry(string monthToClose, Guid expencesId, Guid incomesId, Amount amount, Configuration config)
        {
            MonthIndex monthIndex = new MonthIndex()
            {
                Id = Guid.NewGuid(),
                ExpencesId = expencesId,
                IncomesId = incomesId,
                AmountsId = amount.Id,
                Month = monthToClose,
                SavingsRate = config.SavingsRate,
                AditionalExpencesRate= config.AditionalExpencesRate,
                BaseExpencesRate = config.BaseExpencesRate,
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
            var wea = _expencePersistence.IsMonthClosed(monthToClose);
            if (!_expencePersistence.IsMonthClosed(monthToClose))
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

        private Amount CalculateAmount(Expences expences, Incomes incomes, Configuration configuration)
        {
            return Calculations.CalculateAmounts(expences, incomes, configuration);
        }

    }
}
