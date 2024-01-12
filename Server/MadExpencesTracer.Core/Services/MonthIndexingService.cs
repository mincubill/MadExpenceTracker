using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;

namespace MadExpenceTracker.Core.Services
{
    public class MonthIndexingService : IMonthIndexingService
    {
        private readonly IMonthIndexPersistence _persistence;

        public MonthIndexingService(IMonthIndexPersistence persistence)
        {
            _persistence = persistence;
        }

        public MonthIndexes GetMonthsIndexes()
        {
            MonthIndexes monthIndexes = _persistence.GetMonthsIndexes() ?? throw new NotFoundException("Indexes not found");
            List<MonthIndex> monthIndexReversed = monthIndexes.MonthIndex.Reverse().ToList();
            monthIndexes.MonthIndex = monthIndexReversed;
            return monthIndexes;
        }

        public MonthIndex GetMonthsIndex(Guid id)
        {
            return _persistence.GetMonthIndex(id) ?? throw new NotFoundException("Index not found");
        }

        public MonthIndexes AddMonthIndex(MonthIndex index)
        {
            return _persistence.AddMonthIndex(index);
        }
    }
}
