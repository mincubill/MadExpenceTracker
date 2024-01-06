using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Services;
using MadExpenceTracker.Core.Test.Fixture;
using Moq;

namespace MadExpenceTracker.Core.Test
{
    public class IncomeServiceTest
    {
        Mock<IIncomesPersistence> _incomePersistenceMock;
        IIncomeService _service;

        [SetUp]
        public void Setup()
        {
            _incomePersistenceMock = new Mock<IIncomesPersistence>();
            _service = new IncomeService(_incomePersistenceMock.Object);
        }

        [Test]
        public void GetAllTest()
        {
            IEnumerable<Incomes> incomes = new List<Incomes>() { IncomesFixture.GetIncomes() };
            _incomePersistenceMock.Setup(i => i.GetAll()).Returns(incomes);

            IEnumerable<Incomes> res = _service.GetAll();

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetIncomesByIdTest()
        {
            Guid id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0");
            Incomes incomes = IncomesFixture.GetIncomes();
            _incomePersistenceMock.Setup(i => i.Get(id)).Returns(incomes);

            Incomes res = _service.GetIncomes(id);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetIncomesByIsActiveTest()
        {
            bool isActive = true;
            Incomes incomes = IncomesFixture.GetIncomes();
            _incomePersistenceMock.Setup(i => i.GetByActive(isActive)).Returns(incomes);

            Incomes res = _service.GetIncomes(isActive);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void GetIncomeTest()
        {
            Guid id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d");
            Income income = IncomesFixture.GetIncome();

            _incomePersistenceMock.Setup(i => i.GetIncome(id)).Returns(income);

            Income res = _service.GetIncome(id);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Name, Is.EqualTo("Sueldo"));
        }

        [Test]
        public void CreateTest()
        {
            Income income = IncomesFixture.GetIncome();
            Incomes incomes = IncomesFixture.GetIncomes();

            _incomePersistenceMock.Setup(i => i.AddIncome(income)).Returns(incomes);

            Incomes res = _service.Create(income);
            Assert.That(res, Is.Not.Null);

        }

        [Test]
        public void CreateIncomeWithoutIdTest()
        {
            Income income = IncomesFixture.GetIncome();
            income.Id = Guid.Empty;
            Incomes incomes = IncomesFixture.GetIncomes();

            _incomePersistenceMock.Setup(i => i.AddIncome(income)).Returns(incomes);

            Incomes res = _service.Create(income);
            Assert.That(res, Is.Not.Null);

        }

        [Test]
        public void CreateNewMonthTest()
        {
            _incomePersistenceMock.Setup(i => i.CreateNewIncomeDocument($"{DateTime.Now.Year}/{DateTime.Now.Month}")).Returns(true);

            bool res = _service.CreateNewMonth();
            Assert.That(res, Is.True);
        }

        [Test]
        public void UpdateTest()
        {
            string expected = "updated";
            Income income = IncomesFixture.GetIncome();
            income.Name = "updated";

            _incomePersistenceMock.Setup(i => i.Update(income)).Returns(true);

            Income res = _service.Update(income);
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Name, Is.EqualTo(expected));
        }

        [Test]
        public void UpdateFailTest()
        {
            Income income = IncomesFixture.GetIncome();
            income.Name = "updated";

            _incomePersistenceMock.Setup(i => i.Update(income)).Returns(false);

            Assert.Throws<Exception>(() => _service.Update(income));
        }

        [Test]
        public void DeleteTest()
        {
            Guid id = Guid.Parse("69cf8cbf-6ef1-4e0a-856a-056d95d7977d");

            _incomePersistenceMock.Setup(i => i.Delete(id)).Returns(true);

            bool res = _service.Delete(id);
            Assert.That(res, Is.True);
        }

        [Test]
        public void CloseMonthTest()
        {
            bool isActive = false;
            string month = "2023/12";
            _incomePersistenceMock.Setup(i => i.UpdateIncomesIsActive(isActive, month)).Returns(true);

            bool res = _service.CloseMonth(month);
            Assert.That(res, Is.True);
        }
    }
}
