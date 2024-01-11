using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Interfaces.UseCase
{
    public interface IMonthClose
    {
        MonthIndex CloseMonth(string monthToClose, Guid expencesId, Guid incomesId);
    }
}
