using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.UseCase
{
    public interface IMonthClose
    {
        MonthIndex CloseMonth(string monthToClose, Expences expences, Incomes incomes, Amount amount);
    }
}
