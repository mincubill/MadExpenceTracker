using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Controllers;
using MadExpenceTracker.Server.Model;
using MadExpenceTracker.Server.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MadExpenceTracker.Server.Test
{
    public class IncomeControllerTest
    {
        Mock<IIncomeService> _serviceMock;
        IncomeController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IIncomeService>();
            _controller = new IncomeController(_serviceMock.Object);
        }

        [Test]
        public void CreateIncomeTest()
        {
            Incomes incomes = IncomesFixture.GetIncomes();
            IncomeApi incomeApi = IncomesFixture.GetIncomeApi();

            _serviceMock.Setup(x => x.Create(It.IsAny<Income>())).Returns(incomes);

            var res = _controller.CreateIncome(incomeApi) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public void UpdateIncomeTest()
        {
            IncomeApi incomeApi = IncomesFixture.GetIncomeApi();
            Income income = IncomesFixture.GetIncome();

            _serviceMock.Setup(x => x.Update(It.IsAny<Income>())).Returns(income);

            var res = _controller.UpdateIncome(incomeApi) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(202));
        }

        [Test]
        public void GetIncomeTest()
        {
            IncomeApi incomeApi = IncomesFixture.GetIncomeApi();
            Income income = IncomesFixture.GetIncome();

            _serviceMock.Setup(x => x.GetIncome(It.IsAny<Guid>())).Returns(income);

            var res = _controller.GetIncome(incomeApi.Id) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetIncomesTest()
        {
            IncomeApi incomeApi = IncomesFixture.GetIncomeApi();

            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(true);

            var res = _controller.DeleteIncome(incomeApi.Id) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void DeleteIncomeTest()
        {
            List<Incomes> incomes = [IncomesFixture.GetIncomes()];

            _serviceMock.Setup(x => x.GetAll()).Returns(incomes);

            var res = _controller.GetIncomes() as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetIncomesByIdTest()
        {
            List<Incomes> incomes = [IncomesFixture.GetIncomes()];
            Guid id = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0");

            _serviceMock.Setup(x => x.GetAll()).Returns(incomes);

            var res = _controller.GetIncomesById(id) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetCurrentIncomesTest()
        {
            Incomes incomes = IncomesFixture.GetIncomes();

            _serviceMock.Setup(x => x.GetIncomes(true)).Returns(incomes);

            var res = _controller.GetCurrentIncomes() as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }
    }
}
