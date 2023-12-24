using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using System.Data;

namespace MadExpenceTracker.Persistence.Test
{
    public class ExpencesMongoTest
    {
        private IExpencePersistence _expencePersistence;

        [SetUp]
        public void Setup()
        {
            Connection mongoConnection = new Connection();
            MongoClient mongoClient = mongoConnection.GetClient();
            IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
            _expencePersistence = new ExpencesPersistence(mongoDatabase);
        }

        [Test]
        public void CreateExpenceTest()
        {
            var expence = new Expence()
            {
                Name = "test3",
                Amount = 20000,
                Date = DateTime.Now,
                ExpenceType = ExpenceType.Base,
                Id = Guid.NewGuid(),
            };
            var result = _expencePersistence.AddExpence(expence);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CreateExpenceExceptionTest()
        {
            var expence = new Expence()
            {
                Name = "test2",
                Amount = 2000,
                Date = DateTime.Now,
                ExpenceType = ExpenceType.Base,
                Id = Guid.NewGuid(),
            };
            Assert.Throws<DataException>(() =>
            {
                var result = _expencePersistence.AddExpence(expence);
            });
        }

        [Test]
        public void GetAllExpencesTest()
        {
            var expences = _expencePersistence.GetAll().ToList();
            Assert.That(expences, Is.Not.Null);
        }

        [Test]
        public void GetExpenceTest()
        {
            Guid id = Guid.Parse("869b7327-89f8-4f3d-810a-42d8577790c8");
            Guid id2 = Guid.Parse("dc912738-4168-49f2-a54c-4d379ee7c765");
            var expences = _expencePersistence.Get(id2).Expence.First(e => e.Id == id2);
            Assert.That(expences, Is.Not.Null);
        }

        [Test]
        public void UpdateExpenceTest()
        {
            Guid id = Guid.Parse("7cfdea35-6697-4935-a612-280b8b920b35");
            Guid id2 = Guid.Parse("7cfdea35-6697-4935-a612-280b8b920b35");
            var expences = _expencePersistence.Get(id).Expence.First(e => e.Id == id);
            expences.Name = "updateao x 2";
            expences.Amount = 6969;
            bool result = _expencePersistence.Update(expences);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void DeleteExpenceTest()
        {
            Guid id = Guid.Parse("7cfdea35-6697-4935-a612-280b8b920b35");
            Guid id2 = Guid.Parse("7cfdea35-6697-4935-a612-280b8b920b35");
            bool result = _expencePersistence.Delete(id);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreateNewDocumentTest()
        {
            var result = _expencePersistence.CreateNewExpencesDocument("2023/11");
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void UpdateExpencesIsActiveTest()
        {
            var result = _expencePersistence.UpdateExpencesIsActive(false, "2023/11");
            Assert.That(result, Is.EqualTo(true));
        }
    }
}