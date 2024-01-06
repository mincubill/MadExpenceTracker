using MadExpenceTracker.Core.Interfaces.Services;
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
    public class ConfigurationControllerTest
    {
        Mock<IConfigurationService> _serviceMock;
        ConfigurationController _controller;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IConfigurationService>();
            _controller = new ConfigurationController(_serviceMock.Object);
        }

        [Test]
        public void GetConfigurationTest()
        {
            Configuration config = ConfigurationFixture.GetConfiguration();
            
            _serviceMock.Setup(x =>  x.GetConfiguration()).Returns(config);

            var res = _controller.GetConfiguration() as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public void SaveConfigurationTest()
        {
            ConfigurationApi configApi = ConfigurationFixture.GetConfigurationApi();
            Configuration config = ConfigurationFixture.GetConfiguration();

            _serviceMock.Setup(x => x.SetConfiguration(It.IsAny<Configuration>())).Returns(config);

            var res = _controller.SaveConfiguration(configApi) as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public void UpdateConfigurationTest()
        {
            ConfigurationApi configApi = ConfigurationFixture.GetConfigurationApi();
            Configuration config = ConfigurationFixture.GetConfiguration();

            _serviceMock.Setup(x => x.UpdateConfiguration(It.IsAny<Configuration>())).Returns(config);

            var res = _controller.UpdateConfiguration(configApi) as OkObjectResult;

            Assert.That(res, Is.Not.Null);
            Assert.That(res.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }
    }
}
