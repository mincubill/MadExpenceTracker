using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Controllers;
using MadExpenceTracker.Server.Model;
using MadExpenceTracker.Server.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MadExpenceTracker.Server.Test
{
    public class AmountControllerTests
    {
        Mock<IAmountsService> _serviceMock;
        AmountController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IAmountsService>();
            _controller = new AmountController(_serviceMock.Object);
        }

        [Test]
        public void GetAmountByIdTest()
        {
            var id = Guid.Parse("df592bf9-ee3a-4a12-bd83-2c5817c150ed");
            Amount amount = AmountFixture.GetAmount();

            _serviceMock.Setup(s => s.GetAmount(id)).Returns(amount);

            var res = _controller.GetAmountById(id) as OkObjectResult;
            AmountApi? resData = res.Value as AmountApi;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(resData, Is.Not.Null);
            Assert.That(resData.Savings, Is.EqualTo(200000));
        }

        [Test]
        public void CalculatedAmountTest()
        {
            Guid expencesId = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");
            Guid incomesId = Guid.Parse("c5a76b10-96ea-4c14-b78a-2d01cac942e0");
            Amount amount = AmountFixture.GetAmount();

            _serviceMock.Setup(s => s.GetAmount(expencesId, incomesId)).Returns(amount);

            var res = _controller.CalculateAmounts(expencesId, incomesId) as OkObjectResult;
            AmountApi? resData = res.Value as AmountApi;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
            Assert.That(resData, Is.Not.Null);
            Assert.That(resData.Savings, Is.EqualTo(200000));
        }

        [Test]
        public void SaveAmountsTest()
        {
            AmountApi amountApi = AmountFixture.GetAmountApi();
            Amounts amounts = AmountFixture.GetAmounts();

            _serviceMock.Setup(s => s.Create(It.IsAny<Amount>())).Returns(amounts);

            var res = _controller.SaveAmounts(amountApi) as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public void GetAmountsTest()
        {
            Amounts amounts = AmountFixture.GetAmounts();

            _serviceMock.Setup(s => s.GetAmounts()).Returns(amounts);

            var res = _controller.GetAmounts() as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

        }
    }
}