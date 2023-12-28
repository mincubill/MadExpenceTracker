using MadExpenceTracker.Core.Interfaces.Services;
using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Core.Services;
using MadExpenceTracker.Core.Test.Fixture;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Core.Test
{
    public class ConfigurationServiceTest
    {
        Mock<IConfigurationPersistence> _configurationPersistenceMock;
        IConfigurationService _configurationService;

        [SetUp]
        public void Setup()
        {
            _configurationPersistenceMock = new Mock<IConfigurationPersistence>();
            _configurationService = new ConfigurationService(_configurationPersistenceMock.Object);
        }

        [Test]
        public void GetConfigurationTest()
        {
            Configuration config = ConfigurationFixture.GetConfiguration();

            _configurationPersistenceMock.Setup(p => p.GetConfiguration()).Returns(config);

            Configuration res = _configurationService.GetConfiguration();

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void SetConfigurationTest()
        {
            Configuration config = ConfigurationFixture.GetConfiguration();

            _configurationPersistenceMock.Setup(p => p.SetConfiguration(config)).Returns(config);

            Configuration res = _configurationService.SetConfiguration(config);

            Assert.That(res, Is.Not.Null);
        }

        [Test]
        public void UpdateConfigurationTest()
        {
            Configuration config = ConfigurationFixture.GetConfiguration();
            config.SavingsRate = 10;

            _configurationPersistenceMock.Setup(p => p.UpdateConfiguration(config)).Returns(true);

            Configuration res = _configurationService.UpdateConfiguration(config);

            Assert.That(res, Is.Not.Null);
            Assert.That(res.SavingsRate, Is.EqualTo(10));

        }
    }
}
