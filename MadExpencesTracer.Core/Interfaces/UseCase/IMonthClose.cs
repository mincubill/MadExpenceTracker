using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.UseCase
{
    public interface IMonthClose
    {
        bool CloseMonth(Expences expences, Incomes incomes, Amounts amounts);
    }
}
