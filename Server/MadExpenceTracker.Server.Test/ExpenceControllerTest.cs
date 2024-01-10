using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Server.Controllers;
using MadExpenceTracker.Server.Model;
using MadExpenceTracker.Server.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MadExpenceTracker.Server.Test
{
    public class ExpenceControllerTest
    {
        Mock<IExpencesService> _serviceMock;
        ExpenceController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IExpencesService>();
            _controller = new ExpenceController( _serviceMock.Object );
        }

        [Test]
        public void CreateExpenceTest()
        {
            Expences expences = ExpencesFixture.GetExpences();
            ExpenceApi expenceApi = ExpencesFixture.GetExpenceApi();

            _serviceMock.Setup(x => x.Create(It.IsAny<Expence>())).Returns(expences);

            var res = _controller.CreateExpence(expenceApi) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(201));

        }

        [Test]
        public void UpdateExpenceTest()
        {
            ExpenceApi expenceApi = ExpencesFixture.GetExpenceApi();
            Expence expence = ExpencesFixture.GetExpence();

            _serviceMock.Setup(x => x.Update(It.IsAny<Expence>())).Returns(expence);

            var res = _controller.UpdateExpence(expenceApi) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(202));
        }

        [Test]
        public void GetExpenceTest()
        {
            ExpenceApi expenceApi = ExpencesFixture.GetExpenceApi();
            Expence expence = ExpencesFixture.GetExpence();

            _serviceMock.Setup(x => x.GetExpence(It.IsAny<Guid>())).Returns(expence);

            var res = _controller.GetExpence(expenceApi.Id) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void DeleteExpenceTest()
        {
            ExpenceApi expenceApi = ExpencesFixture.GetExpenceApi();

            _serviceMock.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(true);

            var res = _controller.DeleteExpence(expenceApi.Id) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetExpencesTest()
        {
            List<Expences> expences = [ ExpencesFixture.GetExpences() ];

            _serviceMock.Setup(x => x.GetAll()).Returns(expences);

            var res = _controller.GetExpences() as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetExpencesByIdTest()
        {
            Expences expences = ExpencesFixture.GetExpences();
            Guid id = Guid.Parse("fd76aa75-1628-4fd6-960a-64d62febbd9f");

            _serviceMock.Setup(x => x.GetExpences(id)).Returns(expences);

            var res = _controller.GetExpencesById(id) as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetCurrentExpencesTest()
        {
            Expences expences = ExpencesFixture.GetExpences();

            _serviceMock.Setup(x => x.GetExpences(true)).Returns(expences);

            var res = _controller.GetCurrentExpences() as ObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo(200));
        }
    }
}
