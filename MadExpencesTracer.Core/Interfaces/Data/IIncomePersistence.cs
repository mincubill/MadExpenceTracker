using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IIncomePersistence
    {
        IEnumerable<Incomes> GetAll();
        Incomes Get(Guid id);
        Incomes GetByActive(bool isActive);
        Incomes AddIncome(Income income);
        bool CreateNewIncomeDocument(string runningMonth);
        bool Update(Income income);
        bool UpdateExpencesIsActive(bool isActive, string runningMonth);
        bool Delete(Guid id);
    }
}
