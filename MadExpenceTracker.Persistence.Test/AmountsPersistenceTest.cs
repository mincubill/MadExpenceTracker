using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.Test
{
    public class AmountsPersistenceTest
    {
        private IAmountsPersistence _amountsPersistence;

        [SetUp]
        public void Setup()
        {
            Connection mongoConnection = new Connection();
            MongoClient mongoClient = mongoConnection.GetClient();
            IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
            _amountsPersistence = new AmountsPersistence(mongoDatabase);
        }

        [Test]
        public void CreateAmountTest()
        {
            Amount amount = new Amount()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            var result = _amountsPersistence.AddAmount(amount);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAmount()
        {
            Guid id1 = Guid.Parse("e6a936ac-bb63-42ef-b33b-e427cf58d1cb");
            Guid id2 = Guid.Parse("1ca28d9e-8348-4c99-97fa-8fc5cd6e4034");
            Amounts result = _amountsPersistence.GetAmount(id1);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAmounts()
        {
            var result = _amountsPersistence.GetAmounts().ToList();
            Assert.That(result, Is.Not.Null);
        }
    }
}
