using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class ExpencesPersistence : IExpencePersistence
    {
        private readonly IMongoDatabase _mongoDatabase;
        private string _collectionName = "expence";
        private IMongoCollection<ExpencesMongo> _expencesCollection;

        public ExpencesPersistence(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
            _expencesCollection = _mongoDatabase.GetCollection<ExpencesMongo>(_collectionName);
        }

        public IEnumerable<Expences> GetAll()
        {
            try
            {
                IEnumerable<ExpencesMongo> expencesOnDb = _expencesCollection.Find(_ => true).ToEnumerable();
                return ExpenceMapper.MapToModel(expencesOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Expences Get(Guid id)
        {
            try
            {
                var filter = Builders<ExpencesMongo>.Filter.ElemMatch(e => e.Expences, exp => exp.Id == id);
                ExpencesMongo expenceMongo = _expencesCollection.Find(filter).First();
                return ExpenceMapper.MapToModel(expenceMongo);
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
                var expencesOnDb = _expencesCollection.Find(e => e.IsActive).ToList();
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
                    expencesOnDb = _expencesCollection.Find(e => e.IsActive).ToList();
                    return ExpenceMapper.MapToModel(expencesOnDb.First());
                }
                else if (expencesOnDb.Count == 1)
                {
                    var filter = Builders<ExpencesMongo>.Filter.Eq(e => e.IsActive, true);
                    var update = Builders<ExpencesMongo>.Update.Push(e => e.Expences, ExpenceMapper.MapToMongo(expenceToCreate));
                    var result = _expencesCollection.UpdateOne(filter, update);
                    expencesOnDb = _expencesCollection.Find(e => e.IsActive).ToList();
                    return result.IsAcknowledged ? ExpenceMapper.MapToModel(expencesOnDb.First()) : null;
                }
                else
                {
                    throw new DataException("Cannot be more than one active month");
                }
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
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Guid id)
        {
            var filter = Builders<ExpencesMongo>.Filter.ElemMatch(e => e.Expences, exp => exp.Id == id);
            var update = Builders<ExpencesMongo>.Update
                .PullFilter(e => e.Expences, Builders<ExpenceMongo>.Filter.Where(nm => nm.Id == id));
            var result = _expencesCollection.UpdateOne(filter, update);
            return result.IsAcknowledged;
        }
    }
}
