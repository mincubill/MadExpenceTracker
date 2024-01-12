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
    public class MonthIndexControllerTest
    {
        Mock<IMonthIndexingService> _serviceMock;
        MonthIndexController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IMonthIndexingService>();
            _controller = new MonthIndexController(_serviceMock.Object);
        }

        [Test]
        public void GetMonthIndexTest()
        {
            MonthIndexes indexes = MonthIndexFixture.GetMonthIndexes();

            _serviceMock.Setup(x => x.GetMonthsIndexes()).Returns(indexes);

            var res = _controller.GetMonthIndex() as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));

        }

        [Test]
        public void SaveMonthIndexTest()
        {
            MonthIndexApi indexApi = MonthIndexFixture.GetMonthIndexApi();
            MonthIndexes indexes = MonthIndexFixture.GetMonthIndexes();

            _serviceMock.Setup(x => x.AddMonthIndex(It.IsAny<MonthIndex>())).Returns(indexes);

            var res = _controller.SaveMonthIndex(indexApi) as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }
    }
}
