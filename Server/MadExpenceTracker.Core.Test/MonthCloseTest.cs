using MadExpenceTracker.Core.Exceptions;
using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Test.Fixture;
using MadExpenceTracker.Core.UseCase;
using Moq;

namespace MadExpenceTracker.Core.Test
{
    public class MonthCloseUseCaseTest
    {
        
        private Mock<IExpencePersistence> _expencesPersistence;
        private Mock<IIncomesPersistence> _incomePersistence;
        private Mock<IAmountsPersistence> _amountPersistence;
        private Mock<IConfigurationPersistence> _configurationPersistence;
        private Mock<IMonthIndexPersistence> _indexingPersistence;
        private readonly string CURRENT_MONTH = $"{DateTime.Now.Year}/{DateTime.Now.Month}";

        private IMonthClose _monthClose;
        [SetUp]
        public void Setup()
        {
            _expencesPersistence = new Mock<IExpencePersistence>();
            _incomePersistence = new Mock<IIncomesPersistence>();
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
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument(CURRENT_MONTH)).Returns(true);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument(CURRENT_MONTH)).Returns(true);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, CURRENT_MONTH)).Returns(true);
            _incomePersistence.Setup(i => i.UpdateIncomesIsActive(false, CURRENT_MONTH)).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());

            MonthIndex res = _monthClose.CloseMonth(
                ExpencesFixture.GetExpences(),
                IncomesFixture.GetIncomes(),
                AmountFixture.GetAmount());
            Assert.That(res.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void MonthCloseExceptionTest()
        {
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument(CURRENT_MONTH)).Returns(false);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument(CURRENT_MONTH)).Returns(true);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, CURRENT_MONTH)).Returns(true);
            _incomePersistence.Setup(i => i.UpdateIncomesIsActive(false, CURRENT_MONTH)).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());

            Assert.Throws<MonthCloseException>(() => {
                _monthClose.CloseMonth(
                ExpencesFixture.GetExpences(),
                IncomesFixture.GetIncomes(),
                AmountFixture.GetAmount());
            });
        }
    }
}