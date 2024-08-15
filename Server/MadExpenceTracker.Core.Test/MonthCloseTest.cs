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
            string month = "2024/8";
            string newMonth = CURRENT_MONTH == month ? $"{DateTime.Now.Year}/{DateTime.Now.AddMonths(1).Month}" : month;
            Expences expences = ExpencesFixture.GetExpences();
            Incomes incomes = IncomesFixture.GetIncomes();
            _expencesPersistence.Setup(e => e.Get(expences.Id)).Returns(expences);
            _incomePersistence.Setup(i => i.Get(incomes.Id)).Returns(incomes);
            _expencesPersistence.Setup(e => e.IsMonthClosed(expences.RunningMonth)).Returns(true);
            _incomePersistence.Setup(e => e.IsMonthClosed(incomes.RunningMonth)).Returns(true);
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument(newMonth)).Returns(true);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument(newMonth)).Returns(true);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, expences.RunningMonth)).Returns(true);
            _incomePersistence.Setup(i => i.UpdateIncomesIsActive(false, incomes.RunningMonth)).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());

            MonthIndex res = _monthClose.CloseMonth(
                month,
                ExpencesFixture.GetExpences().Id,
                IncomesFixture.GetIncomes().Id);
            Assert.That(res.Id, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void MonthCloseExceptionTest()
        {
            string month = "2024/8";
            string newMonth = CURRENT_MONTH == month ? $"{DateTime.Now.Year}/{DateTime.Now.AddMonths(1).Month}" : month;
            Expences expences = ExpencesFixture.GetExpences();
            Incomes incomes = IncomesFixture.GetIncomes();
            _expencesPersistence.Setup(e => e.Get(expences.Id)).Returns(expences);
            _incomePersistence.Setup(i => i.Get(incomes.Id)).Returns(incomes);
            _expencesPersistence.Setup(e => e.IsMonthClosed(expences.RunningMonth)).Returns(true);
            _incomePersistence.Setup(e => e.IsMonthClosed(incomes.RunningMonth)).Returns(true);
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument(month)).Returns(true);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument(month)).Returns(false);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, expences.RunningMonth)).Returns(true);
            _incomePersistence.Setup(i => i.UpdateIncomesIsActive(false, incomes.RunningMonth)).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());


            Assert.Throws<MonthCloseException>(() => {
                _monthClose.CloseMonth(
                month,
                ExpencesFixture.GetExpences().Id,
                IncomesFixture.GetIncomes().Id);
            });
        }

        [Test]
        public void MonthCloseInvalidOperationExceptionTest()
        {
            Expences expences = ExpencesFixture.GetExpences();
            Incomes incomes = IncomesFixture.GetIncomes();
            _expencesPersistence.Setup(e => e.Get(expences.Id)).Returns(expences);
            _incomePersistence.Setup(i => i.Get(incomes.Id)).Returns(incomes);
            _expencesPersistence.Setup(e => e.IsMonthClosed(expences.RunningMonth)).Returns(false);
            _incomePersistence.Setup(e => e.IsMonthClosed(incomes.RunningMonth)).Returns(true);
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument(CURRENT_MONTH)).Returns(true);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument(CURRENT_MONTH)).Returns(true);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, expences.RunningMonth)).Returns(true);
            _incomePersistence.Setup(i => i.UpdateIncomesIsActive(false, incomes.RunningMonth)).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());


            Assert.Throws<InvalidOperationException>(() => {
                _monthClose.CloseMonth(
                "2023/12",
                ExpencesFixture.GetExpences().Id,
                IncomesFixture.GetIncomes().Id);
            });
        }

        [Test]
        public void MonthCloseNotFoundExceptionTest()
        {
            Expences expences = ExpencesFixture.GetExpences();
            Incomes incomes = IncomesFixture.GetIncomes();
            _expencesPersistence.Setup(e => e.Get(expences.Id));
            _incomePersistence.Setup(i => i.Get(incomes.Id)).Returns(incomes);
            _expencesPersistence.Setup(e => e.IsMonthClosed(expences.RunningMonth)).Returns(false);
            _incomePersistence.Setup(e => e.IsMonthClosed(incomes.RunningMonth)).Returns(true);
            _expencesPersistence.Setup(e => e.CreateNewExpencesDocument(CURRENT_MONTH)).Returns(true);
            _incomePersistence.Setup(i => i.CreateNewIncomeDocument(CURRENT_MONTH)).Returns(true);
            _expencesPersistence.Setup(e => e.UpdateExpencesIsActive(false, expences.RunningMonth)).Returns(true);
            _incomePersistence.Setup(i => i.UpdateIncomesIsActive(false, incomes.RunningMonth)).Returns(true);
            _amountPersistence.Setup(a => a.AddAmount(AmountFixture.GetAmount())).Returns(AmountFixture.GetAmounts());
            _configurationPersistence.Setup(c => c.GetConfiguration()).Returns(ConfigurationFixture.GetConfiguration());


            Assert.Throws<NotFoundException>(() => {
                _monthClose.CloseMonth(
                "2023/12",
                ExpencesFixture.GetExpences().Id,
                IncomesFixture.GetIncomes().Id);
            });
        }
    }
}