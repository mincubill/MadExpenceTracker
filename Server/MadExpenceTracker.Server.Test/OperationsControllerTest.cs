using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Interfaces.UseCase;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Controllers;
using MadExpenceTracker.Server.Model;
using MadExpenceTracker.Server.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Server.Test
{
    public class OperationsControllerTest
    {
        Mock<IMonthClose> _serviceMock;
        OperationsController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IMonthClose>();
            _controller = new OperationsController(_serviceMock.Object);
        }

        [Test]
        public void GetMonthIndexTest()
        {
            MonthIndex monthIndex = MonthIndexFixture.GetMonthIndex();
            MonthResumeApi resume = new MonthResumeApi
            {
                AmountApi = AmountFixture.GetAmountApi(),
                ExpencesApi = ExpencesFixture.GetExpencesApi(),
                IncomesApi = IncomesFixture.GetIncomesApi(),
            };

            _serviceMock.Setup(c => c.CloseMonth(
                It.IsAny<Expences>(),
                It.IsAny<Incomes>(),
                It.IsAny<Amount>()
                )).Returns(monthIndex);

            var res = _controller.CloseMonth(resume) as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }
    }
}
