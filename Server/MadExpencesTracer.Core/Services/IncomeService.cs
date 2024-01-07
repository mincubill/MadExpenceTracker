using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;

namespace MadExpenceTracker.Core.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomesPersistence _persistence;

        public IncomeService(IIncomesPersistence persistence)
        {
            _persistence = persistence;
        }

        public IEnumerable<Incomes> GetAll()
        {
            return _persistence.GetAll() ?? new List<Incomes>();
        }

        public Incomes GetIncomes(Guid id)
        {
            return _persistence.Get(id) ?? throw new NotFoundException("Incomes not found");
        }

        public Incomes GetIncomes(bool isActive)
        {
            return _persistence.GetByActive(isActive) ?? throw new NotFoundException("Incomes not found");
        }

        public Income GetIncome(Guid id)
        {
            return _persistence.GetIncome(id) ?? throw new NotFoundException("Income not found");
        }

        public Incomes Create(Income income)
        {
            income.Id = income.Id == Guid.Empty ? Guid.NewGuid() : income.Id;
            return _persistence.AddIncome(income);
        }

        public bool CreateNewMonth()
        {
            return _persistence.CreateNewIncomeDocument($"{DateTime.Now.Year}/{DateTime.Now.Month}");
        }

        public Income Update(Income income)
        {
            if(_persistence.Update(income))
            {
                return income;
            }
            else
            {
                throw new CannotUpdateException("Error updating income");
            }
        }

        public bool Delete(Guid id)
        {
            return _persistence.Delete(id);
        }

        public bool CloseMonth(string month)
        {
            return _persistence.UpdateIncomesIsActive(false, month);
        }
    }
}
