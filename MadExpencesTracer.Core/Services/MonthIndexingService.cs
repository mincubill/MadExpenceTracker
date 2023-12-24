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

        public IEnumerable<MonthIndexes> GetMonthsIndexes()
        {
            return _persistence.GetMonthsIndexes();
        }

        public MonthIndex GetMonthsIndex(Guid id)
        {
            throw new NotImplementedException();
        }

        public MonthIndexes AddMonthIndex(MonthIndex index)
        {
            throw new NotImplementedException();
        }
    }
}
