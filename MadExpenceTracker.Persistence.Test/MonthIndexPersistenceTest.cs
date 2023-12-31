using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;

namespace MadExpenceTracker.Persistence.Test
{
    public class MonthIndexPersistenceTest
    {
        //private IMonthIndexPersistence _monthIndexPersistence;

        //[SetUp]
        //public void Setup()
        //{
        //    Connection mongoConnection = new Connection();
        //    MongoClient mongoClient = mongoConnection.GetClient();
        //    IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
        //    _monthIndexPersistence = new MonthIndexPersistence(mongoDatabase);
        //}

        //[Test]
        //public void GetIndexMonthTest()
        //{
        //    IEnumerable<MonthIndexes> indexes = _monthIndexPersistence.GetMonthsIndexes();
        //    Assert.That(indexes, Is.Not.Null);
        //}

        //[Test]
        //public void SetIndexMonth()
        //{
        //    MonthIndex index = new MonthIndex() 
        //    { 
        //        Id = Guid.NewGuid(),
        //        AmountsId = Guid.NewGuid(),
        //        ExpencesId = Guid.NewGuid(),
        //        IncomesId = Guid.NewGuid(),
        //        Month = "2023/12",
        //        SavingsRate = 20
        //    };
        //    MonthIndexes indexes = _monthIndexPersistence.AddMonthIndex(index);
        //    Assert.That(indexes, Is.Not.Null);
        //}
    }
}
