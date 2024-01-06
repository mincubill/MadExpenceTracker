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
    public class IncomesMongoTest
    {
        Mock<IMongoCollection<IncomesMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<IncomesMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IIncomesPersistence _persistence;
        private readonly string CURRENT_MONTH = $"{DateTime.Now.Year}/{DateTime.Now.Month}";

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<IncomesMongo>>();
            _mockCursor = new Mock<IAsyncCursor<IncomesMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
            _mockDbprovider.Setup(x => x.GetCollection<IncomesMongo>("income"))
                .Returns(_mockMongoCollection.Object);
        }

        [Test]
        public void GetAllTest()
        {
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                Builders<IncomesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var result = _persistence.GetAll().ToList();
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAllTimeoutExceptionTest()
        {
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                Builders<IncomesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetAll().ToList());
        }

        [Test]
        public void GetAllExceptionTest()
        {
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                Builders<IncomesMongo>.Filter.Empty,
                null,
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetAll().ToList());
        }

        [Test]
        public void GetByIdTest()
        {
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var result = _persistence.Get(id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GetByIdTimeoutExceptionTest()
        {
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.Get(id));
        }

        [Test]
        public void GetByIdExceptionTest()
        {
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.Get(id));
        }

        [Test]
        public void GetByActiveTest()
        {
            bool isActive = true;
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var result = _persistence.GetByActive(isActive);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GetByActiveTimeoutExceptionTest()
        {
            bool isActive = true;
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetByActive(isActive));
        }

        [Test]
        public void GetByActiveExceptionTest()
        {
            bool isActive = true;
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetByActive(isActive));
        }

        [Test]
        public void AddIncomeAndNewDocumentTest()
        {
            List<IncomesMongo> incomesOnDbNoData = [];
            List<IncomesMongo> incomesOnDbWithData = [IncomesFixture.GetIncomesMongo()];
            IncomesMongo incomesMongoToAdd = new IncomesMongo()
            {
                Id = Guid.Empty,
                RunningMonth = CURRENT_MONTH,
                IsActive = true,
                Incomes = [IncomesFixture.GetIncomeMongo()]
            };
            Income incomeToAdd = IncomesFixture.GetIncome();

            Mock<IAsyncCursor<IncomesMongo>> mockWithNoData = new Mock<IAsyncCursor<IncomesMongo>>();
            mockWithNoData.Setup(x => x.Current).Returns(incomesOnDbNoData);
            mockWithNoData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<IncomesMongo>> mockWithData = new Mock<IAsyncCursor<IncomesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(incomesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithNoData.Object)
                .Returns(mockWithData.Object);

            _mockMongoCollection.Setup(m => m.InsertOne(incomesMongoToAdd, default, It.IsAny<CancellationToken>()));

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var res = _persistence.AddIncome(incomeToAdd);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void AddIncomeTest()
        {
            List<IncomesMongo> incomesOnDbWithData = [IncomesFixture.GetIncomesMongo()];
            Income incomeToAdd = IncomesFixture.GetIncome();

            Mock<IAsyncCursor<IncomesMongo>> mockWithData = new Mock<IAsyncCursor<IncomesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(incomesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var res = _persistence.AddIncome(incomeToAdd);
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void AddIncomeTimeoutExceptionTest()
        {
            List<IncomesMongo> incomesOnDbWithData = [IncomesFixture.GetIncomesMongo()];
            Income incomeToAdd = IncomesFixture.GetIncome();

            Mock<IAsyncCursor<IncomesMongo>> mockWithData = new Mock<IAsyncCursor<IncomesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(incomesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.AddIncome(incomeToAdd));
        }

        [Test]
        public void AddIncomeExceptionTest()
        {
            List<IncomesMongo> incomesOnDbWithData = [IncomesFixture.GetIncomesMongo()];
            Income incomeToAdd = IncomesFixture.GetIncome();

            Mock<IAsyncCursor<IncomesMongo>> mockWithData = new Mock<IAsyncCursor<IncomesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(incomesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                default,
                It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.AddIncome(incomeToAdd));
        }

        [Test]
        public void AddIncomeExceptionIfMoreThanOneActiveMonthTest()
        {
            List<IncomesMongo> incomesOnDbNoData = [];
            List<IncomesMongo> incomesOnDbWithData = [IncomesFixture.GetIncomesMongo()];
            IncomesMongo incomesMongoToAdd = new IncomesMongo()
            {
                Id = Guid.Empty,
                RunningMonth = CURRENT_MONTH,
                IsActive = true,
                Incomes = [IncomesFixture.GetIncomeMongo()]
            };
            Income incomeToAdd = IncomesFixture.GetIncome();

            Mock<IAsyncCursor<IncomesMongo>> mockWithData = new Mock<IAsyncCursor<IncomesMongo>>();
            mockWithData.Setup(x => x.Current).Returns(incomesOnDbWithData);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithData.Object);

            _mockMongoCollection.Setup(m => m.InsertOne(incomesMongoToAdd, default, It.IsAny<CancellationToken>()));

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<DataException>(() => _persistence.AddIncome(incomeToAdd));
        }

        [Test]
        public void GetIncomeTest()
        {
            Guid id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d");
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var res = _persistence.GetIncome(id);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.Id, Is.EqualTo(id));
        }

        [Test]
        public void GetIncomeTimeoutExceptionTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetIncome(id));
        }

        [Test]
        public void GetIncomeExceptionTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            List<IncomesMongo> incomesOnDb = [IncomesFixture.GetIncomesMongo()];

            _mockCursor.Setup(x => x.Current).Returns(incomesOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);

            _mockMongoCollection.Setup(x => x.FindSync<IncomesMongo>(
                    It.IsAny<FilterDefinition<IncomesMongo>>(),
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetIncome(id));
        }

        [Test]
        public void CreateNewIncomesDocumentTest()
        {
            _mockMongoCollection.Setup(m => m.InsertOne(
                It.IsAny<IncomesMongo>(),
                default,
                It.IsAny<CancellationToken>())
            );

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var res = _persistence.CreateNewIncomeDocument("2024/2");

            Assert.That(res, Is.True);
        }

        [Test]
        public void CreateNewIncomesDocumentTimeoutExceptionTest()
        {
            _mockMongoCollection.Setup(m => m.InsertOne(
                It.IsAny<IncomesMongo>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>())
            ).Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.CreateNewIncomeDocument("2024/2"));
        }

        [Test]
        public void CreateNewIncomesDocumentExceptionTest()
        {
            _mockMongoCollection.Setup(m => m.InsertOne(
                It.IsAny<IncomesMongo>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>())
            ).Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.CreateNewIncomeDocument("2024/2"));
        }

        [Test]
        public void UpdateTest()
        {
            Income income = IncomesFixture.GetIncome();
            income.Name = "wea";

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var res = _persistence.Update(income);

            Assert.That(res, Is.True);
        }

        [Test]
        public void UpdateTimeoutExceptionTest()
        {
            Income income = IncomesFixture.GetIncome();
            income.Name = "wea";

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.Update(income));
        }

        [Test]
        public void UpdateExceptionTest()
        {
            Income income = IncomesFixture.GetIncome();
            income.Name = "wea";

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.Update(income));
        }

        [Test]
        public void UpdateIncomesIsActiveTest()
        {
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            var res = _persistence.UpdateIncomesIsActive(false, "2024/1");

            Assert.That(res, Is.True);
        }

        [Test]
        public void UpdateIncomesIsActiveTimeoutExceptionTest()
        {
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
               .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.UpdateIncomesIsActive(false, "2024/1"));
        }

        [Test]
        public void UpdateIncomesIsActiveExceptionTest()
        {
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
               .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.UpdateIncomesIsActive(false, "2024/1"));
        }

        [Test]
        public void DeleteSuccessTest()
        {
            Guid id = Guid.Parse("e96157a0-f966-4f86-832f-d394de7f75e2");
            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

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
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

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
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

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
                It.IsAny<FilterDefinition<IncomesMongo>>(),
                It.IsAny<UpdateDefinition<IncomesMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new IncomesPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.Delete(id));
        }

    }
}
