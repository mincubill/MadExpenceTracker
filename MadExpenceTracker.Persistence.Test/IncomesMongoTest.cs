using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using System.Data;

namespace MadExpenceTracker.Persistence.Test
{
    public class IncomesMongoTest
    {
        private IIncomePersistence _incomePersistence;

        [SetUp]
        public void Setup()
        {
            Connection mongoConnection = new Connection();
            MongoClient mongoClient = mongoConnection.GetClient();
            IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
            _incomePersistence = new IncomePersistence(mongoDatabase);
        }

        [Test]
        public void CreateIncomeTest()
        {
            var income = new Income()
            {
                Name = "test2",
                Amount = 1000,
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
            };
            var result = _incomePersistence.AddIncome(income);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CreateExpenceExceptionTest()
        {
            var income = new Income()
            {
                Name = "test2",
                Amount = 1000,
                Date = DateTime.Now,
                Id = Guid.NewGuid(),
            };
            Assert.Throws<DataException>(() =>
            {
                var result = _incomePersistence.AddIncome(income);
            });
        }

        [Test]
        public void GetAllIncomeTest()
        {
            var incomes = _incomePersistence.GetAll().ToList();
            Assert.That(incomes, Is.Not.Null);
        }

        [Test]
        public void GetIncomeTest()
        {
            Guid id = Guid.Parse("179c14ab-6520-49e7-8a58-d77b8fbf3621");
            Guid id2 = Guid.Parse("3bab6ee6-26d7-46f1-89fa-acc36cd87cfb");
            var income = _incomePersistence.Get(id2).Income.FirstOrDefault(e => e.Id == id2);
            Assert.That(income, Is.Not.Null);
        }

        [Test]
        public void UpdateIncomeTest()
        {
            Guid id = Guid.Parse("179c14ab-6520-49e7-8a58-d77b8fbf3621");
            var expences = _incomePersistence.Get(id).Income.FirstOrDefault(e => e.Id == id);
            expences.Name = "updateao x 1";
            expences.Amount = 6969;
            bool result = _incomePersistence.Update(expences);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void DeleteIncomeTest()
        {
            Guid id = Guid.Parse("09ab5f12-7345-4138-b173-707c702f9fa0");
            bool result = _incomePersistence.Delete(id);
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void CreateNewDocumentTest()
        {
            var result = _incomePersistence.CreateNewIncomeDocument("2023/11");
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void UpdateExpencesIsActiveTest()
        {
            var result = _incomePersistence.UpdateExpencesIsActive(false, "2023/11");
            Assert.That(result, Is.EqualTo(true));
        }

    }
}
