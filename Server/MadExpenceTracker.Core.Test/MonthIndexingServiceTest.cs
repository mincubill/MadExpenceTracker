using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Test.Fixture;
using Moq;
using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Services;

namespace MadExpenceTracker.Core.Test
{
    public class MonthIndexingServiceTest
    {
        private Mock<IMonthIndexPersistence> _monthIndexPersistenceMock;
        private IMonthIndexingService _service;

        [SetUp]
        public void Setup()
        {
            _monthIndexPersistenceMock = new Mock<IMonthIndexPersistence>();
            _service = new MonthIndexingService(_monthIndexPersistenceMock.Object);
        }

        [Test]
        public void GetMonthsIndexesTest()
        {
            MonthIndexes indexes = MonthIndexFixture.GetMonthIndexes();

            _monthIndexPersistenceMock.Setup(m => m.GetMonthsIndexes()).Returns(indexes);

            MonthIndexes res = _service.GetMonthsIndexes();
            
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetMonthsIndexTest()
        {
            Guid id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac05489");
            MonthIndex index = MonthIndexFixture.GetMonthIndex();

            _monthIndexPersistenceMock.Setup(m => m.GetMonthIndex(id)).Returns(index);

            MonthIndex res = _service.GetMonthsIndex(id);
            
            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void AddMonthIndexTest()
        {
            MonthIndex index = MonthIndexFixture.GetMonthIndex();
            MonthIndexes indexes = MonthIndexFixture.GetMonthIndexes();

            _monthIndexPersistenceMock.Setup(i => i.AddMonthIndex(index)).Returns(indexes);

            MonthIndexes res = _service.AddMonthIndex(index);

            Assert.That(res, Is.Not.Null);
        }
    }
}