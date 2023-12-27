using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Test.Fixture;
using MadExpenceTracker.Core.UseCase;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using Moq;

namespace MadExpenceTracker.Core.Test
{
    public class Tests
    {
        
        private Mock<IExpencePersistence> _expencesPersistence;
        private Mock<IIncomePersistence> _incomePersistence;
        private Mock<IAmountsPersistence> _amountPersistence;
        private Mock<IConfigurationPersistence> _configurationPersistence;
        private Mock<IMonthIndexPersistence> _indexingPersistence;

        private IMonthClose _monthClose;
        [SetUp]
        public void Setup()
        {
            Mock<Connection> mongoConnection = new Mock<Connection>();
            Mock<MongoClient> mongoClient = new Mock<MongoClient>("mongodb://localhost:27017");
            IMongoDatabase mongoDatabase = mongoConnection.Object.GetDatabase(mongoClient.Object);

            _expencesPersistence = new Mock<IExpencePersistence>();
            _incomePersistence = new Mock<IIncomePersistence>();
            _amountPersistence = new Mock<IAmountsPersistence>();
            _configurationPersistence = new Mock<IConfigurationPersistence>();
            _indexingPersistence = new Mock<IMonthIndexPersistence>();
            _monthClose = new MonthClose(
                _expencesPersistence.Object,
                _incomePersistence.Object,
                _amountPersistence.Object,
                _configurationPersistence.Object,
                _indexingPersistence.Object);
        }

        [Test]
        public void MonthCloseTest()
        {
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument("2023/12")).Returns(true);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument("2023/12")).Returns(true);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, "2023/12")).Returns(true);
            _incomePersistence.Setup(i => i.UpdateExpencesIsActive(false, "2023/12")).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());

            MonthIndex res = _monthClose.CloseMonth(
                ExpencesFixture.GetExpences(),
                IncomesFixture.GetIncomes(),
                AmountFixture.GetAmount());
            Assert.That(res.Id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}