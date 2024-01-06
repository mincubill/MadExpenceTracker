using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MadExpenceTracker.Persistence.Test.Fixture;
using MongoDB.Driver;
using Moq;

namespace MadExpenceTracker.Persistence.Test
{
    public class MonthIndexPersistenceTest
    {
        Mock<IMongoCollection<MonthIndexesMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<MonthIndexesMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IMonthIndexPersistence _persistence;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<MonthIndexesMongo>>();
            _mockCursor = new Mock<IAsyncCursor<MonthIndexesMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
            _mockDbprovider.Setup(x => x.GetCollection<MonthIndexesMongo>("monthIndex"))
                .Returns(_mockMongoCollection.Object);
        }

        [Test]
        public void GetMonthsIndexesTest()
        {
            List<MonthIndexesMongo> indexesOnDb = [MonthIndexFixture.GetMonthIndexesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(indexesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
                Builders<MonthIndexesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            var result = _persistence.GetMonthsIndexes();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetMonthsIndexesTimeoutExceptionTest()
        {
            List<MonthIndexesMongo> indexesOnDb = [MonthIndexFixture.GetMonthIndexesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(indexesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
                Builders<MonthIndexesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetMonthsIndexes());
        }

        [Test]
        public void GetMonthsIndexesExceptionTest()
        {
            List<MonthIndexesMongo> indexesOnDb = [MonthIndexFixture.GetMonthIndexesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(indexesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
                Builders<MonthIndexesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetMonthsIndexes());
        }

        [Test]
        public void GetMonthIndexByIdTest()
        {
            List<MonthIndexesMongo> indexesOnDb = [MonthIndexFixture.GetMonthIndexesMongo()];
            Guid id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489");

            _mockCursor.Setup(x => x.Current).Returns(indexesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);

            var filter = Builders<MonthIndexesMongo>.Filter.ElemMatch(e => e.MonthIndex, d => d.Id == id);
            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
                It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
                null,
                It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            var result = _persistence.GetMonthIndex(id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));

        }

        [Test]
        public void GetMonthIndexByIdTimeoutExceptionTest()
        {
            List<MonthIndexesMongo> indexesOnDb = [MonthIndexFixture.GetMonthIndexesMongo()];
            Guid id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489");

            _mockCursor.Setup(x => x.Current).Returns(indexesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
                It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
                null,
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetMonthIndex(id));
        }

        [Test]
        public void GetMonthIndexByIdExceptionTest()
        {
            List<MonthIndexesMongo> indexesOnDb = [MonthIndexFixture.GetMonthIndexesMongo()];
            Guid id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489");

            _mockCursor.Setup(x => x.Current).Returns(indexesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true).Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
                It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
                null,
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetMonthIndex(id));
        }

        [Test]
        public void AddMonthIndexNewDocumentTest()
        {
            MonthIndex indexToCreate = MonthIndexFixture.GetMonthIndex();
            MonthIndexMongo indexMongoToCreate = MonthIndexFixture.GetMonthIndexMongo();
            MonthIndexesMongo indexesMongoToCreate = MonthIndexFixture.GetMonthIndexesMongo();
            List<MonthIndexesMongo> emptyIndexesMongo = [];
            List<MonthIndexesMongo> indexesMongo = new List<MonthIndexesMongo>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    MonthIndex = new List<MonthIndexMongo>() { indexMongoToCreate }.AsEnumerable(),
                },
            };

            Mock<IAsyncCursor<MonthIndexesMongo>> mockWithNoData = new Mock<IAsyncCursor<MonthIndexesMongo>>();
            mockWithNoData.Setup(x => x.Current).Returns(emptyIndexesMongo);
            mockWithNoData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<MonthIndexesMongo>> mockWithData = new Mock<IAsyncCursor<MonthIndexesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(indexesMongo);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<MonthIndexesMongo>(
               Builders<MonthIndexesMongo>.Filter.Empty,
               default,
               It.IsAny<CancellationToken>()))
               .Returns(mockWithNoData.Object)
               .Returns(mockWithData.Object);

            _mockMongoCollection.Setup(m => m.InsertOne(
                indexesMongoToCreate,
                default,
                It.IsAny<CancellationToken>())
            );

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            var res = _persistence.AddMonthIndex(indexToCreate);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void AddMonthIndexTest()
        {
            MonthIndex indexToCreate = MonthIndexFixture.GetMonthIndex();
            List<MonthIndexesMongo> indexesMongo = new List<MonthIndexesMongo>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    MonthIndex = new List<MonthIndexMongo>() { MonthIndexFixture.GetMonthIndexMongo() }.AsEnumerable(),
                },
            };

            Mock<IAsyncCursor<MonthIndexesMongo>> mockWithData = new Mock<IAsyncCursor<MonthIndexesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(indexesMongo);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
               It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
                It.IsAny<UpdateDefinition<MonthIndexesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            var res = _persistence.AddMonthIndex(indexToCreate);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void AddMonthIndexTimeoutExceptionTest()
        {
            MonthIndex indexToCreate = MonthIndexFixture.GetMonthIndex();
            List<MonthIndexesMongo> indexesMongo = new List<MonthIndexesMongo>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    MonthIndex = new List<MonthIndexMongo>() { MonthIndexFixture.GetMonthIndexMongo() }.AsEnumerable(),
                },
            };

            Mock<IAsyncCursor<MonthIndexesMongo>> mockWithData = new Mock<IAsyncCursor<MonthIndexesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(indexesMongo);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
               It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Throws<TimeoutException>();

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.AddMonthIndex(indexToCreate));

        }

        [Test]
        public void AddMonthIndexExceptionTest()
        {
            MonthIndex indexToCreate = MonthIndexFixture.GetMonthIndex();
            List<MonthIndexesMongo> indexesMongo = new List<MonthIndexesMongo>()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    MonthIndex = new List<MonthIndexMongo>() { MonthIndexFixture.GetMonthIndexMongo() }.AsEnumerable(),
                },
            };

            Mock<IAsyncCursor<MonthIndexesMongo>> mockWithData = new Mock<IAsyncCursor<MonthIndexesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(indexesMongo);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<MonthIndexesMongo>(
               It.IsAny<FilterDefinition<MonthIndexesMongo>>(),
               default,
               It.IsAny<CancellationToken>()))
               .Throws<Exception>();

            _persistence = new MonthIndexPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.AddMonthIndex(indexToCreate));
        }
    }
}
