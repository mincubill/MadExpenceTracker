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
        private const string _collectionName = "amounts";
        private readonly IMongoCollection<AmountsMongo> _amountsCollection;

        public AmountsPersistence(IMongoDBProvider provider)
        {
            _amountsCollection = provider.GetCollection<AmountsMongo>(_collectionName);
        }

        public IEnumerable<Amounts>? GetAmounts()
        {
            try
            {
                IEnumerable<AmountsMongo> amountsOnDb = _amountsCollection
                    .FindSync(Builders<AmountsMongo>.Filter.Empty).ToList();
                if (!amountsOnDb.Any()) return null;
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

        public Amounts? GetAmounts(Guid id)
        {
            try
            {
                var filter = Builders<AmountsMongo>.Filter.ElemMatch(e => e.Amount, exp => exp.Id == id);
                AmountsMongo amountsMongo = _amountsCollection.FindSync(filter).FirstOrDefault();
                if (amountsMongo == null) return null;
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
                if (amountsOnDb.Count <= 0)
                {
                    AmountsMongo newAmountMongo = new AmountsMongo()
                    {
                        Id = Guid.NewGuid(),
                        Amount = [AmountMapper.MapToMongo(amountToCreate)]
                    };
                    _amountsCollection.InsertOne(newAmountMongo);
                    amountsOnDb = _amountsCollection.FindSync(filterEmpty).ToList();
                    return AmountMapper.MapToModel(amountsOnDb[0]);
                }
                else
                {
                    var filter = Builders<AmountsMongo>.Filter.Eq(e => e.Id, amountsOnDb[0].Id);
                    var update = Builders<AmountsMongo>.Update.Push(e => e.Amount, AmountMapper.MapToMongo(amountToCreate));
                    var result = _amountsCollection.UpdateOne(filter, update);
                    return result.IsAcknowledged ? AmountMapper.MapToModel(amountsOnDb[0]) : null;
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
