using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MongoDB.Driver;
using Moq;
using System.Data;

namespace MadExpenceTracker.Persistence.Test
{
    public class ExpencesMongoTest
    {
        Mock<IMongoCollection<ExpenceMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<ExpenceMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        IAmountsPersistence _persistence;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<ExpenceMongo>>();
            _mockCursor = new Mock<IAsyncCursor<ExpenceMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
            _mockDbprovider.Setup(x => x.GetCollection<ExpenceMongo>("expences"))
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
        public void AddExpenceTest()
        {

        }

        [Test]
        public void AddExpenceTimeoutExceptionTest()
        {

        }

        [Test]
        public void AddExpenceExceptionTest()
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
        public void CreateNewExpencesDocumentTest()
        {

        }

        [Test]
        public void CreateNewExpencesDocumentTimeoutExceptionTest()
        {

        }

        [Test]
        public void CreateNewExpencesDocumentExceptionTest()
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
        public void UpdateExpencesIsActiveTest()
        {

        }

        [Test]
        public void UpdateExpencesIsActiveTimeoutExceptionTest()
        {

        }

        [Test]
        public void UpdateExpencesIsActiveExceptionTest()
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