using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MasIncomeTracker.Persistence.MongoDB.Mapper;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Data;

namespace MadExpenceTracker.Persistence.MongoDB.Persistence
{
    public class IncomesPersistence : IIncomesPersistence
    {
        private const string CollectionName = "income";
        private readonly IMongoCollection<IncomesMongo> _incomesCollection;
        private readonly FilterDefinition<IncomesMongo> _emptyFilter = Builders<IncomesMongo>.Filter.Empty;

        public IncomesPersistence(IMongoDBProvider provider)
        {
            _incomesCollection = provider.GetCollection<IncomesMongo>(CollectionName);
        }
        
        public IEnumerable<Incomes>? GetAll()
        {
            try
            {
                IEnumerable<IncomesMongo> incomesOnDb = _incomesCollection
                    .FindSync(_emptyFilter).ToList();
                if (!incomesOnDb.Any()) return null;
                return IncomeMapper.MapToModel(incomesOnDb);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Incomes? Get(Guid id)
        {
            try
            {
                var filter = Builders<IncomesMongo>.Filter.Eq(e => e.Id, id);
                IncomesMongo incomeMongo = _incomesCollection.FindSync(filter).FirstOrDefault();
                if (incomeMongo == null) return null;
                return IncomeMapper.MapToModel(incomeMongo);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Incomes? GetByActive(bool isActive)
        {
            try
            {
                var filter = Builders<IncomesMongo>.Filter.Eq(e => e.IsActive, isActive);
                IncomesMongo incomesMongo = _incomesCollection.FindSync(filter).FirstOrDefault();
                if (incomesMongo == null) return null;
                return IncomeMapper.MapToModel(incomesMongo);
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
                var filterActiveMonth = Builders<IncomesMongo>.Filter.Eq(e => e.IsActive, true);
                var incomesOnDb = _incomesCollection.FindSync(filterActiveMonth).ToList();
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
                    incomesOnDb = _incomesCollection.FindSync(filterActiveMonth).ToList();
                    return IncomeMapper.MapToModel(incomesOnDb.First());
                }
                else if (incomesOnDb.Count == 1)
                {
                    var filter = Builders<IncomesMongo>.Filter.Eq(e => e.IsActive, true);
                    var update = Builders<IncomesMongo>.Update.Push(e => e.Incomes, IncomeMapper.MapToMongo(incomeToCreate));
                    var result = _incomesCollection.UpdateOne(filter, update);
                    incomesOnDb = _incomesCollection.FindSync(i => i.IsActive).ToList();
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
                var filter = Builders<IncomesMongo>.Filter.Eq(e => e.IsActive, true);
                var update = Builders<IncomesMongo>.Update.Set(e => e.IsActive, false);
                var result = _incomesCollection.UpdateOne(filter, update);
                if(result.IsAcknowledged) 
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
                return false;
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

        public bool UpdateIncomesIsActive(bool isActive, string runningMonth)
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

        public Income? GetIncome(Guid id)
        {
            try
            {
                var filter = Builders<IncomesMongo>.Filter.ElemMatch(e => e.Incomes, d => d.Id == id);
                IncomesMongo incomesOnDb = _incomesCollection.FindSync(filter).FirstOrDefault();
                if (incomesOnDb == null) return null;
                return IncomeMapper.MapToModel(incomesOnDb.Incomes.First(e => e.Id == id));
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

        public bool IsMonthClosed(string month)
        {
            try
            {
                var filter = Builders<IncomesMongo>.Filter.Eq(e => e.RunningMonth, month);
                IncomesMongo incomeOnDb = _incomesCollection.FindSync(filter).First();
                return incomeOnDb.IsActive;
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
