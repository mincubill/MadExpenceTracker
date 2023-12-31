using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MasIncomeTracker.Persistence.MongoDB.Mapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data;
using MadExpenceTracker.Persistence.MongoDB.Provider;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class IncomePersistence : IIncomePersistence
    {
        private const string CollectionName = "income";
        private readonly IMongoCollection<IncomesMongo> _incomesCollection;
        
        public IncomePersistence(IMongoDBProvider provider)
        {
            _incomesCollection = provider.GetCollection<IncomesMongo>(CollectionName);
        }
        
        public IEnumerable<Incomes> GetAll()
        {
            try
            {
                IEnumerable<IncomesMongo> expencesOnDb = _incomesCollection.Find(_ => true).ToEnumerable();
                return IncomeMapper.MapToModel(expencesOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Incomes Get(Guid id)
        {
            try
            {
                IncomesMongo expenceMongo = _incomesCollection.Find(i => i.Id == id).First();
                return IncomeMapper.MapToModel(expenceMongo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Incomes GetByActive(bool isActive)
        {
            try
            {
                IncomesMongo expenceMongo = _incomesCollection.Find(i => i.IsActive == isActive).First();
                return IncomeMapper.MapToModel(expenceMongo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Incomes AddIncome(Income incomeToCreate)
        {
            try
            {
                var incomesOnDb = _incomesCollection.Find(i => i.IsActive).ToList();
                string runningMonth = $"{DateTime.Now.Year}/{DateTime.Now.Month}";
                if (incomesOnDb.Count <= 0)
                {
                    IncomesMongo newIncomesMongo = new IncomesMongo()
                    {
                        Id = Guid.NewGuid(),
                        RunningMonth = runningMonth, 
                        IsActive = true,
                        Incomes = [IncomeMapper.MapToMongo(incomeToCreate)] 
                    };
                    _incomesCollection.InsertOne(newIncomesMongo);
                    incomesOnDb = _incomesCollection.Find(i => i.IsActive).ToList();
                    return IncomeMapper.MapToModel(incomesOnDb.First());
                }
                else if (incomesOnDb.Count == 1)
                {
                    var filter = Builders<IncomesMongo>.Filter.Eq(e => e.RunningMonth, runningMonth);
                    var update = Builders<IncomesMongo>.Update.Push(e => e.Incomes, IncomeMapper.MapToMongo(incomeToCreate));
                    var result = _incomesCollection.UpdateOne(filter, update);
                    incomesOnDb = _incomesCollection.Find(i => i.IsActive).ToList();
                    return result.IsAcknowledged ? IncomeMapper.MapToModel(incomesOnDb.First()) : null;
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

        public bool CreateNewIncomeDocument(string runningMonth)
        {
            try
            {
                IncomesMongo newIncomesMongo = new IncomesMongo() 
                {
                    Id = Guid.NewGuid(),
                    RunningMonth = runningMonth, 
                    IsActive = true,
                    Incomes = new List<IncomeMongo>().AsEnumerable(),
                };
                _incomesCollection.InsertOne(newIncomesMongo);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Income expence)
        {
            try
            {
                var filter = Builders<IncomesMongo>.Filter.ElemMatch(e => e.Incomes, exp => exp.Id == expence.Id);
                var update = Builders<IncomesMongo>.Update
                    .Set(e => e.Incomes.FirstMatchingElement().Name, expence.Name)
                    .Set(e => e.Incomes.FirstMatchingElement().Amount, expence.Amount)
                    .Set(e => e.Incomes.FirstMatchingElement().Date, expence.Date);
                var result = _incomesCollection.UpdateOne(filter, update);
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
                var filter = Builders<IncomesMongo>.Filter.Eq(e => e.RunningMonth, runningMonth);
                var update = Builders<IncomesMongo>.Update
                    .Set(e => e.IsActive, isActive);
                var result = _incomesCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Guid id)
        {
            var filter = Builders<IncomesMongo>.Filter.ElemMatch(e => e.Incomes, exp => exp.Id == id);
            var update = Builders<IncomesMongo>.Update
                .PullFilter(e => e.Incomes, Builders<IncomeMongo>.Filter.Where(nm => nm.Id == id));
            var result = _incomesCollection.UpdateOne(filter, update);
            return result.IsAcknowledged;
        }

        
    }
}
