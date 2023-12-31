using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class AmountsPersistence : IAmountsPersistence
    {
        private const string CollectionName = "amounts";
        private readonly IMongoCollection<AmountsMongo> _amountsCollection;

        public AmountsPersistence(IMongoDBProvider provider)
        {
            _amountsCollection = provider.GetCollection<AmountsMongo>(CollectionName);
        }

        public IEnumerable<Amounts> GetAmounts()
        {
            try
            {
                IEnumerable<AmountsMongo> amountsOnDb = _amountsCollection
                    .FindSync(Builders<AmountsMongo>.Filter.Empty).ToEnumerable();
                return AmountMapper.MapToModel(amountsOnDb);
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

        public Amounts GetAmounts(Guid id)
        {
            try
            {
                var filter = Builders<AmountsMongo>.Filter.ElemMatch(e => e.Amount, exp => exp.Id == id);
                AmountsMongo amountsMongo = _amountsCollection.FindSync(filter).First();
                return AmountMapper.MapToModel(amountsMongo);
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

        public Amounts AddAmount(Amount amountToCreate)
        {
            try
            {
                var filterEmpty = Builders<AmountsMongo>.Filter.Empty;
                List<AmountsMongo> amountsOnDb = _amountsCollection.FindSync(filterEmpty).ToList();
                string runningMonth = $"{DateTime.Now.Year}/{DateTime.Now.Month}";
                if (amountsOnDb.Count <= 0)
                {
                    AmountsMongo newExpencesMongo = new AmountsMongo()
                    {
                        Id = Guid.NewGuid(),
                        Amount = [AmountMapper.MapToMongo(amountToCreate)]
                    };
                    _amountsCollection.InsertOne(newExpencesMongo);
                    amountsOnDb = _amountsCollection.FindSync(filterEmpty).ToList();
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
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

       
    }
}
