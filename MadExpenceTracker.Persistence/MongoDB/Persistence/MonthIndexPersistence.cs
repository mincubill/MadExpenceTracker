using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class MonthIndexPersistence : IMonthIndexPersistence
    {
        private readonly IMongoDatabase _mongoDatabase;
        private string _collectionName = "monthIndex";
        private IMongoCollection<MonthIndexesMongo> _monthIndexCollection;

        public MonthIndexPersistence(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _monthIndexCollection = _mongoDatabase.GetCollection<MonthIndexesMongo>(_collectionName);
        }

        public IEnumerable<MonthIndexes> GetMonthsIndexes()
        {
            try
            {
                IEnumerable<MonthIndexesMongo> monthIndexOnDb = _monthIndexCollection.Find(_ => true).ToEnumerable();
                return MonthIndexMapper.MapToModel(monthIndexOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MonthIndex GetMonthIndex(Guid id)
        {
            try
            {
                return null;
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
                var indexesOnDb = _monthIndexCollection.Find(_ => true).ToList();
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
                indexesOnDb = _monthIndexCollection.Find(_ => true).ToList();
                return MonthIndexMapper.MapToModel(indexesOnDb.First());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
