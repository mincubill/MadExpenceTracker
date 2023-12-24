using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;

namespace MadExpenceTracker.Core.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomePersistence _persistence;

        public IncomeService(IIncomePersistence persistence)
        {
            _persistence = persistence;
        }

        public IEnumerable<Incomes> GetAll()
        {
            return _persistence.GetAll();
        }

        public Incomes GetIncomes(Guid id)
        {
            return _persistence.Get(id);
        }

        public Income GetIncome(Guid id)
        {
            return _persistence.Get(id).Income.First(e => e.Id == id);
        }

        public Incomes Create(Income income)
        {
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
                throw new Exception("Error updating income");
            }
        }

        public bool Delete(Guid id)
        {
            return _persistence.Delete(id);
        }

        public bool CloseMonth(string month)
        {
            return _persistence.UpdateExpencesIsActive(false, month);
        }
    }
}
