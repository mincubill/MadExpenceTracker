using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class AmountsPersistence : IAmountsPersistence
    {

        private readonly IMongoDatabase _mongoDatabase;
        private string _collectionName = "amounts";
        private IMongoCollection<AmountsMongo> _amountsCollection;

        public AmountsPersistence(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _amountsCollection = _mongoDatabase.GetCollection<AmountsMongo>(_collectionName);
        }

        public IEnumerable<Amounts> GetAmounts()
        {
            try
            {
                IEnumerable<AmountsMongo> amountsOnDb = _amountsCollection.Find(_ => true).ToEnumerable();
                return AmountMapper.MapToModel(amountsOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Amounts AddAmount(Amount amountToCreate)
        {
            try
            {
                var amountsOnDb = _amountsCollection.Find(_ => true).ToList();
                string runningMonth = $"{DateTime.Now.Year}/{DateTime.Now.Month}";
                if (amountsOnDb.Count <= 0)
                {
                    AmountsMongo newExpencesMongo = new AmountsMongo() {Id = Guid.NewGuid(), Amount = [AmountMapper.MapToMongo(amountToCreate)] };
                    _amountsCollection.InsertOne(newExpencesMongo);
                    amountsOnDb = _amountsCollection.Find(_ => true).ToList();
                    return AmountMapper.MapToModel(amountsOnDb.First());
                }
                else
                {
                    var filter = Builders<AmountsMongo>.Filter.Eq(e => e.Id, amountsOnDb.First().Id);
                    var update = Builders<AmountsMongo>.Update.Push(e => e.Amount, AmountMapper.MapToMongo(amountToCreate));
                    var result = _amountsCollection.UpdateOne(filter, update);
                    return result.IsAcknowledged ? AmountMapper.MapToModel(amountsOnDb.First()) : null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Amounts GetAmount(Guid id)
        {
            try
            {
                var filter = Builders<AmountsMongo>.Filter.ElemMatch(e => e.Amount, exp => exp.Id == id);
                AmountsMongo expenceMongo = _amountsCollection.Find(filter).First();
                return AmountMapper.MapToModel(expenceMongo);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
