using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IIncomesPersistence
    {
        IEnumerable<Incomes>? GetAll();
        Incomes? Get(Guid id);
        Incomes? GetByActive(bool isActive);
        Incomes AddIncome(Income income);
        Income? GetIncome(Guid id);
        bool CreateNewIncomeDocument(string runningMonth);
        bool Update(Income income);
        bool UpdateIncomesIsActive(bool isActive, string runningMonth);
        bool Delete(Guid id);
        bool IsMonthClosed(string month);
    }
}
