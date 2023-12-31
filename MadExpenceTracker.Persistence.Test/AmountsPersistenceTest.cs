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
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.Test.Fixture;
using Moq;
using MongoDB.Driver.Core.Operations;
using MongoDB.Bson;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MongoDB.Driver.Linq;

namespace MadExpenceTracker.Persistence.Test
{
    public class AmountsPersistenceTest
    {
        Mock<IMongoCollection<AmountsMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<AmountsMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IAmountsPersistence _persistence;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<AmountsMongo>>();
            _mockCursor = new Mock<IAsyncCursor<AmountsMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
        }

        [Test]
        public void GetAllAmountsTest()
        {
            List<AmountsMongo> amountsOnDb = new() { AmountFixture.GetAmountsMongo() };

            _mockCursor.Setup(x => x.Current).Returns(amountsOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);
            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
                Builders<AmountsMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);


            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            var result = _persistence.GetAmounts().ToList();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.First().Id, Is.EqualTo(Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7")));
        }

        [Test]
        public void GetAmountsTestTimeOut()
        {
            List<AmountsMongo> amountsOnDb = new() { AmountFixture.GetAmountsMongo() };

            _mockCursor.Setup(x => x.Current).Returns(amountsOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);

            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
                Builders<AmountsMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetAmounts());
        }

        [Test]
        public void GetAmountsTestException()
        {
            List<AmountsMongo> amountsOnDb = new() { AmountFixture.GetAmountsMongo() };

            _mockCursor.Setup(x => x.Current).Returns(amountsOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);

            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
                Builders<AmountsMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetAmounts());
        }

        [Test]
        public void AddAmountAndNewDocumentTest()
        {
            Amount amountToCreate = new Amount()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            AmountMongo amountMongo = new AmountMongo()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            AmountsMongo amountsMongo = new AmountsMongo()
            {
                Id = Guid.NewGuid(),
                Amount = new List<AmountMongo>() { amountMongo }
            };
            List<AmountsMongo> amountsOnDbNoData = new();
            List<AmountsMongo> amountsOnDbWithData = new()
            {
                new AmountsMongo
                {
                    Id= Guid.NewGuid(),
                    Amount= new List<AmountMongo>() { amountMongo }
                }
            };

            Mock<IAsyncCursor<AmountsMongo>> mockWithNoData = new Mock<IAsyncCursor<AmountsMongo>>();
            mockWithNoData.Setup(x => x.Current).Returns(amountsOnDbNoData);
            mockWithNoData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<AmountsMongo>> mockWithData = new Mock<IAsyncCursor<AmountsMongo>>();
            mockWithData.Setup(x => x.Current).Returns(amountsOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<AmountsMongo>(
               Builders<AmountsMongo>.Filter.Empty,
               default,
               It.IsAny<CancellationToken>()))
               .Returns(mockWithNoData.Object)
               .Returns(mockWithData.Object);

            _mockMongoCollection.Setup(m => m.InsertOne(amountsMongo, default, It.IsAny<CancellationToken>()));

            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);


            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            var res = _persistence.AddAmount(amountToCreate);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Amount, Is.Not.Null);
        }

        [Test]
        public void AddAmountTest()
        {
            Amount amountToCreate = new Amount()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            List<AmountsMongo> amountsOnDbWithData = new List<AmountsMongo> { AmountFixture.GetAmountsMongo() }; ;

            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);

            Mock<IAsyncCursor<AmountsMongo>> mockWithData = new Mock<IAsyncCursor<AmountsMongo>>();
            mockWithData.Setup(x => x.Current).Returns(amountsOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
               It.IsAny<FilterDefinition<AmountsMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<AmountsMongo>>(),
                It.IsAny<UpdateDefinition<AmountsMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            var res = _persistence.AddAmount(amountToCreate);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Amount, Is.Not.Null);
        }

        [Test]
        public void AddAmountTimeoutExeptionOnGetAmountsTest()
        {
            Amount amountToCreate = new Amount()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            List<AmountsMongo> amountsOnDbWithData = new List<AmountsMongo> { AmountFixture.GetAmountsMongo() }; ;

            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);

            Mock<IAsyncCursor<AmountsMongo>> mockWithData = new Mock<IAsyncCursor<AmountsMongo>>();
            mockWithData.Setup(x => x.Current).Returns(amountsOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
               It.IsAny<FilterDefinition<AmountsMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Throws<TimeoutException>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);
            Assert.Throws<TimeoutException>(() => _persistence.AddAmount(amountToCreate));
        }

        [Test]
        public void AddAmountExceptionOnGetAmountsTest()
        {
            Amount amountToCreate = new Amount()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            List<AmountsMongo> amountsOnDbWithData = new List<AmountsMongo> { AmountFixture.GetAmountsMongo() }; ;

            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);

            Mock<IAsyncCursor<AmountsMongo>> mockWithData = new Mock<IAsyncCursor<AmountsMongo>>();
            mockWithData.Setup(x => x.Current).Returns(amountsOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
               It.IsAny<FilterDefinition<AmountsMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Throws<Exception>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);
            Assert.Throws<Exception>(() => _persistence.AddAmount(amountToCreate));
        }

        [Test]
        public void AddAmountExceptionOnUpdateTest()
        {
            Amount amountToCreate = new Amount()
            {
                Id = Guid.NewGuid(),
                Savings = 20,
                TotalAditionalExpences = 200000,
                TotalBaseExpences = 200000,
                TotalIncomes = 200000,
            };
            List<AmountsMongo> amountsOnDbWithData = new List<AmountsMongo> { AmountFixture.GetAmountsMongo() }; ;

            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);

            Mock<IAsyncCursor<AmountsMongo>> mockWithData = new Mock<IAsyncCursor<AmountsMongo>>();
            mockWithData.Setup(x => x.Current).Returns(amountsOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
               It.IsAny<FilterDefinition<AmountsMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<AmountsMongo>>(),
                It.IsAny<UpdateDefinition<AmountsMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.AddAmount(amountToCreate));

        }

        [Test]
        public void GetAmountsTest()
        {
            Guid id = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7");
            List<AmountsMongo> amountsOnDb = new() { AmountFixture.GetAmountsMongo() };

            _mockCursor.Setup(x => x.Current).Returns(amountsOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);
            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
                It.IsAny<FilterDefinition<AmountsMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);
            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            var result = _persistence.GetAmounts(id);

            Assert.That(result, Is.Not.Null);

        }

        [Test]
        public void GetAmountsTimeOutExceptionTest()
        {
            Guid id = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7");
            List<AmountsMongo> amountsOnDb = new() { AmountFixture.GetAmountsMongo() };

            _mockCursor.Setup(x => x.Current).Returns(amountsOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);
            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
                It.IsAny<FilterDefinition<AmountsMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetAmounts());
        }

        [Test]
        public void GetAmountsExceptionTest()
        {
            Guid id = Guid.Parse("9c64e4d9-c9af-4714-8650-c98e6ebecfa7");
            List<AmountsMongo> amountsOnDb = new() { AmountFixture.GetAmountsMongo() };

            _mockCursor.Setup(x => x.Current).Returns(amountsOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockDbprovider.Setup(x => x.GetCollection<AmountsMongo>("amounts"))
                .Returns(_mockMongoCollection.Object);
            _mockMongoCollection.Setup(x => x.FindSync<AmountsMongo>(
                It.IsAny<FilterDefinition<AmountsMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new AmountsPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetAmounts());
        }
    }
}
