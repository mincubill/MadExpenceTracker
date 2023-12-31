using MadExpenceTracker.Core.Model;
using MadExpenceTracker.Core.Persistence;
using MadExpenceTracker.Persistence.MongoDB.MongoConfiguration;
using MadExpenceTracker.Persistence.MongoDB.Persistence;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadExpenceTracker.Persistence.Test
{
    public class ConfigurationPersistenceTest
    {
        //private IConfigurationPersistence _configurationPersistence;

        //[SetUp]
        //public void Setup()
        //{
        //    Connection mongoConnection = new Connection();
        //    MongoClient mongoClient = mongoConnection.GetClient();
        //    IMongoDatabase mongoDatabase = mongoConnection.GetDatabase(mongoClient);
        //    _configurationPersistence = new ConfigurationPersistence(mongoDatabase);
        //}

        //[Test]
        //public void GetConfigurationTest()
        //{
        //    Configuration configuration = _configurationPersistence.GetConfiguration();
        //    Assert.That(configuration, Is.Not.Null);
        //}


        //[Test]
        //public void SetConfigurationTest()
        //{
        //    Configuration config = new Configuration()
        //    {
        //        SavingsRate = 15
        //    };
        //    Configuration result = _configurationPersistence.SetConfiguration(config);
        //    Assert.That(result, Is.Not.Null);
        //}
    }
}
