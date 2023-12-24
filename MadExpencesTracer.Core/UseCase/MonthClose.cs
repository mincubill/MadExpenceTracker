using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.UseCase
{
    public class MonthClose : IMonthClose
    {
        private readonly IExpencesService _expencesService;
        private readonly IIncomeService _incomeService;
        private readonly IAmountsService _amountsService;
        private readonly IConfigurationService _configurationService;
        private readonly IMonthIndexingService _monthIndexingService;

        public MonthClose(IExpencesService expencesService, 
            IIncomeService incomeService, 
            IAmountsService amountsService,
            IConfigurationService configurationService,
            IMonthIndexingService monthIndexingService)
        {
            _expencesService = expencesService;
            _incomeService = incomeService;
            _amountsService = amountsService;
            _configurationService = configurationService;
            _monthIndexingService = monthIndexingService;
        }

        public bool CloseMonth(Expences expences, Incomes incomes, Amounts amounts)
        {
           
            throw new NotImplementedException();
        }

        private MonthIndex CreateIndexEntry(Expences expences, Incomes incomes, Amounts amounts)
        {
            byte savingsRate = _configurationService.GetConfiguration().SavingsRate;
            return new MonthIndex()
            {
                Id = Guid.NewGuid(),
                ExpencesId = expences.Id,
                IncomesId = incomes.Id,
                AmountsId = amounts.Id,
                Month = $"{DateTime.Now.Year}/{DateTime.Now.Month}",
                SavingsRate = savingsRate
            };
        }

        private bool CreateNewExpencesCollection()
        {
            return false;
        }

        private bool CreateNewIncomesCollection()
        {
            return false;
        }

        private bool CreateNewAmountsCollection()
        {
            return false;
        }
    }
}
