using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Model;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MadExpenceTracker.Persistence.MongoDB.Provider;
using MadExpenceTracker.Persistence.Test.Fixture;
using MongoDB.Driver;
using Moq;

namespace MadExpenceTracker.Persistence.Test
{
    public class ConfigurationPersistenceTest
    {
        Mock<IMongoCollection<ConfigurationMongo>> _mockMongoCollection;
        Mock<IAsyncCursor<ConfigurationMongo>> _mockCursor;
        Mock<IMongoDBProvider> _mockDbprovider;
        private IConfigurationPersistence _persistence;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockMongoCollection = new Mock<IMongoCollection<ConfigurationMongo>>();
            _mockCursor = new Mock<IAsyncCursor<ConfigurationMongo>>();
            _mockDbprovider = new Mock<IMongoDBProvider>();
            _mockDbprovider.Setup(x => x.GetCollection<ConfigurationMongo>("configuration"))
                .Returns(_mockMongoCollection.Object);
        }

        [Test]
        public void GetConfigurationTest()
        {
            List<ConfigurationMongo> configOnDb = [ConfigurationFixture.GetConfigurationMongo()];
            
            _mockCursor.Setup(x => x.Current).Returns(configOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockMongoCollection.Setup(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    null,
                    It.IsAny<CancellationToken>()))
                .Returns(_mockCursor.Object);

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            var result = _persistence.GetConfiguration();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SavingsRate, Is.EqualTo(20));
        }
        
        [Test]
        public void GetConfigurationTimeoOutExceptionTest()
        {
            List<ConfigurationMongo> configOnDb = [ConfigurationFixture.GetConfigurationMongo()];
            
            _mockCursor.Setup(x => x.Current).Returns(configOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockMongoCollection.Setup(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.GetConfiguration());
        }
        
        [Test]
        public void GetConfigurationExceptionTest()
        {
            List<ConfigurationMongo> configOnDb = [ConfigurationFixture.GetConfigurationMongo()];
            
            _mockCursor.Setup(x => x.Current).Returns(configOnDb);
            _mockCursor.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>())).Returns(true);
            _mockMongoCollection.Setup(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    null,
                    It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.GetConfiguration());
        }
        
        [Test]
        public void SetNewConfigurationTest()
        {
            Configuration config = ConfigurationFixture.GetConfiguration();
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            config.SavingsRate = 69;
            configOnDb.SavingsRate = 69;

            Mock<IAsyncCursor<ConfigurationMongo>> mockWithNoData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockWithNoData.Setup(x => x.Current).Returns([]);
            mockWithNoData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            
            Mock<IAsyncCursor<ConfigurationMongo>> mockWithData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockWithData.Setup(x => x.Current).Returns([configOnDb]);
            mockWithData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockWithNoData.Object)
                .Returns(mockWithData.Object);
            
            _mockMongoCollection.Setup(c => c.InsertOne(
                configOnDb, 
                default,
                It.IsAny<CancellationToken>()));

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            var result = _persistence.SetConfiguration(config);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SavingsRate, Is.EqualTo(69));
        }
        
        [Test]
        public void SetUpdatedConfigurationTest()
        {
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            ConfigurationMongo configOnDbUpdated = ConfigurationFixture.GetConfigurationMongo();
            Configuration config = ConfigurationFixture.GetConfiguration();
            config.SavingsRate = 50;
            configOnDbUpdated.SavingsRate = 50;

            Mock<IAsyncCursor<ConfigurationMongo>> mockOldData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockOldData.Setup(x => x.Current).Returns([configOnDb]);
            mockOldData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<ConfigurationMongo>> mockNewData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockNewData.Setup(x => x.Current).Returns([configOnDbUpdated]);
            mockNewData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockOldData.Object).
                Returns(mockNewData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            var result = _persistence.SetConfiguration(config);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SavingsRate, Is.EqualTo(50));
        }

        [Test]
        public void SetConfigurationTimeoutExceptionTest()
        {
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            ConfigurationMongo configOnDbUpdated = ConfigurationFixture.GetConfigurationMongo();
            Configuration config = ConfigurationFixture.GetConfiguration();
            config.SavingsRate = 50;
            configOnDbUpdated.SavingsRate = 50;

            Mock<IAsyncCursor<ConfigurationMongo>> mockOldData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockOldData.Setup(x => x.Current).Returns([configOnDb]);
            mockOldData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<ConfigurationMongo>> mockNewData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockNewData.Setup(x => x.Current).Returns([configOnDbUpdated]);
            mockNewData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockOldData.Object).
                Returns(mockNewData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.SetConfiguration(config));
        }

        [Test]
        public void SetConfigurationExceptionTest()
        {
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            ConfigurationMongo configOnDbUpdated = ConfigurationFixture.GetConfigurationMongo();
            Configuration config = ConfigurationFixture.GetConfiguration();
            config.SavingsRate = 50;
            configOnDbUpdated.SavingsRate = 50;

            Mock<IAsyncCursor<ConfigurationMongo>> mockOldData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockOldData.Setup(x => x.Current).Returns([configOnDb]);
            mockOldData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            Mock<IAsyncCursor<ConfigurationMongo>> mockNewData = new Mock<IAsyncCursor<ConfigurationMongo>>();
            mockNewData.Setup(x => x.Current).Returns([configOnDbUpdated]);
            mockNewData.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);

            _mockMongoCollection.SetupSequence(x => x.FindSync<ConfigurationMongo>(
                    Builders<ConfigurationMongo>.Filter.Empty,
                    default,
                    It.IsAny<CancellationToken>()))
                .Returns(mockOldData.Object).
                Returns(mockNewData.Object);

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.SetConfiguration(config));
        }

        [Test]
        public void UpdateConfigurationTest()
        {
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            Configuration config = ConfigurationFixture.GetConfiguration();
            configOnDb.SavingsRate = 50;
            config.SavingsRate = 50;

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockDbprovider.Setup(x => x.GetCollection<ConfigurationMongo>("configuration"))
                .Returns(_mockMongoCollection.Object);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Returns(mockUpdateResult.Object);

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            var result = _persistence.UpdateConfiguration(config);
            Assert.That(result, Is.True);
        }

        [Test]
        public void UpdateConfigurationTimeoutExceptionTest()
        {
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            Configuration config = ConfigurationFixture.GetConfiguration();
            configOnDb.SavingsRate = 50;
            config.SavingsRate = 50;

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockDbprovider.Setup(x => x.GetCollection<ConfigurationMongo>("configuration"))
                .Returns(_mockMongoCollection.Object);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<TimeoutException>();

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            Assert.Throws<TimeoutException>(() => _persistence.UpdateConfiguration(config));
        }

        [Test]
        public void UpdateConfigurationExceptionTest()
        {
            ConfigurationMongo configOnDb = ConfigurationFixture.GetConfigurationMongo();
            Configuration config = ConfigurationFixture.GetConfiguration();
            configOnDb.SavingsRate = 50;
            config.SavingsRate = 50;

            var mockUpdateResult = new Mock<UpdateResult>();
            mockUpdateResult.Setup(_ => _.IsAcknowledged).Returns(true);
            mockUpdateResult.Setup(_ => _.ModifiedCount).Returns(1);

            _mockDbprovider.Setup(x => x.GetCollection<ConfigurationMongo>("configuration"))
                .Returns(_mockMongoCollection.Object);

            _mockMongoCollection.Setup(m => m.UpdateOne(It.IsAny<FilterDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateDefinition<ConfigurationMongo>>(),
                It.IsAny<UpdateOptions>(),
                It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            _persistence = new ConfigurationPersistence(_mockDbprovider.Object);

            Assert.Throws<Exception>(() => _persistence.UpdateConfiguration(config));
        }
    }
}