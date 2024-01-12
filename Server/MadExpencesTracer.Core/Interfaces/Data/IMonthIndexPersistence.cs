using MadExpenceTracker.Core.Model;

namespace MadExpenceTracker.Core.Persistence
{
    public interface IMonthIndexPersistence
    {
        MonthIndexes? GetMonthsIndexes();
        MonthIndex? GetMonthIndex(Guid id);
        MonthIndexes AddMonthIndex(MonthIndex index);
    }
}
