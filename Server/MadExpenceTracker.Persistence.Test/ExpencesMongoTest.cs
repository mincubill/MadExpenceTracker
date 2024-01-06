using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MadExpenceTracker.Persistence.Test.Fixture;
using MongoDB.Driver;
using Moq;
using System.Data;

namespace MadExpenceTracker.Persistence.Test
{
    public class ExpencesMongoTest
    {
        Mock<IMongoCollection<ExpencesMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<ExpencesMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IExpencePersistence _persistence;
        private readonly string CURRENT_MONTH = $"{DateTime.Now.Year}/{DateTime.Now.Month}";

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<ExpencesMongo>>();
            _mockCursor = new Mock<IAsyncCursor<ExpencesMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
            _mockDbprovider.Setup(x => x.GetCollection<ExpencesMongo>("expence"))
                .Returns(_mockMongoCollection.Object);
        }

        [Test]
        public void GetAllTest()
        {
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                Builders<ExpencesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var result = _persistence.GetAll().ToList();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAllTimeoutExceptionTest()
        {
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                Builders<ExpencesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetAll().ToList());
        }

        [Test]
        public void GetAllExceptionTest()
        {
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                Builders<ExpencesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetAll().ToList());
        }

        [Test]
        public void GetExpencesByIdTest()
        {
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var result = _persistence.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GetByIdTimeoutExceptionTest()
        {
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.Get(id));
        }

        [Test]
        public void GetByIdExceptionTest()
        {
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.Get(id));
        }

        [Test]
        public void GetByActiveTest()
        {
            bool isActive = true;
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var result = _persistence.GetByActive(isActive);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GetByActiveTimeoutExceptionTest()
        {
                bool isActive = true;
                List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

                _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
                _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

                _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                        It.IsAny<FilterDefinition<ExpencesMongo>>(),
                        null,
                        It.IsAny<CancellationToken>()))
                    .Throws<TimeoutException>();

                _persistence = new ExpencesPersistence(_mockDbprovider.Object);

                Assert.Throws<TimeoutException>(() => _persistence.GetByActive(isActive));
        }

        [Test]
        public void GetByActiveExceptionTest()
        {
            bool isActive = true;
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetByActive(isActive));
        }

        [Test]
        public void AddExpenceAndNewDocumentTest()
        {
            List<ExpencesMongo> expencesOnDbNoData = [];
            List<ExpencesMongo> expencesOnDbWithData = [ExpencesFixture.ExpencesMongo()];
            ExpencesMongo expencesMongoToAdd = new ExpencesMongo()
            {
                Id = Guid.Empty,
                RunningMonth = CURRENT_MONTH,
                IsActive = true,
                Expences = [ExpencesFixture.GetExpenceMongo()]
            };
            Expence expenceToAdd = ExpencesFixture.GetExpence();

            Mock<IAsyncCursor<ExpencesMongo>> mockWithNoData = new Mock<IAsyncCursor<ExpencesMongo>>();
            mockWithNoData.Setup(x => x.Current).Returns(expencesOnDbNoData);
            mockWithNoData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<ExpencesMongo>> mockWithData = new Mock<IAsyncCursor<ExpencesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(expencesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithNoData.Object)
                .Returns(mockWithData.Object);

            _mockMongoCollection.Setup(m => m.InsertOne(expencesMongoToAdd, default, It.IsAny<CancellationToken>()));

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.AddExpence(expenceToAdd);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void AddExpenceTest()
        {
            List<ExpencesMongo> expencesOnDbWithData = [ExpencesFixture.ExpencesMongo()];
            Expence expenceToAdd = ExpencesFixture.GetExpence();

            Mock<IAsyncCursor<ExpencesMongo>> mockWithData = new Mock<IAsyncCursor<ExpencesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(expencesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.AddExpence(expenceToAdd);
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void AddExpenceTimeoutExceptionTest()
        {
            List<ExpencesMongo> expencesOnDbWithData = [ExpencesFixture.ExpencesMongo()];
            Expence expenceToAdd = ExpencesFixture.GetExpence();

            Mock<IAsyncCursor<ExpencesMongo>> mockWithData = new Mock<IAsyncCursor<ExpencesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(expencesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.AddExpence(expenceToAdd));
        }

        [Test]
        public void AddExpenceExceptionTest()
        {
            List<ExpencesMongo> expencesOnDbWithData = [ExpencesFixture.ExpencesMongo()];
            Expence expenceToAdd = ExpencesFixture.GetExpence();

            Mock<IAsyncCursor<ExpencesMongo>> mockWithData = new Mock<IAsyncCursor<ExpencesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(expencesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.AddExpence(expenceToAdd));
        }

        [Test]
        public void AddExpenceExceptionIfMoreThanOneActiveMonthTest()
        {
            List<ExpencesMongo> expencesOnDbNoData = [];
            List<ExpencesMongo> expencesOnDbWithData = [ExpencesFixture.ExpencesMongo()];
            ExpencesMongo expencesMongoToAdd = new ExpencesMongo()
            {
                Id = Guid.Empty,
                RunningMonth = CURRENT_MONTH,
                IsActive = true,
                Expences = [ExpencesFixture.GetExpenceMongo()]
            };
            Expence expenceToAdd = ExpencesFixture.GetExpence();

            Mock<IAsyncCursor<ExpencesMongo>> mockWithData = new Mock<IAsyncCursor<ExpencesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(expencesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            _mockMongoCollection.Setup(m => m.InsertOne(expencesMongoToAdd, default, It.IsAny<CancellationToken>()));

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<DataException>(() => _persistence.AddExpence(expenceToAdd));
        }

        [Test]
        public void GetExpenceTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.GetExpence(id);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.EqualTo(id));

        }

        [Test]
        public void GetExpenceTimeoutExceptionTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetExpence(id));
        }

        [Test]
        public void GetExpenceExceptionTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            List<ExpencesMongo> expencesOnDb = [ExpencesFixture.ExpencesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(expencesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<ExpencesMongo>(
                    It.IsAny<FilterDefinition<ExpencesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetExpence(id));
        }

        [Test]
        public void CreateNewExpencesDocumentTest()
        {

            _mockMongoCollection.Setup(m => m.InsertOne(
                It.IsAny<ExpencesMongo>(),
                default,
                It.IsAny<CancellationToken>())
            );

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.CreateNewExpencesDocument("2024/2");

            Assert.That(res, Is.True);
        }

        [Test]
        public void CreateNewExpencesDocumentTimeoutExceptionTest()
        {
            _mockMongoCollection.Setup(m => m.InsertOne(
                It.IsAny<ExpencesMongo>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>())
            ).Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.CreateNewExpencesDocument("2024/2"));

        }

        [Test]
        public void CreateNewExpencesDocumentExceptionTest()
        {
            _mockMongoCollection.Setup(m => m.InsertOne(
                It.IsAny<ExpencesMongo>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>())
            ).Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.CreateNewExpencesDocument("2024/2"));
        }

        [Test]
        public void UpdateTest()
        {
            Expence expence = ExpencesFixture.GetExpence();
            expence.Name = "wea";

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.Update(expence);

            Assert.That(res, Is.True);
        }

        [Test]
        public void UpdateTimeoutExceptionTest()
        {
            Expence expence = ExpencesFixture.GetExpence();
            expence.Name = "wea";

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.Update(expence));
        }

        [Test]
        public void UpdateExceptionTest()
        {
            Expence expence = ExpencesFixture.GetExpence();
            expence.Name = "wea";

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.Update(expence));
        }

        [Test]
        public void UpdateExpencesIsActiveTest()
        {
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.UpdateExpencesIsActive(false, "2024/1");

            Assert.That(res, Is.True);
        }

        [Test]
        public void UpdateExpencesIsActiveTimeoutExceptionTest()
        {
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
               .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.UpdateExpencesIsActive(false, "2024/1"));
        }

        [Test]
        public void UpdateExpencesIsActiveExceptionTest()
        {
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
               .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.UpdateExpencesIsActive(false, "2024/1"));
        }

        [Test]
        public void DeleteSuccessTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.Delete(id);

            Assert.That(res, Is.True);
        }

        [Test]
        public void DeleteFailTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(false);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            var res = _persistence.Delete(id);

            Assert.That(res, Is.False);
        }

        [Test]
        public void DeleteTimeoutExceptionTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(false);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.Delete(id));
        }

        [Test]
        public void DeleteExceptionTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(false);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateDefinition<ExpencesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ExpencesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.Delete(id));
        }
    }
}