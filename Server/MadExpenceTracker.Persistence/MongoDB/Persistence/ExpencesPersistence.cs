using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class ExpencesPersistence : IExpencePersistence
    {
        private const string CollectionName = "expence";
        private readonly IMongoCollection<ExpencesMongo> _expencesCollection;
        private readonly FilterDefinition<ExpencesMongo> _emptyFilter = Builders<ExpencesMongo>.Filter.Empty;

        public ExpencesPersistence(IMongoDBProvider provider)
        {
            _expencesCollection = provider.GetCollection<ExpencesMongo>(CollectionName);
        }
        
        public IEnumerable<Expences>? GetAll()
        {
            try
            {
                IEnumerable<ExpencesMongo> expencesOnDb = _expencesCollection
                    .FindSync(_emptyFilter).ToList();
                if (!expencesOnDb.Any()) return null;
                return ExpenceMapper.MapToModel(expencesOnDb);
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

        public Expences? Get(Guid id)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.Eq(e => e.Id, id);
                ExpencesMongo expenceMongo = _expencesCollection.FindSync(filter).FirstOrDefault();
                if (expenceMongo == null) return null;
                return ExpenceMapper.MapToModel(expenceMongo);
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

        public Expences? GetByActive(bool isActive)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.Eq(e => e.IsActive, isActive);
                ExpencesMongo expenceMongo = _expencesCollection.FindSync(filter).FirstOrDefault();
                if (expenceMongo == null) return null;
                return ExpenceMapper.MapToModel(expenceMongo);
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

        public Expences AddExpence(Expence expenceToCreate)
        {
            try
            {
                var filterActiveMonth = Builders<ExpencesMongo>.Filter.Eq(e => e.IsActive, true);
                var expencesOnDb = _expencesCollection.FindSync(filterActiveMonth).ToList();
                string runningMonth = $"{DateTime.Now.Year}/{DateTime.Now.Month}";
                if (expencesOnDb.Count <= 0)
                {
                    ExpencesMongo newExpencesMongo = new ExpencesMongo()
                    {
                        Id = Guid.NewGuid(),
                        RunningMonth = runningMonth,
                        IsActive = true,
                        Expences = [ExpenceMapper.MapToMongo(expenceToCreate)]
                    };
                    _expencesCollection.InsertOne(newExpencesMongo);
                    expencesOnDb = _expencesCollection.FindSync(filterActiveMonth).ToList();
                    return ExpenceMapper.MapToModel(expencesOnDb.First());
                }
                else if (expencesOnDb.Count == 1)
                {
                    var update = Builders<ExpencesMongo>.Update.Push(e => e.Expences, 
                        ExpenceMapper.MapToMongo(expenceToCreate));
                    var result = _expencesCollection.UpdateOne(filterActiveMonth, update);
                    expencesOnDb = _expencesCollection.FindSync(filterActiveMonth).ToList();
                    return result.IsAcknowledged ? ExpenceMapper.MapToModel(expencesOnDb.First()) : null;
                }
                else
                {
                    throw new DataException("Cannot be more than one active month");
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

        public bool CreateNewExpencesDocument(string runningMonth)
        {
            try
            {
                ExpencesMongo newExpencesMongo = new ExpencesMongo()
                {
                    Id = Guid.NewGuid(),
                    RunningMonth = runningMonth,
                    IsActive = true,
                    Expences = new List<ExpenceMongo>().AsEnumerable()
                };
                _expencesCollection.InsertOne(newExpencesMongo);
                return true;
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

        public bool Update(Expence expence)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.ElemMatch(e => e.Expences, exp => exp.Id == expence.Id);
                var update = Builders<ExpencesMongo>.Update
                    .Set(e => e.Expences.FirstMatchingElement().Name, expence.Name)
                    .Set(e => e.Expences.FirstMatchingElement().Amount, expence.Amount)
                    .Set(e => e.Expences.FirstMatchingElement().Date, expence.Date)
                    .Set(e => e.Expences.FirstMatchingElement().ExpenceType, expence.ExpenceType.ToString());
                var result = _expencesCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
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

        public bool UpdateExpencesIsActive(bool isActive, string runningMonth)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.Eq(e => e.RunningMonth, runningMonth);
                var update = Builders<ExpencesMongo>.Update
                    .Set(e => e.IsActive, isActive);
                var result = _expencesCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
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

        public bool Delete(Guid id)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.ElemMatch(e => e.Expences, exp => exp.Id == id);
                var update = Builders<ExpencesMongo>.Update
                    .PullFilter(e => e.Expences, Builders<ExpenceMongo>.Filter.Where(nm => nm.Id == id));
                var result = _expencesCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
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

        public Expence? GetExpence(Guid id)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.ElemMatch(e => e.Expences, d => d.Id == id);
                ExpencesMongo expencesOnDb = _expencesCollection.FindSync(filter).First();
                ExpenceMongo? expenceMongo = expencesOnDb.Expences.FirstOrDefault(e => e.Id == id);
                if (expenceMongo == null) return null;
                return ExpenceMapper.MapToModel(expenceMongo);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
