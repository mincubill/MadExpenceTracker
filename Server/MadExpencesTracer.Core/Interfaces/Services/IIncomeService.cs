using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.Services
{
    public interface IIncomeService
    {
        IEnumerable<Incomes> GetAll();
        Incomes GetIncomes(Guid id);
        Incomes GetIncomes(bool isActive);
        Income GetIncome(Guid id);
        Incomes Create(Income income);
        bool CreateNewMonth();
        Income Update(Income income);
        bool Delete(Guid id);
        bool CloseMonth(string month);
    }
}
