using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MasIncomeTracker.Persistence.MongoDB.Mapper;
using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class MonthIndexPersistence : IMonthIndexPersistence
    {
        private const string CollectionName = "monthIndex";
        private readonly IMongoCollection<MonthIndexesMongo> _monthIndexCollection;

        public MonthIndexPersistence(IMongoDBProvider provider)
        {
            _monthIndexCollection = provider.GetCollection<MonthIndexesMongo>(CollectionName);
        }

        public IEnumerable<MonthIndexes>? GetMonthsIndexes()
        {
            try
            {
                IEnumerable<MonthIndexesMongo> monthIndexOnDb = _monthIndexCollection
                    .FindSync(Builders<MonthIndexesMongo>.Filter.Empty).ToEnumerable();
                if (!monthIndexOnDb.Any()) return null;
                return MonthIndexMapper.MapToModel(monthIndexOnDb);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MonthIndex? GetMonthIndex(Guid id)
        {
            try
            {
                var filter = Builders<MonthIndexesMongo>.Filter.ElemMatch(e => e.MonthIndex, d => d.Id == id);
                MonthIndexesMongo indexOnDb = _monthIndexCollection.FindSync(filter).FirstOrDefault();
                if(indexOnDb == null) return null;
                return MonthIndexMapper.MapToModel(indexOnDb.MonthIndex.First(e => e.Id == id));
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MonthIndexes AddMonthIndex(MonthIndex indexToCreate)
        {
            try
            {
                var emptyFilter = Builders<MonthIndexesMongo>.Filter.Empty;
                var indexesOnDb = _monthIndexCollection.FindSync(emptyFilter).ToList();
                Guid monthIndexId = Guid.NewGuid();
                if (indexesOnDb.Capacity <= 0)
                {
                    MonthIndexesMongo newExpencesMongo = new MonthIndexesMongo() { Id = monthIndexId, MonthIndex = [MonthIndexMapper.MapToMongo(indexToCreate)] };
                    _monthIndexCollection.InsertOne(newExpencesMongo);
                }
                else
                {
                    var filter = Builders<MonthIndexesMongo>.Filter.Eq(e => e.Id, indexesOnDb.First().Id);
                    var update = Builders<MonthIndexesMongo>.Update.Push(e => e.MonthIndex, MonthIndexMapper.MapToMongo(indexToCreate));
                    var result = _monthIndexCollection.UpdateOne(filter, update);
                    return result.IsAcknowledged ? MonthIndexMapper.MapToModel(indexesOnDb.First()) : null;

                }
                indexesOnDb = _monthIndexCollection.FindSync(emptyFilter).ToList();
                return MonthIndexMapper.MapToModel(indexesOnDb.First());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
