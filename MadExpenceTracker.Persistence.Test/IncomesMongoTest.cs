using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver;
using Moq;

namespace MadExpenceTracker.Persistence.Test
{
    public class IncomesMongoTest
    {
        Mock<IMongoCollection<IncomeMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<IncomeMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IAmountsPersistence _persistence;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<IncomeMongo>>();
            _mockCursor = new Mock<IAsyncCursor<IncomeMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
            _mockDbprovider.Setup(x => x.GetCollection<IncomeMongo>("income"))
                .Returns(_mockMongoCollection.Object);
        }

        [Test]
        public void GetAllTest()
        {

        }

        [Test]
        public void GetAllTimeoutExceptionTest()
        {

        }

        [Test]
        public void GetAllExceptionTest()
        {

        }

        [Test]
        public void GetByIdTest()
        {

        }

        [Test]
        public void GetByIdTimeoutExceptionTest()
        {

        }

        [Test]
        public void GetByIdExceptionTest()
        {

        }

        [Test]
        public void GetByActiveTest()
        {

        }

        [Test]
        public void GetByActiveTimeoutExceptionTest()
        {

        }

        [Test]
        public void GetByActiveExceptionTest()
        {

        }

        [Test]
        public void AddIncomeTest()
        {

        }

        [Test]
        public void AddIncomeTimeoutExceptionTest()
        {

        }

        [Test]
        public void AddIncomeExceptionTest()
        {

        }

        [Test]
        public void Test()
        {

        }

        [Test]
        public void TimeoutExceptionTest()
        {

        }

        [Test]
        public void ExceptionTest()
        {

        }

        [Test]
        public void CreateNewIncomesDocumentTest()
        {

        }

        [Test]
        public void CreateNewIncomesDocumentTimeoutExceptionTest()
        {

        }

        [Test]
        public void CreateNewIncomesDocumentExceptionTest()
        {

        }

        [Test]
        public void UpdateTest()
        {

        }

        [Test]
        public void UpdateTimeoutExceptionTest()
        {

        }

        [Test]
        public void UpdateExceptionTest()
        {

        }

        [Test]
        public void UpdateIncomesIsActiveTest()
        {

        }

        [Test]
        public void UpdateIncomesIsActiveTimeoutExceptionTest()
        {

        }

        [Test]
        public void UpdateIncomesIsActiveExceptionTest()
        {

        }

        [Test]
        public void DeleteTest()
        {

        }

        [Test]
        public void DeleteTimeoutExceptionTest()
        {

        }

        [Test]
        public void DeleteExceptionTest()
        {

        }

    }
}
