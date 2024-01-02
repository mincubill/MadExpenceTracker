using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver;
using Moq;

namespace MadExpenceTracker.Persistence.Test
{
    public class MonthIndexPersistenceTest
    {
        Mock<IMongoCollection<MonthIndexesMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<MonthIndexesMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IAmountsPersistence _persistence;

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

        }

        [Test]
        public void GetMonthsIndexesTimeoutExceptionTest()
        {

        }

        [Test]
        public void GetMonthsIndexesExceptionTest()
        {

        }

        [Test]
        public void GetMonthIndexByIdTest()
        {

        }

        [Test]
        public void GetMonthIndexByIdTimeoutExceptionTest()
        {

        }

        [Test]
        public void GetMonthIndexByIdExceptionTest()
        {

        }

        [Test]
        public void AddMonthIndexTest()
        {

        }

        [Test]
        public void AddMonthIndexTimeoutExceptionTest()
        {

        }

        [Test]
        public void AddMonthIndexExceptionTest()
        {

        }
    }
}
